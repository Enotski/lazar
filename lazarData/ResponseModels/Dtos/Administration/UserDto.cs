using lazarData.Models.Response.ViewModels;

namespace lazarData.ResponseModels.Dtos.Administration
{
    public class UserDto : BaseResponseModel
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
