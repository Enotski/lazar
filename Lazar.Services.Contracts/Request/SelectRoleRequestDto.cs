namespace Lazar.Services.Contracts.Request {
    public class SelectRoleRequestDto : SelectRequestDto
    {
        public string? selectedUserId { get; set; }
    }
}
