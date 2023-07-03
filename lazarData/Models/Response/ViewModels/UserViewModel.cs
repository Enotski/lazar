namespace lazarData.Models.Response.ViewModels
{
    public class UserViewModel : BaseResponseModel
    {
        public Guid? Id { get; set; } = null;
        public Guid? RoleId { get; set; } = null;
        public string? Login { get; set; } = "";
        public string? Email { get; set; } = "";
        public string? Password { get; set; } = "";
        public UserViewModel(string mess): base(mess) { }
        public UserViewModel(): base() { }
    }
}
