namespace Lazar.Services.Contracts.Administration {
    public class UserRoleDto {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsAddRole { get; set; }
    }
}
