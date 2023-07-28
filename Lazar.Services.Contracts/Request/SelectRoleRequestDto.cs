namespace Lazar.Services.Contracts.Request {
    public class SelectRoleRequestDto : SelectRequestDto
    {
        public Guid? SelectedUserId { get; set; }
    }
}
