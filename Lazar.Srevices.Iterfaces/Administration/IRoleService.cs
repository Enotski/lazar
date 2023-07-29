using Lazar.Services.Contracts.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Base;

namespace Lazar.Srevices.Iterfaces.Administration {
    public interface IRoleService : IBaseService {
        /// <summary>
        /// Возвращает список записей
        /// </summary>
        /// <param name="options">Параметры фильтрации и поиска</param>
        /// <returns></returns>
        Task<DataTableDto<RoleDto>> GetAsync(DataTableRequestDto options);
        /// <summary>
        /// Возвращает запись
        /// </summary>
        /// <param name="id">Ключ записи</param>
        /// <returns></returns>
        Task<EntityResponseDto<RoleDto>> GetAsync(Guid id);
        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="model">Dto модель</param>
        /// <param name="login">Логин пользователя, инициирующего событие</param>
        /// <returns></returns>
        Task CreateAsync(RoleDto model, string login);
        /// <summary>
        /// Изменение записи
        /// </summary>
        /// <param name="model">Dto модель</param>
        /// <param name="login">Логин пользователя, инициирующего событие</param>
        /// <returns></returns>
        Task UpdateAsync(RoleDto model, string login);
        Task<EntityResponseDto<IReadOnlyList<ListItemDto<Guid>>>> GetListAsync(KeyNameRequestDto options);
    }
}
