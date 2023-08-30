namespace Lazar.Services.Contracts.Request {
    public class SelectRoleRequestDto : KeyNameRequestDto {
        public string? SelectedUserId { get; set; }
    }
}
