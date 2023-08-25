namespace Lazar.Domain.Core.SelectorModels.Base {
    /// <summary>
    /// Base date change model for select from storage
    /// </summary>
    public abstract class DateChangeSelectorModel : BaseSelectorModel {
        /// <summary>
        /// Date change in UTC
        /// </summary>
        public readonly DateTime DateChange;
        public DateChangeSelectorModel() : base() { }
        public DateChangeSelectorModel(Guid id, DateTime dateChange) : base(id) {
            DateChange = dateChange;
        }
    }
}
