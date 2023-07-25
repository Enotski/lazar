using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.EntityModels.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class ChangedByEntity : DateChangeEntity, IChanged {
        /// <summary>
        /// Пользователь изменивший запись
        /// </summary>
        public string ChangedBy { get; set; }
        public ChangedByEntity() : base() { }
        public ChangedByEntity(Guid id, string changedBy, DateTime dateChange) : base(id, dateChange) {
            ChangedBy = changedBy;
        }
        public virtual void Update(string changedBy) {
            ChangedBy = changedBy;
            base.Update();

        }
    }
}
