using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.EntityModels.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class DateChangeEntity : BaseEntity, IDateChange {
        /// <summary>
        /// Дата изменения записи
        /// </summary>
        public DateTime DateChange { get; set; }
        public DateChangeEntity() : base() { }
        public DateChangeEntity(Guid id, DateTime dateChange)
            : base(id) {
            DateChange = dateChange;
        }
        public virtual void Update() {
            DateChange = DateTime.UtcNow;
        }
    }
}
