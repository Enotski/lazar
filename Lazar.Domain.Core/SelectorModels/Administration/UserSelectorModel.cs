using Lazar.Domain.Core.SelectorModels.Base;

namespace Lazar.Domain.Core.SelectorModels.Administration {
    public class UserSelectorModel : NameSelectorModel {
        public readonly string Login;
        public readonly string Email;
        public readonly string Password;
        public readonly string Roles;
        public readonly IReadOnlyList<Guid> RoleIds;
        public UserSelectorModel() : base() {
            RoleIds = new List<Guid>();
        }
        public UserSelectorModel(Guid id, string roleNames, IReadOnlyList<Guid> roleIds, string name, string login, string password, string email, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
            Login = login;
            Email = email;
            Password = password;
            Roles = roleNames;
            RoleIds = roleIds;
        }
    }
}
