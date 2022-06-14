using breweries_wholesellers.Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace breweries_wholesellers.Entity.Context
{
    public static class DbContextExtension
    {

        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
            //return false;
        }

        public static void EnsureSeeded(this breweries_wholesellersContext context)
        {

            if (!context.Brewery.Any())
            {
                var breweries = JsonConvert.DeserializeObject<List<Brewery>>(File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "breweries.json"));
                context.AddRange(breweries);
                context.SaveChanges();
            }


            if (!context.Wholasaller.Any())
            {
                var wholesellers = JsonConvert.DeserializeObject<List<Wholesaller>>(File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "wholesallers.json"));
                context.AddRange(wholesellers);
                context.SaveChanges();
            }

            if (!context.Beer.Any())
            {
                var beers = JsonConvert.DeserializeObject<List<Beer>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "beers.json"));
                context.AddRange(beers);
                context.SaveChanges();
            }

            if (!context.WholesallerStock.Any())
            {
                var wholesallerStocks = JsonConvert.DeserializeObject<List<WholesallerStock>>(File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "wholesallerStocks.json"));
                context.AddRange(wholesallerStocks);
                context.SaveChanges();
            }

            if (!context.WholesallerSale.Any())
            {
                var wholesallerSales = JsonConvert.DeserializeObject<List<WholesallerSale>>(File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "wholesallerSales.json"));
                context.AddRange(wholesallerSales);
                context.SaveChanges();
            }
        }

    }
}
