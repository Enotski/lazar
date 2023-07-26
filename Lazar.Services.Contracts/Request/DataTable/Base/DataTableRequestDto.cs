using Lazar.Domain.Interfaces.Options;

namespace Lazar.Services.Contracts.Request.DataTable.Base {
    public class DataTableRequestDto
    {
        public IPaginatedOption Pagination { get; set; }
        public IEnumerable<ISearchOption>? Filters { get; set; }
        public IEnumerable<ISortOption>? Sorts { get; set; }
    }
}
