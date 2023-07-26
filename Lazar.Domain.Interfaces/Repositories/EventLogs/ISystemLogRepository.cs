using Lazar.Domain.Core.EntityModels.EventLogs;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;

namespace Lazar.Domain.Interfaces.Repositories.EventLogs {
    public interface ISystemLogRepository : ILogRepository<SystemLog> {
        /// <summary>
        /// Добавляет запись в журнал событий
        /// </summary>
        /// <param name="subSystem"></param>
        /// <param name="eventType"></param>
        /// <param name="description"></param>
        /// <param name="message"></param>
        /// <param name="changedBy"></param>
        /// <returns></returns>
        Task AddAsync(SubSystemType subSystem, EventType eventType, string message, string description, string changedBy = null);
        /// <summary>
        /// Возвращает количество записей в соответствии с параметрами поиска
        /// </summary>
        /// <param name="options"></param> 
        /// <returns></returns>
        Task<int> CountAsync(IEnumerable<ISearchOption> options);
        /// <summary>
        /// Возвращает список записей журнала в соответсии с параметрами поиска и сортировки
        /// </summary>
        /// <param name="searchOptions"></param>
        /// <param name="sortOptions"></param> 
        /// <param name="paginationOption"></param> 
        /// <returns></returns>
        Task<IReadOnlyList<SystemLog>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption);
        /// <summary>
        /// Возвращает список уникальных значений в определенной колонке
        /// </summary>
        /// <param name="searchOptions"></param>
        /// <param name="sortOptions"></param> 
        /// <param name="paginationOption"></param> 
        /// <param name="columnSelector"></param> 
        /// <returns></returns>
        Task<IReadOnlyList<string>> GetRecordsBySelectorAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, string columnSelector);
    }
}
