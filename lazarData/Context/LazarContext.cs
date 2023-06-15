using lazarData.Models.Administration;
using lazarData.Models.EventLogs;
using lazarData.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazarData.Context
{
    public class LazarContext : DbContext
    {
        internal DbSet<User> Users { get; set; }
        internal DbSet<Role> Roles { get; set; }
        internal DbSet<EventLog> EventLogs { get; set; }
        public LazarContext(DbContextOptions<LazarContext> options) : base(options) => Database.EnsureCreated();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasOne(x => x.ChangedUser).WithMany(f => f.ChangedRoles).HasForeignKey(f => f.ChangedUserId).HasPrincipalKey(x => x.Id);
            modelBuilder.Entity<EventLog>().HasOne(x => x.ChangedUser).WithMany(f => f.ChangedEventLogs).HasForeignKey(f => f.ChangedUserId).HasPrincipalKey(x => x.Id);
            modelBuilder.Entity<User>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity(j => j.ToTable("UserRole"));

            modelBuilder.Entity<User>().HasData(
                    new User { Id = Guid.NewGuid(), Login = "Tom", Password = "adwa", Email = "aa@fawf.su" },
                    new User { Id = Guid.NewGuid(), Login = "Bob", Password = "adwa", Email = "aa@fawf.su" },
                    new User { Id = Guid.NewGuid(), Login = "Sam", Password = "adwa", Email = "aa@fawf.su" }
            );
            modelBuilder.Entity<Role>().HasData(
                   new Role()
                   {
                       Id = Guids.Roles.Administrator,
                       Name = "Admin",
                       ChangedUserId = null,
                       DateChange = DateTime.UtcNow,
                   },
                   new Role()
                   {
                       Id = Guids.Roles.Moderator,
                       Name = "Moderator",
                       ChangedUserId = null,
                       DateChange = DateTime.UtcNow
                   });

            base.OnModelCreating(modelBuilder);
        }
    }
}
