using Lazar.Services.Contracts.Base;

namespace Lazar.Services.Contracts.Administration {
    public class UserDto : NameDto
    {
        public IEnumerable<Guid> RoleIds { get; set; }
        public string? Roles { get; set; } = null;
        public string? Login { get; set; } = "";
        public string? Email { get; set; } = "";
        public string? Password { get; set; } = "";
        public UserDto() : base() { }
        public UserDto(Guid id, string name, string login, string password, string email, string roles, IEnumerable<Guid> roleIds, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
            Login = login;
            Email = email;
            Password = password;
            Roles = roles;
            RoleIds = roleIds;
        }
    }
}
