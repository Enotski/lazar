using Lazar.Domain.Core.SelectorModels.Base;

namespace Lazar.Domain.Core.SelectorModels.Administration {
    /// <summary>
    /// User model for select from storage
    /// </summary>
    public class UserSelectorModel : NameSelectorModel {
        public readonly string Login;
        public readonly string Email;
        public readonly string Password;
        /// <summary>
        /// List of roles names
        /// </summary>
        public readonly IEnumerable<string> RoleNames;
        /// <summary>
        /// Keys of roles
        /// </summary>
        public readonly IEnumerable<Guid> RoleIds;
        /// <summary>
        /// List of roles names in raw string
        /// </summary>
        public readonly string Roles;
        public UserSelectorModel(Guid id, IEnumerable<string> roles, IEnumerable<Guid> roleIds, string name, string login, string password, string email, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
            Login = login;
            Email = email;
            Password = password;
            RoleNames = roles;
            Roles = roles != null ? string.Join("; ", roles) : "";
            RoleIds = roleIds;
        }
    }
}
