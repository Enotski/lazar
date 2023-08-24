using Lazar.Domain.Core.EntityModels.Logging;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;

namespace Lazar.Domain.Interfaces.Repositories.Logging {
    public interface ISystemLogRepository : ILogRepository<SystemLog> {
        /// <summary>
        /// Adds an entry to the event log
        /// </summary>
        /// <param name="subSystem">Type of subsystem</param>
        /// <param name="eventType">Type of event</param>
        /// <param name="description">Description of event</param>
        /// <param name="changedBy">Initiator of event</param>
        /// <returns></returns>
        Task AddAsync(SubSystemType subSystem, EventType eventType, string description, string? changedBy = null);
        /// <summary>
        /// Get the count of records according to the search parameters
        /// </summary>
        /// <param name="options"></param> 
        /// <returns>Count of records</returns>
        Task<int> CountAsync(IEnumerable<ISearchOption> options);
        /// <summary>
        /// Get a list of log entries according to search and sort options
        /// </summary>
        /// <param name="searchOptions">Select parameters</param>
        /// <param name="sortOptions">Sort parameters</param> 
        /// <param name="paginationOption">Pagination parameters</param> 
        /// <returns>List of entities</returns>
        Task<IReadOnlyList<SystemLog>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption);
        /// <summary>
        /// Get a list of unique values ​​in a specific column
        /// </summary>
        /// <param name="searchOptions">Select parameters</param>
        /// <param name="sortOptions">Sort parameters</param> 
        /// <param name="paginationOption">Pagination parameters</param> 
        /// <param name="columnSelector">Property for select</param> 
        /// <returns>List of values ​​of a properties</returns>
        Task<IReadOnlyList<string>> GetRecordsBySelectorAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, string columnSelector);
    }
}
