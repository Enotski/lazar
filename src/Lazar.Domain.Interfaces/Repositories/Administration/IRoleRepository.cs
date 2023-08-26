using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;

namespace Lazar.Domain.Interfaces.Repositories.Administration
{
    public interface IRoleRepository : INameRepository<Role> {
        /// <summary>
        /// Returns the number of records according to the search parameters
        /// </summary>
        /// <param name="options">Filtration</param> 
        /// <returns>Number of records</returns>
        Task<int> CountAsync(IEnumerable<ISearchOption> options);
        /// <summary>
        /// Returns a list of entities according to the search and sort options
        /// </summary>
        /// <param name="searchOptions">Filtration</param>
        /// <param name="sortOptions">Sorting</param> 
        /// <param name="paginationOption">Pagination</param> 
        /// <returns>List of entities</returns>
        Task<IReadOnlyList<Role>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption);
        /// <summary>
        /// Returns a list of entities according to the search and sort options
        /// </summary>
        /// <param name="searchOptions">Filtration</param>
        /// <param name="sortOptions">Sorting</param> 
        /// <param name="paginationOption">Pagination</param> 
        /// <param name="userId">Primary key of user</param>
        /// <returns>List of entities</returns>
        Task<IReadOnlyList<Role>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, Guid? userId = null);
        /// <summary>
        /// Return records by keys
        /// </summary>
        /// <param name="ids">List of Keys</param>
        /// <returns>List of entities</returns>
        Task<IReadOnlyList<Role>> GetRecordsAsync(IEnumerable<Guid> ids);
        /// <summary>
        /// Returns a list of unique values ​​in a specific column
        /// </summary>
        /// <param name="searchOptions">Filtration</param>
        /// <param name="sortOptions">Sorting</param> 
        /// <param name="paginationOption">Pagination</param> 
        /// <param name="columnSelector">Name of specific column</param> 
        /// <returns>List of entities specific property values</returns>
        Task<IReadOnlyList<string>> GetRecordsBySelectorAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, string columnSelector);
        /// <summary>
        /// Get list of key-name models of user's roles
        /// </summary>
        /// <param name="term">Search term by login property</param>
        /// <param name="userId">Primary key of user</param>
        /// <param name="paginationOption">Pagination</param>
        /// <returns>List of roles</returns>
        Task<IReadOnlyList<Role>> GetRecordsByUserAsync(string term, Guid? userId = null, IPaginatedOption? paginationOption = null);
    }
}
