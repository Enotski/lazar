using Lazar.Services.Contracts.Administration;
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
        Task<DataTableDto<UserDto>> GetRecordsAsync(DataTableRequestDto options);
        /// <summary>
        /// Возвращает запись
        /// </summary>
        /// <param name="id">Ключ записи</param>
        /// <returns></returns>
        Task<EntityResponseDto<UserDto>> GetRecordAsync(Guid id);
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
