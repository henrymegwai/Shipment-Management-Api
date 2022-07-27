using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration) 
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase(databaseName: "ShipmentData");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shipment>(entity =>
            {
               
                entity.HasOne<Driver>(x => x.driver).WithMany("Shipments");
            });
            modelBuilder.Entity<Driver>(entity =>
            {
                
                entity.HasMany<Shipment>(x => x.Shipments).WithOne("driver");
            });

        }
        public DbSet<Shipment> Shipments { get; set; } 
        public DbSet<Driver> Drivers { get; set; }
    }
}
