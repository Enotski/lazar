namespace Lazar.Services.Contracts.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class ChangedByDto : DateChangeDto {
        /// <summary>
        /// Пользователь изменивший запись
        /// </summary>
        public string ChangedBy { get; set; }
        public ChangedByDto() : base() { }
        public ChangedByDto(Guid id, string changedBy, DateTime dateChange) : base(id, dateChange) {
            ChangedBy = changedBy;
        }
    }
}
