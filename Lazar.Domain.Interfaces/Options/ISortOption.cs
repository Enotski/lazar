using Lazar.Domain.Core.Enums;

namespace Lazar.Domain.Interfaces.Options {
    /// <summary>
    /// Sort parameters
    /// </summary>
    public interface ISortOption : ISortColumn, ISortType { }
    /// <summary>
    /// Column to sort by
    /// </summary>
    public interface ISortColumn {
        string ColumnName { get; set; }
    }
    /// <summary>
    /// Sorting order
    /// </summary>
    public interface ISortType {
        SortType Type { get; set; }
    }
}
