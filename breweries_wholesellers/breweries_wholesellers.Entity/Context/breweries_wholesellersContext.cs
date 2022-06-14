using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace breweries_wholesellers.Entity.Context
{
    public partial class breweries_wholesellersContext : DbContext
    {

        public breweries_wholesellersContext(DbContextOptions<breweries_wholesellersContext> options)
            : base(options)
        {
        }


        public DbSet<Beer> Beer { get; set; }
        public DbSet<Brewery> Brewery { get; set; }
        public DbSet<Wholesaller> Wholasaller { get; set; }
        public DbSet<WholesallerStock> WholesallerStock { get; set; }
        public DbSet<WholesallerStock> WholesallerSale { get; set; }

        //lazy-loading
        //https://entityframeworkcore.com/querying-data-loading-eager-lazy
        //https://docs.microsoft.com/en-us/ef/core/querying/related-data
        //EF Core will enable lazy-loading for any navigation property that is virtual and in an entity class that can be inherited

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=breweries_wholesellers;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<Beer>()
           .HasOne(u => u.Brewery)
           .WithMany(e => e.Beers);
            modelBuilder.Entity<WholesallerStock>()
           .HasOne(u => u.Beer)
           .WithMany(e => e.WholesallerStock);
            modelBuilder.Entity<WholesallerStock>()
           .HasOne(u => u.Wholesaller)
           .WithMany(e => e.WholesallerStock);
            modelBuilder.Entity<WholesallerSale>()
           .HasOne(u => u.Wholesaller)
           .WithMany(e => e.WholesallerSale);
            modelBuilder.Entity<WholesallerSale>()
           .HasOne(u => u.Beer)
           .WithMany(e => e.WholesallerSale);


            //concurrency
            modelBuilder.Entity<Beer>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Brewery>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<WholesallerStock>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<Wholesaller>()
            .Property(a => a.RowVersion).IsRowVersion();
            modelBuilder.Entity<WholesallerSale>()
            .Property(a => a.RowVersion).IsRowVersion();
        }

        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            Audit();
            return await base.SaveChangesAsync();
        }

        private void Audit()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.UtcNow;
                }
            ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }

    }
}
