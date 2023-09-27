using CommonUtils.Utils;
using Lazar.Domain.Core.EntityModels.Logging;
using Lazar.Domain.Core.Keys;
using Lazar.Domain.Core.Models.Administration;

namespace Lazar.Services.Tests.Mocks {
    internal static class TestData {
        public static List<Role> Roles = new List<Role>() {
                new Role(RoleKeys.User, "User", "_", DateTime.UtcNow),
                new Role(RoleKeys.Admin, "Admin", "_", DateTime.UtcNow),
                new Role(RoleKeys.Moderator, "Moderator", "_", DateTime.UtcNow)
            };
        static PasswordHelper _hasher = new PasswordHelper();
        public static string FakeUsersPass = "adwa";
        public static List<User> Users = new List<User>() {
                new User("Tom", "Tom", _hasher.HashPassword(FakeUsersPass), "aa@fawf.su", "_"),
                new User("Bob", "Bob", _hasher.HashPassword(FakeUsersPass), "aa@fawf.su", "_"),
                new User("Sam", "Sam", _hasher.HashPassword(FakeUsersPass), "aa@fawf.su", "_")
            };
        public static List<SystemLog> Logs = new List<SystemLog>() {
                new SystemLog("Role added", Domain.Core.Enums.SubSystemType.Roles, Domain.Core.Enums.EventType.Create, "_"),
                new SystemLog("Role deleted", Domain.Core.Enums.SubSystemType.Roles, Domain.Core.Enums.EventType.Delete, "_"),
                new SystemLog("Role changed", Domain.Core.Enums.SubSystemType.Roles, Domain.Core.Enums.EventType.Update, "_"),
                new SystemLog("User added", Domain.Core.Enums.SubSystemType.Users, Domain.Core.Enums.EventType.Create, "_"),
                new SystemLog("User deleted", Domain.Core.Enums.SubSystemType.Users, Domain.Core.Enums.EventType.Delete, "_"),
                new SystemLog("User changed", Domain.Core.Enums.SubSystemType.Users, Domain.Core.Enums.EventType.Update, "_"),
            };
        public static List<AuthModel> AuthModels = new List<AuthModel>() {
                new AuthModel("Tom", "tom_token", DateTime.MaxValue),
                new AuthModel("Bob", "bob_token", DateTime.MaxValue),
                new AuthModel("Sam", "sam_token", DateTime.MaxValue),
            };
    }
}
