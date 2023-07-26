namespace Lazar.Services.Contracts.Request {
    public class SelectRoleRequestDto : SelectRequestDto
    {
        public string? SelectedUserId { get; set; }
    }
}
