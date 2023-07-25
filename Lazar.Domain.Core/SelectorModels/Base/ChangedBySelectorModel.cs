using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Core.SelectorModels.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class ChangedBySelectorModel : DateChangeSelectorModel {
        /// <summary>
        /// Пользователь изменивший запись
        /// </summary>
        public readonly string ChangedBy;
        public ChangedBySelectorModel() : base() { }
        public ChangedBySelectorModel(Guid id, string changedBy, DateTime dateChange) : base(id, dateChange) {
            ChangedBy = changedBy;
        }
    }
}
