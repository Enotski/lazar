using Lazar.Domain.Core.Enums;

namespace Lazar.Services.Contracts.Request.DataGrid.Base {
    public class DataGridSortDto
    {
        public string ColumnName { get; set; }
        public SortType Type { get; set; }
    }
}
