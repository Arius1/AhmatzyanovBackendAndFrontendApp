using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ahmatzyanov_lab2.Models;

namespace ahmatzyanov_lab2.Contexts
{
    public class AuthContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public AuthContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=lab2_db_users;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User u1 = new User { Id = 1, Login = "user", Role = RoleNames.User, Password = "12345" };
            User u2 = new User { Id = 2, Login = "admin", Role = RoleNames.Admin, Password = "55555" };

            modelBuilder.Entity<User>().HasData(u1, u2);

        }
    }
}
