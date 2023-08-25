namespace Lazar.Domain.Core.SelectorModels.Base {
    /// <summary>
    /// Named model
    /// </summary>
    public class NameSelectorModel : ChangedBySelectorModel {
        public readonly string Name;
        public NameSelectorModel() : base() { }
        public NameSelectorModel(Guid id, string name, string changedBy, DateTime dateChange)
            : base(id, changedBy, dateChange) {
            Name = name;
        }
    }
}
