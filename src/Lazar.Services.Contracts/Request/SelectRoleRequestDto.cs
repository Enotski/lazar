namespace Lazar.Services.Contracts.Request {
    public class SelectRoleRequestDto : KeyNameRequestDto {
        public Guid? SelectedUserId { get; set; }
    }
}
