using Lazar.Domain.Interfaces.Options;

namespace Lazar.Services.Contracts.Request {
    public class ListRequestDto {
        public IEnumerable<ISearchOption>? Filters { get; set; }
        public IEnumerable<ISortOption>? Sorts { get; set; }
    }
}
