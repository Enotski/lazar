using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.EntityModels.Base {
    /// <summary>
    /// Base entity model
    /// </summary>
    public abstract class BaseEntity : IKey {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Id { get; set; }
        public BaseEntity() { }
        public BaseEntity(Guid id) {
            Id = id;
        }
    }
}
