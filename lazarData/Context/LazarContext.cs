using lazarData.Models.Administration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazarData.Context {
    public class LazarContext : DbContext {
        internal DbSet<User> Users { get; set; }
        internal DbSet<Role> Roles { get; set; }
        internal DbSet<UserRole> UserRoles { get; set; }
        public LazarContext(DbContextOptions<LazarContext> options) : base(options) => Database.EnsureCreated();
        public LazarContext(string connectionString) : base(connectionString) {
            Database.SetInitializer<LazarContext>(new MigrateDatabaseToLatestVersion<LazarContext, Configuration>());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Persist Security Info=False;TrustServerCertificate=True;Trusted_Connection=True;Server=Lilith\SQLEXPRESS;Database=lazar_db;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Role>().HasMany(x => x.Users).WithRequired(f => f.Role).HasForeignKey(f => f.RoleId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Role>().HasOptional(x => x.Filial).WithMany(x => x.Roles).HasForeignKey(f => new { f.FilialId, f.FilialDateChange }).WillCascadeOnDelete(true);

            modelBuilder.Entity<User>().HasMany(x => x.Roles).WithRequired(f => f.User).HasForeignKey(f => f.UserId).WillCascadeOnDelete(true);

            modelBuilder.Entity<User>().HasData(
                    new User { Id = Guid.NewGuid(), Login = "Tom", Password = "adwa", Email = "aa@fawf.su" },
                    new User { Id = Guid.NewGuid(), Login = "Bob", Password = "adwa", Email = "aa@fawf.su" },
                    new User { Id = Guid.NewGuid(), Login = "Sam", Password = "adwa", Email = "aa@fawf.su" }
            );

            base.OnModelCreating(modelBuilder);
        }

        internal class InsertInitialData {
            public static void SeedRmData(LazarContext context) {

                context.Roles.AddRange(new Role[] {
                new Role() {
                    IsDefault = true,
                    Id = Guids.Role.AdministratorIA,
                    GroupAd = null,
                    Name = "Администратор ИА",
                    ChangedUserId = null,
                    DateChange = DateTime.UtcNow,
                },
                new Role() {
                    IsDefault = true,
                    Id = Guids.Role.AdministratorNSI,
                    GroupAd = null,
                    Name = "Администратор НСИ",
                    ChangedUserId = null,
                    DateChange = DateTime.UtcNow
                } 
                });

                context.SaveChanges();
            }

            public static void InitFilialIndicators(LazarContext context) {
                Guid currFilialId;
                Guid currIndicatorId;
                List<FilialIndicator> allFilialIndicators = new List<FilialIndicator>();
                var allFilials = context.Filials.ToList();              

                foreach (var filial in allFilials) {

                    string name = filial.Name.Contains("РДУ") ? "РДУ" : filial.Name.Contains("ОДУ") ? "ОДУ" : filial.Name.Contains("СО ЕЭС") || /*filial.Name.Contains("ИА") || */filial.Name.Contains("Исполнительный аппарат") ? "ИА" : "";
                    currFilialId = filial.Id;

                    foreach (var ind in allIndicators[name]) {

                        currIndicatorId = context.Indicators.FirstOrDefault(i => i.Name == ind).Id;

                        allFilialIndicators.Add(new FilialIndicator {
                            Id = Guid.NewGuid(),
                            FilialId = currFilialId,
                            FilialDateChange = filial.DateChange,
                            IndicatorId = currIndicatorId
                        });
                    }
                }
                context.FilialIndicators.AddRange(allFilialIndicators);
            }
        }
        internal sealed class Configuration : DbMigrationsConfiguration<LazarContext> {

            public Configuration() {
                AutomaticMigrationsEnabled = true;
                ContextKey = "SysRmConnection";
                AutomaticMigrationDataLossAllowed = true;
                CommandTimeout = 7200;
            }

            protected override void Seed(LazarContext context) {
                try {
                    InsertInitialData.AppendMissingSysSettings(context);
                    if (context.Roles.Any()) {
                        return;
                    }
                    InsertInitialData.SeedRmData(context);
                    base.Seed(context);
                } catch (DbEntityValidationException e) {
                    string msg = "";
                    foreach (var eve in e.EntityValidationErrors) {
                        msg += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                            msg += String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                    throw new Exception(msg);
                } catch (Exception exp) {
                    throw exp;
                }
            }
        }
        /// <summary>
        /// Инициализация БД
        /// </summary>
        public class RmDBInitializer : CreateDatabaseIfNotExists<LazarContext> {
            /// <summary>
            /// Заполнение по умолчанию
            /// </summary>
            /// <param name="context"></param>
            protected override void Seed(LazarContext context) {
                try {
                    InsertInitialData.SeedRmData(context);
                    base.Seed(context);
                } catch (Exception exp) {

                }
            }
        }
    }
}
