using lazarData.Models.Administration;
using lazarData.Models.EventLogs;
using Microsoft.EntityFrameworkCore;

namespace Lazar.Infrastructure.Data.Ef.Context
{
    public class LazarContext : DbContext
    {
        internal DbSet<User> Users { get; set; }
        internal DbSet<Role> Roles { get; set; }
        internal DbSet<EventLog> EventLogs { get; set; }
        public LazarContext(DbContextOptions<LazarContext> options) : base(options) => Database.EnsureCreated();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventLog>().HasOne(x => x.ChangedUser).WithMany(f => f.ChangedEventLogs).HasForeignKey(f => f.ChangedUserId).HasPrincipalKey(x => x.Id);
            modelBuilder.Entity<User>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity(j => j.ToTable("UserRole"));
            var users = new List<User>()
            {
                new User { Id = Guid.NewGuid(), Login = "Tom", Password = "adwa", Email = "aa@fawf.su" },
                new User { Id = Guid.NewGuid(), Login = "Bob", Password = "adwa", Email = "aa@fawf.su" },
                new User { Id = Guid.NewGuid(), Login = "Sam", Password = "adwa", Email = "aa@fawf.su" }
            };
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Role>().HasData(
                   new Role()
                   {
                       Id = Guids.Roles.Administrator,
                       Name = "Admin",
                       DateChange = DateTime.UtcNow,
                   },
                   new Role()
                   {
                       Id = Guids.Roles.Moderator,
                       Name = "Moderator",
                       DateChange = DateTime.UtcNow
                   });

            base.OnModelCreating(modelBuilder);
        }
    }
}
