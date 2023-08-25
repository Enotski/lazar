namespace Lazar.Domain.Core.SelectorModels.Base {
    /// <summary>
    /// Base changed by info model for select from storage
    /// </summary>
    public abstract class ChangedBySelectorModel : DateChangeSelectorModel {
        /// <summary>
        /// Changed by info
        /// </summary>
        public readonly string ChangedBy;
        public ChangedBySelectorModel() : base() { }
        public ChangedBySelectorModel(Guid id, string changedBy, DateTime dateChange) : base(id, dateChange) {
            ChangedBy = changedBy;
        }
    }
}
