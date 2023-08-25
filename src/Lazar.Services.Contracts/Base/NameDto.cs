namespace Lazar.Services.Contracts.Base {
    /// <summary>
    /// Base named dto model
    /// </summary>
    public abstract class NameDto : ChangedByDto {
        public string? Name { get; set; }
        public NameDto() : base() { }
        public NameDto(Guid id, string name, string changedBy, DateTime dateChange)
            : base(id, changedBy, dateChange) {
            Name = name;
        }
    }
}
