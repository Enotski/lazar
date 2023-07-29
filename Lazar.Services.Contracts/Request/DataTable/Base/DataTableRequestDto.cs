using Lazar.Domain.Interfaces.Options;

namespace Lazar.Services.Contracts.Request.DataTable.Base {
    public class DataTableRequestDto
    {
        public PaginatedOptionDto Pagination { get; set; }
        public IEnumerable<SearchOptionDto>? Filters { get; set; }
        public IEnumerable<SortOptionDto>? Sorts { get; set; }
    }
}
