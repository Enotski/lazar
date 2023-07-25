using Lazar.Services.Contracts.Request.DataGrid.Base;

namespace Lazar.Services.Contracts.Request.DataGrid
{
    public class RoleDataGridRequestDto : DataGridRequestDto
    {
        public string? selectedUserId { get; set; } = "";
    }
}
