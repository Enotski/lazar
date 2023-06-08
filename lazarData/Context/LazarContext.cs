using LazarModel.Users;
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
        public DbSet<User> Users => Set<User>();
        public LazarContext(DbContextOptions<LazarContext> options) : base(options) => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Persist Security Info=False;TrustServerCertificate=True;Trusted_Connection=True;Server=Lilith\SQLEXPRESS;Database=lazar_db;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { Id = Guid.NewGuid(), Name = "Tom", Password = "adwa", Email = "aa@fawf.su" },
                    new User { Id = Guid.NewGuid(), Name = "Bob", Password = "adwa", Email = "aa@fawf.su" },
                    new User { Id = Guid.NewGuid(), Name = "Sam", Password = "adwa", Email = "aa@fawf.su" }
            );
        }
    }
}
