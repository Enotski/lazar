namespace Lazar.Domain.Core.SelectorModels.Base {
    /// <summary>
    /// Key-Name model
    /// </summary>
    public sealed class KeyNameSelectorModel : BaseSelectorModel {
        public readonly string Name;
        public KeyNameSelectorModel(Guid id, string name)
            : base(id) {
            Name = name;
        }
    }
}
