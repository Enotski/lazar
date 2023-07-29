namespace Lazar.Services.Contracts.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class DateChangeDto : BaseDto {
        /// <summary>
        /// Дата изменения записи
        /// </summary>
        public DateTime? DateChange { get; set; }
        public DateChangeDto() : base() { }
        public DateChangeDto(Guid id, DateTime dateChange)
            : base(id) {
            DateChange = dateChange;
        }
    }
}
