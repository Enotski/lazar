namespace Lazar.Services.Contracts.Request
{
    public class SelectRequestDto
    {
        public string? searchExpr { get; set; }
        public string? searchOperation { get; set; }
        public string? searchValue { get; set; }
    }
}
