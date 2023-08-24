namespace Lazar.Domain.Core.SelectorModels.Base {
    /// <summary>
    /// Base model for select from storage
    /// </summary>
    public abstract class BaseSelectorModel {
        /// <summary>
        /// Primary key
        /// </summary>
        public readonly Guid Id;
        public BaseSelectorModel() { }
        public BaseSelectorModel(Guid id) {
            Id = id;
        }
    }
}
