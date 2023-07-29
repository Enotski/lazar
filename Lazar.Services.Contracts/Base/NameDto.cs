namespace Lazar.Services.Contracts.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class NameDto : ChangedByDto {
        /// <summary>
        /// Наименование
        /// </summary>
        public string? Name { get; set; }
        public NameDto() : base() { }
        public NameDto(Guid id, string name, string changedBy, DateTime dateChange)
            : base(id, changedBy, dateChange) {
            Name = name;
        }
    }
}
