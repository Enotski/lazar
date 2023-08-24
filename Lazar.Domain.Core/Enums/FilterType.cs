namespace Lazar.Domain.Core.Enums {
    /// <summary>
    /// Type of filter predicate in select operation
    /// </summary>
    public enum FilterType {
        Contains,
        NotContains,
        StartsWith,
        EndsWith,
        Equals,
        NotEquals,
        Less,
        Greater,
        LessOrEqual,
        GreaterOrEqual,
        Between
    }
}
