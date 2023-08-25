namespace Lazar.Domain.Interfaces.Options {
    /// <summary>
    /// Select parameters
    /// </summary> 
    public interface ISearchOption {
        /// <summary>
        /// Column to which the condition applies
        /// </summary>
        string ColumnName { get; set; }
        /// <summary>
        /// Value according to which the sample is performed
        /// </summary>
        string Value { get; set; }
    }
}
