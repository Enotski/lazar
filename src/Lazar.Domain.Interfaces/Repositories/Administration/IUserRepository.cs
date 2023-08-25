using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Administration;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;

namespace Lazar.Domain.Interfaces.Repositories.Administration {
    public interface IUserRepository : INameRepository<User>, ILoginRepository<User> {
        /// <summary>
        /// Operation Permissions
        /// </summary>
        /// <param name="login">Login of user</param>
        /// <returns>Permission value</returns>
        Task<bool> PermissionToPerformOperation(string login);
        /// <summary>
        /// Returns the number of records according to the search parameters
        /// </summary>
        /// <param name="options">Filtration</param> 
        /// <returns>Number of entities</returns>
        Task<int> CountAsync(IEnumerable<ISearchOption> options);
        /// <summary>
        /// Return record by key
        /// </summary>
        /// <param name="id">Key of record</param>
        /// <returns>Entity selector model</returns>
        Task<UserSelectorModel> GetRecordAsync(Guid? id);
        /// <summary>
        /// Return user by login
        /// </summary>
        /// <param name="login">Login of user</param>
        /// <returns>Entity selector model</returns>
        Task<UserSelectorModel> GetByLoginAsync(string login);
        /// <summary>
        /// Returns a list of records according to the search and sort options
        /// </summary>
        /// <param name="searchOptions">Filtration</param>
        /// <param name="sortOptions">Sorting</param> 
        /// <param name="paginationOption">Pagination</param> 
        /// <returns>Entity selector model</returns>
        Task<IReadOnlyList<UserSelectorModel>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption);
        /// <summary>
        /// Return records by keys
        /// </summary>
        /// <param name="ids">List of Keys</param>
        /// <returns>List of entities selector models</returns>
        Task<IReadOnlyList<UserSelectorModel>> GetRecordsAsync(IEnumerable<Guid> ids);
        /// <summary>
        /// Returns a list of unique values ​​in a specific column
        /// </summary>
        /// <param name="searchOptions">Filtration</param>
        /// <param name="sortOptions">Sorting</param> 
        /// <param name="paginationOption">Pagination</param> 
        /// <param name="columnSelector">Name of specific column</param> 
        /// <returns>List of entities specific property values</returns>
        Task<IReadOnlyList<string>> GetRecordsBySelectorAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, string columnSelector);
    }
}
