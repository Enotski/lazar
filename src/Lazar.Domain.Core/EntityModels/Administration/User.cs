﻿using Lazar.Domain.Core.EntityModels.Base;
using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.Models.Administration {
    public class User : NameEntity, ILogin {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public User(Guid id, string name, string login, string password, string email, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
            Login = login;
            Email = email;
            Password = password;
        }
        public User(string name, string login, string password, string email, string changedBy) 
            : this(Guid.NewGuid(), name, login, password, email, changedBy, DateTime.UtcNow) {
        }
        public void Update(string name, string login, string password, string email, IEnumerable<Role> roles, string changedBy) {
            Login = login;
            Email = email;
            Password = password;
            Roles = roles?.ToList();
            base.Update(name, changedBy);
        }
        public void Update(IEnumerable<Role> roles, string changedBy) {
            Roles = roles.ToList();
            base.Update(changedBy);
        }
    }
}