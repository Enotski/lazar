using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.EntityModels.Base {
    /// <summary>
    /// Base entity with changed by info
    /// </summary>
    public abstract class ChangedByEntity : DateChangeEntity, IChanged {
        /// <summary>
        /// User, which changed entity
        /// </summary>
        public string? ChangedBy { get; set; }
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
