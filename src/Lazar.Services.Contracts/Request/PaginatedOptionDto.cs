using Lazar.Domain.Interfaces.Options;

namespace Lazar.Services.Contracts.Request {
    public class PaginatedOptionDto : IPaginatedOption {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
