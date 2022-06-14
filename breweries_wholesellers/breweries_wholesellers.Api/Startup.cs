using AutoMapper;
using breweries_wholesellers.Api;
using breweries_wholesellers.Domain.Mapping;
using breweries_wholesellers.Domain.Service;
using breweries_wholesellers.Entity.Context;
using breweries_wholesellers.Entity.Repository;
using breweries_wholesellers.Entity.UnitofWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace breweries_wholesellers.Api
{
    public class Startup
    {

        public static IConfiguration Configuration { get; set; }
        public IWebHostEnvironment HostingEnvironment { get; private set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            Log.Information("Startup::ConfigureServices");

            try
            {
                services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

                #region "API versioning"
                //API versioning service
                services.AddApiVersioning(
                    o =>
                    {
                        o.AssumeDefaultVersionWhenUnspecified = true;
                        o.ReportApiVersions = true;
                        o.DefaultApiVersion = new ApiVersion(1, 0);
                        o.ApiVersionReader = new UrlSegmentApiVersionReader();
                    }
                    );

                // format code as "'v'major[.minor][-status]"
                services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    //versioning by url segment
                    options.SubstituteApiVersionInUrl = true;
                });
                #endregion

                //db service
                if (Configuration["ConnectionStrings:UseInMemoryDatabase"] == "True")
                    services.AddDbContext<breweries_wholesellersContext>(opt => opt.UseInMemoryDatabase("TestDB-" + Guid.NewGuid().ToString()));
                else
                    services.AddDbContext<breweries_wholesellersContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:breweries_wholesellersDB"]));

                #region "Authentication"
                //Authentication:IdentityServer4 - full version
                //JWT API authentication service
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                }
                 );
                #endregion

                #region "CORS"
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy-public",
                        builder => builder.AllowAnyOrigin()   
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                    //.AllowCredentials()
                    .Build());
                });
                #endregion

                #region "MVC and JSON options"
                services.AddMvc(option => option.EnableEndpointRouting = false)
            .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });  //NO entity classes' children serialization
                                                                                                                                                  //.AddNewtonsoftJson(ops =>
                                                                                                                                                  //{
                                                                                                                                                  //    ops.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
                                                                                                                                                  //    ops.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                                                                                                                                                  //}); //WITH entity classes' children serialization
                #endregion

                #region "DI code"
                //general unitofwork injections
                services.AddTransient<IUnitOfWork, UnitOfWork>();

                //services injections
                services.AddTransient(typeof(BreweryService<,>), typeof(BreweryService<,>));
                services.AddTransient(typeof(BeerService<,>), typeof(BeerService<,>));
                services.AddTransient(typeof(WholesallerService<,>), typeof(WholesallerService<,>));
                services.AddTransient(typeof(WholesallerStockService<,>), typeof(WholesallerStockService<,>));
                services.AddTransient(typeof(WholesallerSaleService<,>), typeof(WholesallerSaleService<,>));
                //...add other services
                //
                services.AddTransient(typeof(IService<,>), typeof(GenericService<,>));
                services.AddTransient(typeof(IServiceAsync<,>), typeof(GenericServiceAsync<,>));
                #endregion

                //data mapper services configuration
                services.AddAutoMapper(typeof(MappingProfile));
                services.AddSwaggerGen();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            Log.Information("Startup::Configure");

            try
            {
                if (env.EnvironmentName == "Development")
                {
                    app.UseDeveloperExceptionPage();

                    app.UseSwagger(); 
                    app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                        options.RoutePrefix = string.Empty;
                    });
                }
                else
                    app.UseMiddleware<ExceptionHandler>();

                app.UseCors("CorsPolicy-public");  //apply to every request
                app.UseAuthentication(); //needs to be up in the pipeline, before MVC
                app.UseAuthorization();

                app.UseMvc();

                //migrations and seeds from json files
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    if (Configuration["ConnectionStrings:UseInMemoryDatabase"] == "False" && !serviceScope.ServiceProvider.GetService<breweries_wholesellersContext>().AllMigrationsApplied())
                    {
                        if (Configuration["ConnectionStrings:UseMigrationService"] == "True")
                            serviceScope.ServiceProvider.GetService<breweries_wholesellersContext>().Database.Migrate();
                    }
                    //it will seed tables on aservice run from json files if tables empty
                    if (Configuration["ConnectionStrings:UseSeedService"] == "True")
                        serviceScope.ServiceProvider.GetService<breweries_wholesellersContext>().EnsureSeeded();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }
    }
}










