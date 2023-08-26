using Lazar.Domain.Core.EntityModels.Logging;
using Lazar.Domain.Core.Keys;
using Lazar.Domain.Core.Models.Administration;
using Microsoft.EntityFrameworkCore;

namespace Lazar.Infrastructure.Data.Ef.Context {
    public class LazarContext : DbContext {
        internal DbSet<User> Users { get; set; }
        internal DbSet<Role> Roles { get; set; }
        internal DbSet<SystemLog> SystemLogs { get; set; }
        public DbSet<AuthModel> AuthModels { get; set; }
        public LazarContext(DbContextOptions<LazarContext> options) : base(options) {
            Database.EnsureCreated();
        }
        private static DbContextOptions<LazarContext> ModifyOptions(DbContextOptions<LazarContext> options) {
            var optionsBuilder = new DbContextOptionsBuilder<LazarContext>(ModifyOptions(options));
            // Disable change tracking by defaultй 
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            return optionsBuilder.Options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity(j => j.ToTable("UserRole"));

            var roles = new Role[] {
                new Role(RoleKeys.User, "User", "_", DateTime.UtcNow),
                new Role(RoleKeys.Admin, "Admin", "_", DateTime.UtcNow),
                new Role(RoleKeys.Moderator, "Moderator", "_", DateTime.UtcNow)
            };
            modelBuilder.Entity<Role>().HasData(roles);

            modelBuilder.Entity<User>().HasData(
                new User("Tom", "Tom", "adwa", "aa@fawf.su", "_"),
                new User("Bob", "Bob", "adwa", "aa@fawf.su", "_"),
                new User("Sam", "Sam", "adwa", "aa@fawf.su", "_")
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
