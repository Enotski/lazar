using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazar.Services.Contracts.Administration
{
    internal class UserDto
    {
        public Guid? Id { get; set; } = null;
        public Guid? RoleId { get; set; } = null;
        public string? Login { get; set; } = "";
        public string? Email { get; set; } = "";
        public string? Password { get; set; } = "";
        public UserDto(string mess) : base(mess) { }
        public UserDto() : base() { }
    }
}
