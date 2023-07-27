﻿using Lazar.Services.Contracts.Base;

namespace Lazar.Services.Contracts.Administration {
    public class UserDto : NameDto
    {
        public IEnumerable<Guid> RoleIds { get; set; }
        public string? RoleNames { get; set; } = null;
        public string? Login { get; set; } = "";
        public string? Email { get; set; } = "";
        public string? Password { get; set; } = "";
        public UserDto() : base() { }
        public UserDto(Guid id, string name, string login, string password, string email, string roleNames, IEnumerable<Guid> roleIds, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
            Login = login;
            Email = email;
            Password = password;
            RoleNames = roleNames;
            RoleIds = roleIds;
        }
    }
}
