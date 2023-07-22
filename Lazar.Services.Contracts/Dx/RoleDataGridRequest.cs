using lazarData.ResponseModels.Dx.Base;

namespace lazarData.ResponseModels.Dx
{
    public class RoleDataGridRequest : DataGridRequestModel
    {
        public string? selectedUserId { get; set; } = "";
    }
}
