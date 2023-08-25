namespace Lazar.Services.Contracts.Request {
    public class SelectRequestDto {
        public string? SearchExpr { get; set; }
        public string? SearchOperation { get; set; }
        public string? SearchValue { get; set; }
    }
}
