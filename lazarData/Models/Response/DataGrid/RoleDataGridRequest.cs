using lazarData.Models.Response.DataGrid.Base;

namespace lazarData.Models.Response.DataGrid
{
    public class RoleDataGridRequest : DataGridRequestModel
    {
        public string? selectedUserId { get; set; } = "";
    }
}
