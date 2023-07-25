namespace Lazar.Services.Contracts.Request.DataGrid.Base {
    public class DataGridRequestDto
    {
        public int skip { get; set; }
        public int take { get; set; }
        public IEnumerable<DataGridFilterDto>? filters { get; set; }
        public IEnumerable<DataGridSortDto>? sorts { get; set; }
    }
}
