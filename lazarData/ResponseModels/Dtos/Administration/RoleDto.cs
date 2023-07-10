using lazarData.Models.Response.ViewModels;

namespace lazarData.ResponseModels.Dtos.Administration
{
    public class RoleDto : BaseResponseModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = "";
    }
}
