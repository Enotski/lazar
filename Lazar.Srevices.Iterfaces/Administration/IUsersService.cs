using Lazar.Services.Contracts.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Base;

namespace Lazar.Srevices.Iterfaces.Administration {
    public interface IUsersService : IBaseService {
        /// <summary>
        /// Возвращает список записей
        /// </summary>
        /// <param name="options">Параметры фильтрации и поиска</param>
        /// <returns></returns>
        Task<DataTableDto<UserDto>> GetAsync(DataTableRequestDto options);
        Task<IReadOnlyList<ListItemDto<Guid>>> GetListByUserAsync(ListRequestDto options);
        Task<DataTableDto<RoleDto>> GetRolesByUserAsync(RoleDataTableRequestDto options);
        Task<IReadOnlyList<ListItemDto<Guid>>> GetRolesListByUserAsync(SelectRoleRequestDto options);
        Task SetUserRoleAsync(UserRoleDto model, string login);
        Task RemoveUserRoleAsync(UserRoleDto model, string login);
        /// <summary>
        /// Возвращает запись
        /// </summary>
        /// <param name="id">Ключ записи</param>
        /// <returns></returns>
        Task<EntityResponseDto<UserDto>> GetAsync(Guid id);
        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="model">Dto модель</param>
        /// <param name="login">Логин пользователя, инициирующего событие</param>
        /// <returns></returns>
        Task CreateAsync(UserDto model, string login);
        /// <summary>
        /// Изменение записи
        /// </summary>
        /// <param name="model">Dto модель</param>
        /// <param name="login">Логин пользователя, инициирующего событие</param>
        /// <returns></returns>
        Task EditAsync(UserDto model, string login);
    }
}
