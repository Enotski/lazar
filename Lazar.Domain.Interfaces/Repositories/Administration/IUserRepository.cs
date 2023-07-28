using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Administration;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;

namespace Lazar.Domain.Interfaces.Repositories.Administration {
    public interface IUserRepository : INameRepository<User> {
        /// <summary>
        /// Разрешения на выполнение операции
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<bool> PermissionToPerformOperation(string login);
        /// <summary>
        /// Возвращает количество записей в соответствии с параметрами поиска
        /// </summary>
        /// <param name="options">Фильтрация</param> 
        /// <returns></returns>
        Task<int> CountAsync(IEnumerable<ISearchOption> options);
        Task<User> GetUserAsync(Guid id);
        /// <summary>
        /// Возвращает список записей в соответсии с параметрами поиска и сортировки
        /// </summary>
        /// <param name="searchOptions">Фильтрация</param>
        /// <param name="sortOptions">Сортировка</param> 
        /// <param name="paginationOption">Пагинация</param> 
        /// <returns></returns>
        Task<IReadOnlyList<UserSelectorModel>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption);
        /// <summary>
        /// Возвращает список записей
        /// </summary>
        /// <param name="ids">список ключей</param>
        /// <returns></returns>
        Task<IReadOnlyList<UserSelectorModel>> GetRecordsAsync(IEnumerable<Guid> ids);
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
