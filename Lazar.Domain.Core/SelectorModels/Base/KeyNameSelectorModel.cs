using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.SelectorModels.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public sealed class KeyNameSelectorModel : BaseSelectorModel {
        /// <summary>
        /// Наименование
        /// </summary>
        public readonly string Name;
        public KeyNameSelectorModel(Guid id, string name)
            : base(id) {
            Name = name;
        }
    }
}
