using Lazar.Domain.Core.SelectorModels.Base;

namespace Lazar.Domain.Core.SelectorModels.Administration {
    public class UserSelectorModel : NameSelectorModel {
        public readonly string Login;
        public readonly string Email;
        public readonly string Password;
        public UserSelectorModel() : base() { }
        public UserSelectorModel(Guid id, string name, string login, string password, string email, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
            Login = login;
            Email = email;
            Password = password;
        }
    }
}
