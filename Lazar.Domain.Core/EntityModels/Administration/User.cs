using Lazar.Domain.Core.EntityModels.Base;
using Lazar.Domain.Core.EntityModels.EventLogs;

namespace Lazar.Domain.Core.Models.Administration {
    public class User : NameEntity {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public ICollection<SystemLog> ChangedEventLogs { get; set; } = new List<SystemLog>();

        public User() : base() { }
        public User(Guid id, string name, string login, string password, string email, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
            Login = login;
            Email = email;
            Password = password;
        }
        public void Update(string name, string login, string password, string email, string changedBy) {
            Login = login;
            Email = email;
            Password = password;
            base.Update(name, changedBy);
        }
    }
}