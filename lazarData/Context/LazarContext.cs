using lazarData.Models.Administration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazarData.Context
{
    public class LazarContext : DbContext
    {
        internal DbSet<User> Users => Set<User>();
        internal DbSet<Role> Roles => Set<Role>();
        internal DbSet<UserRole> UserRoles => Set<UserRole>();
        public LazarContext(DbContextOptions<LazarContext> options) : base(options) => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Persist Security Info=False;TrustServerCertificate=True;Trusted_Connection=True;Server=Lilith\SQLEXPRESS;Database=lazar_db;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { Id = Guid.NewGuid(), Login = "Tom", Password = "adwa", Email = "aa@fawf.su" },
                    new User { Id = Guid.NewGuid(), Login = "Bob", Password = "adwa", Email = "aa@fawf.su" },
                    new User { Id = Guid.NewGuid(), Login = "Sam", Password = "adwa", Email = "aa@fawf.su" }
            );
        }
    }
}
