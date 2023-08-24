namespace Lazar.Domain.Interfaces.Options {
    /// <summary>
    /// Pagination parameters
    /// </summary>
    public interface IPaginatedOption : ISkip, ITake { }
    /// <summary>
    /// Skip records from collection
    /// </summary>
    public interface ISkip {
        /// <summary>
        /// Skip count
        /// </summary>
        int? Skip { get; set; }
    }
    /// <summary>
    /// Take records from collection
    /// </summary>
    public interface ITake {
        /// <summary>
        /// Take count
        /// </summary>
        int? Take { get; set; }
    }
}
