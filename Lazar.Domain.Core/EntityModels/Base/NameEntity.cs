using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.EntityModels.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class NameEntity : ChangedByEntity, IName {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        public NameEntity() : base() { }
        public NameEntity(Guid id, string name, string changedBy, DateTime dateChange)
            : base(id, changedBy, dateChange) {
            Name = name;
        }
        public virtual void Update(string name, string changedBy) {
            Name = name;
            base.Update(changedBy);
        }
    }
}
