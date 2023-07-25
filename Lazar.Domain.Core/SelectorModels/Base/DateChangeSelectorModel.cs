using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.SelectorModels.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class DateChangeSelectorModel : BaseSelectorModel {
        /// <summary>
        /// Дата изменения записи
        /// </summary>
        public readonly DateTime DateChange;
        public DateChangeSelectorModel() : base() { }
        public DateChangeSelectorModel(Guid id, DateTime dateChange) : base(id) {
            DateChange = dateChange;
        }
    }
}
