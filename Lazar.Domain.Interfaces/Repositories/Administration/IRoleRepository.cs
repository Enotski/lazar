using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;

namespace Lazar.Domain.Interfaces.Repositories.Administration {
    public interface IRoleRepository : INameRepository<Role> {
        /// <summary>
        /// Возвращает количество записей в соответствии с параметрами поиска
        /// </summary>
        /// <param name="options">Фильтрация</param> 
        /// <returns></returns>
        Task<int> CountAsync(IEnumerable<ISearchOption> options);
        /// <summary>
        /// Возвращает список записей в соответсии с параметрами поиска и сортировки
        /// </summary>
        /// <param name="searchOptions">Фильтрация</param>
        /// <param name="sortOptions">Сортировка</param> 
        /// <param name="paginationOption">Пагинация</param> 
        /// <returns></returns>
        Task<IReadOnlyList<Role>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption);
        /// <summary>
        /// Возвращает список записей
        /// </summary>
        /// <param name="ids">список ключей</param>
        /// <returns></returns>
        Task<IReadOnlyList<Role>> GetRecordsAsync(IEnumerable<Guid> ids);
        /// <summary>
        /// Возвращает список уникальных значений в определенной колонке
        /// </summary>
        /// <param name="searchOptions">Фильтрация</param>
        /// <param name="sortOptions">Сортировка</param> 
        /// <param name="paginationOption">Пагинация</param> 
        /// <param name="columnSelector"></param> 
        /// <returns></returns>
        Task<IReadOnlyList<string>> GetRecordsBySelectorAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, string columnSelector);
    }
}
