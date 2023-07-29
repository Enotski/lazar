using Lazar.Domain.Core.Enums;
using Lazar.Domain.Interfaces.Options;

namespace Lazar.Services.Contracts.Request {
    public class SortOptionDto : ISortOption {
        public string ColumnName { get; set; }
        public SortType Type { get; set; }
    }
}
