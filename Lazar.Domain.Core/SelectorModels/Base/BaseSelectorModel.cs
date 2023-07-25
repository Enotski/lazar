using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.SelectorModels.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class BaseSelectorModel {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        public readonly Guid Id;
        public BaseSelectorModel() { }
        public BaseSelectorModel(Guid id) {
            Id = id;
        }
    }
}
