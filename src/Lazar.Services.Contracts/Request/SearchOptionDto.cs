using Lazar.Domain.Interfaces.Options;

namespace Lazar.Services.Contracts.Request {
    public class SearchOptionDto : ISearchOption {
        public string ColumnName { get; set; }
        public string Value { get; set ; }
    }
}
