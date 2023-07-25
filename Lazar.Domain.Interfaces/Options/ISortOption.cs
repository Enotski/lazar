using Lazar.Domain.Core.Enums;

namespace Lazar.Domain.Interfaces.Options {
    /// <summary>
    /// Интерфейс параметров сортировки
    /// </summary>
    public interface ISortOption : ISortColumn, ISortType { }
    public interface ISortColumn {
        /// <summary>
        /// Столбец, по которому выполняется сортировка
        /// </summary>
        string ColumnName { get; set; }
    }
    public interface ISortType {
        /// <summary>
        /// Порядок сортировки
        /// </summary>
        SortType Order { get; set; }
    }
}
