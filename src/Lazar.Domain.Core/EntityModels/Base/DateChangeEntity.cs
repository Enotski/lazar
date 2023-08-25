using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.EntityModels.Base {
    /// <summary>
    /// Base model of date change info
    /// </summary>
    public abstract class DateChangeEntity : BaseEntity, IDateChange {
        /// <summary>
        /// Date change of entity in UTC format
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
