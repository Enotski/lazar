using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Base;

namespace Lazar.Domain.Core.SelectorModels.Administration {
    public class UserSelectorModel : NameSelectorModel {
        public readonly string Login;
        public readonly string Email;
        public readonly string Password;
        public readonly IEnumerable<string> Roles;
        public readonly IEnumerable<Guid> RoleIds;
        public readonly string RoleNames;
        public UserSelectorModel() : base() {
            RoleIds = new List<Guid>();
        }
        public UserSelectorModel(Guid id, IEnumerable<string> roles, IEnumerable<Guid> roleIds, string name, string login, string password, string email, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
            Login = login;
            Email = email;
            Password = password;
            Roles = roles;
            RoleNames = roles != null ? string.Join("; ", roles) : "";
            RoleIds = roleIds;
        }
    }
}
