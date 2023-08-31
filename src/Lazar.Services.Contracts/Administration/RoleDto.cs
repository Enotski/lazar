using Lazar.Services.Contracts.Base;

namespace Lazar.Services.Contracts.Administration {
    public class RoleDto : NameDto {
        public RoleDto() : base() { }
        public RoleDto(Guid id, string name, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
        }
    }
}
