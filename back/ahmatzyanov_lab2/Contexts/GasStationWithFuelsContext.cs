using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ahmatzyanov_lab2.Models;

namespace ahmatzyanov_lab2.Contexts
{
    public class GasStationWithFuelsContext : DbContext
    {
        public DbSet<Fuel> Fuels { get; set; } = null!;
        public DbSet<GasStation> GasStations { get; set; } = null!;

        public GasStationWithFuelsContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=lab2_db;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fuel>()
                .HasOne(u => u.GasStation)
                .WithMany(c => c.Fuels)
                .HasForeignKey(u => u.GasStationId);

            GasStation g1 = new GasStation { Id = 1, Name = "Заправка Боба-Строителя", Address = "ул. Строителей, д. 77", PhoneNumber = 77777777 };
            GasStation g2 = new GasStation { Id = 2, Name = "Заправка Винни-Пуха", Address = "ул. Пчеловодов, д. 88", PhoneNumber = 88888888 };

            modelBuilder.Entity<GasStation>().HasData(g1, g2);

            Fuel f1 = new Fuel { Id = 1, Brand = FuelNames.Brands.AI92, Price = 50, Value = 12000, GasStationId = 1 };
            Fuel f2 = new Fuel { Id = 2, Brand = FuelNames.Brands.AI95, Price = 53, Value = 9000, GasStationId = 1 };
            Fuel f3 = new Fuel { Id = 3, Brand = FuelNames.Brands.AI98, Price = 56, Value = 8000, GasStationId = 1 };
            Fuel f4 = new Fuel { Id = 4, Brand = FuelNames.Brands.AI92, Price = 50, Value = 15000, GasStationId = 2 };
            Fuel f5 = new Fuel { Id = 5, Brand = FuelNames.Brands.AI95, Price = 53, Value = 99000, GasStationId = 2 };
            Fuel f6 = new Fuel { Id = 6, Brand = FuelNames.Brands.AI98, Price = 56, Value = 88000, GasStationId = 2 };

            modelBuilder.Entity<Fuel>().HasData(f1, f2, f3, f4, f5, f6);

        }
    }
    
}
