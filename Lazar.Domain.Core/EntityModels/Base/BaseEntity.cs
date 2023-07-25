using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.EntityModels.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class BaseEntity : IKey {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        public Guid Id { get; set; }
        public BaseEntity() { }
        public BaseEntity(Guid id) {
            Id = id;
        }
    }
}
