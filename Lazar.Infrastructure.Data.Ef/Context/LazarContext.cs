using Lazar.Domain.Core.EntityModels.Logging;
using Lazar.Domain.Core.Keys;
using Lazar.Domain.Core.Models.Administration;
using Microsoft.EntityFrameworkCore;

namespace Lazar.Infrastructure.Data.Ef.Context {
    public class LazarContext : DbContext {
        internal DbSet<User> Users { get; set; }
        internal DbSet<Role> Roles { get; set; }
        internal DbSet<SystemLog> SystemLogs { get; set; }
        public LazarContext(DbContextOptions<LazarContext> options) : base(options) {
            Database.EnsureCreated();
        }
        private static DbContextOptions<LazarContext> ModifyOptions(DbContextOptions<LazarContext> options) {
            var optionsBuilder = new DbContextOptionsBuilder<LazarContext>(ModifyOptions(options));
            // По умолчанию отключаем отслеживание изменений https://metanit.com/sharp/entityframeworkcore/5.7.php
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            return optionsBuilder.Options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity(j => j.ToTable("UserRole"));

            var roles = new Role[] {
                new Role() {
                       Id = RoleKeys.User,
                       Name = "User",
                       DateChange = DateTime.UtcNow,
                   },
                   new Role() {
                       Id = RoleKeys.Admin,
                       Name = "Admin",
                       DateChange = DateTime.UtcNow,
                   },
                   new Role() {
                       Id = RoleKeys.Moderator,
                       Name = "Moderator",
                       DateChange = DateTime.UtcNow
                   }
            };
            modelBuilder.Entity<Role>().HasData(roles);
            
            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.NewGuid(), Name = "Tom", Login = "Tom", Password = "adwa", Email = "aa@fawf.su"},
                new User { Id = Guid.NewGuid(), Name = "Tom", Login = "Bob", Password = "adwa", Email = "aa@fawf.su"},
                new User { Id = Guid.NewGuid(), Name = "Tom", Login = "Sam", Password = "adwa", Email = "aa@fawf.su" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
