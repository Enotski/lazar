using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.SelectorModels.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public class NameSelectorModel : ChangedBySelectorModel {
        /// <summary>
        /// Наименование
        /// </summary>
        public readonly string Name;
        public NameSelectorModel() : base() { }
        public NameSelectorModel(Guid id, string name, string changedBy, DateTime dateChange)
            : base(id, changedBy, dateChange) {
            Name = name;
        }
    }
}
