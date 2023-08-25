namespace Lazar.Services.Contracts.Request {
    public class KeyNameRequestDto {
        public string Search { get; set; }
        public PaginatedOptionDto? Pagination { get; set; }
    }
}
