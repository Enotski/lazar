using Lazar.Services.Contracts.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Base;

namespace Lazar.Srevices.Iterfaces.Administration {
    /// <summary>
    /// Service of roles
    /// </summary>
    public interface IRoleService : IBaseService {
        /// <summary>
        /// Records for presentation layer
        /// </summary>
        /// <param name="options">Filtering and search options</param>
        /// <returns>List of records</returns>
        Task<DataTableDto<RoleTableDto>> GetAsync(DataTableRequestDto options);
        /// <summary>
        /// Records for presentation layer by user key
        /// </summary>
        /// <param name="options">Filtering and search options</param>
        /// <returns>List of records by user key</returns>
        Task<DataTableDto<RoleTableDto>> GetByUserAsync(RoleTableRequestDto options);
        /// <summary>
        /// Entity for presentation layer
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Entity dto</returns>
        Task<EntityResponseDto<RoleDto>> GetAsync(Guid id);
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="model">Dto model</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        Task CreateAsync(RoleDto model, string login);
        /// <summary>
        /// Update exsisting entity
        /// </summary>
        /// <param name="model">Dto model</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        Task UpdateAsync(RoleDto model, string login);
        /// <summary>
        /// Get collection of key-names
        /// </summary>
        /// <param name="options">Selection parameters</param>
        /// <returns>List of key-names</returns>
        Task<EntityResponseDto<IEnumerable<ListItemDto<Guid>>>> GetListAsync(KeyNameRequestDto options);
        /// <summary>
        /// Get list of key-name models of user's roles
        /// </summary>
        /// <param name="options">Selection parameters</param>
        /// <returns>List of keys-names of models</returns>
        Task<EntityResponseDto<IEnumerable<ListItemDto<Guid>>>> GetKeyNameByUserAsync(SelectRoleRequestDto options);
    }
}