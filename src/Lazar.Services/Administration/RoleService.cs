using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Base;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Contracts.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Administration;

namespace Lazar.Services.Administration {
    /// <summary>
    /// Service of roles
    /// </summary>
    public class RoleService : IRoleService {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IModelMapper _mapper;

        public RoleService(IRepositoryManager repositoryManager, IModelMapper mapper) {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// Get a list of entity changes
        /// </summary>
        /// <param name="newModel">Updated entity</param>
        /// <param name="model">Current entity</param>
        /// <returns>List of entity changes</returns>
        private List<string> GetChangeFieldsList(Role model, Role newModel = null) {
            try {
                var res = new List<string>();

                if (newModel != null) {
                    if (newModel.Name != model.Name)
                        res.Add($"Name: {model.Name} -> {newModel.Name}");
                } else {
                    res.Add($"Name: {model.Name}");
                }
                return res;
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Validation of operation
        /// </summary>
        /// <param name="model">Model of record</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        private async Task ModelValidation(RoleDto model, string login) {
            try {
                // Проверка на наличие прав для редактирования
                bool IsHaveRight = await _repositoryManager.UserRepository.PermissionToPerformOperation(login);
                if (!IsHaveRight) {
                    throw new Exception("You do not have sufficient rights to perform this operation");
                }
                // Валидация наименования
                if (string.IsNullOrWhiteSpace(model.Name)) {
                    throw new Exception("Name is required");
                }
                // Запрет на создание дубликата наименования
                bool isExist = await _repositoryManager.RoleRepository.NameExistsAsync(model.Name, model.Id);
                if (isExist) {
                    throw new Exception("Dublicate name");
                }
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Records for presentation layer
        /// </summary>
        /// <param name="options">Filtering and search options</param>
        /// <returns>List of records</returns>
        public async Task<DataTableDto<RoleTableDto>> GetAsync(DataTableRequestDto options) {
            try {
                int totalRecords = await _repositoryManager.RoleRepository.CountAsync(options.Filters);
                var records = await _repositoryManager.RoleRepository.GetRecordsAsync(options.Filters, options.Sorts, options.Pagination);
                return new DataTableDto<RoleTableDto>(totalRecords, _mapper.Mapper.Map<IEnumerable<RoleTableDto>>(records).Select((x, i) => { x.Num = ++i; return x; }));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Read, "Getting a list of roles", exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Records for presentation layer
        /// </summary>
        /// <param name="options">Filtering and search options</param>
        /// <returns>List of records</returns>
        public async Task<DataTableDto<RoleTableDto>> GetByUserAsync(RoleTableRequestDto options) {
            try {
                int totalRecords = await _repositoryManager.RoleRepository.CountAsync(options.Filters);
                var records = await _repositoryManager.RoleRepository.GetRecordsAsync(options.Filters, options.Sorts, options.Pagination, Guid.TryParse(options.SelectedUserId, out Guid userId) ? userId : null);
                return new DataTableDto<RoleTableDto>(totalRecords, _mapper.Mapper.Map<IEnumerable<RoleTableDto>>(records).Select((x, i) => { x.Num = ++i; return x; }));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Read, "Getting a list of roles", exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Entity for presentation layer
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Entity dto</returns>
        public async Task<EntityResponseDto<RoleDto>> GetAsync(Guid id) {
            try {
                var data = await _repositoryManager.RoleRepository.GetAsync(id);
                if (data is null) {
                    throw new Exception("Record not found");
                }
                return new EntityResponseDto<RoleDto>(_mapper.Mapper.Map<RoleDto>(data));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Read, "Getting a role", exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Get collection of key-names
        /// </summary>
        /// <param name="options">Selection parameters</param>
        /// <returns>List of key-names</returns>
        public async Task<EntityResponseDto<IEnumerable<ListItemDto<Guid>>>> GetListAsync(KeyNameRequestDto options) {
            try {
                var records = await _repositoryManager.RoleRepository.GetKeyNameRecordsAsync(options.Search, options.Pagination);
                return new EntityResponseDto<IEnumerable<ListItemDto<Guid>>>(
                     _mapper.Mapper.Map<IEnumerable<ListItemDto<Guid>>>(records));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Read, "Getting a key-name list of roles", exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Get list of key-name models of user's roles
        /// </summary>
        /// <param name="term">Search term by login property</param>
        /// <param name="userId">Primary key of user</param>
        /// <param name="paginationOption">Pagination</param>
        /// <returns>List of keys-names of models</returns>
        public async Task<EntityResponseDto<IEnumerable<ListItemDto<Guid>>>> GetKeyNameByUserAsync(SelectRoleRequestDto options) {
            try {
                var roles = await _repositoryManager.RoleRepository.GetNotUserAsync(options.Search, Guid.TryParse(options.SelectedUserId, out Guid userId) ? userId : null, options.Pagination);
                var records = roles.Select(x => new KeyNameSelectorModel(x.Id, x.Name));

                return new EntityResponseDto<IEnumerable<ListItemDto<Guid>>>(
                     _mapper.Mapper.Map<IEnumerable<ListItemDto<Guid>>>(records));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Read, "Getting a key-name list of roles", exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="model">Dto model</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        public async Task CreateAsync(RoleDto model, string login) {
            try {
                await ModelValidation(model, login);

                var entity = new Role(model.Name, login);
                await _repositoryManager.RoleRepository.AddAsync(entity);

                var newEntity = await _repositoryManager.RoleRepository.GetAsync(entity.Id);
                var changes = GetChangeFieldsList(newEntity);

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Create, $"{string.Join("; ", changes)}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Create, "Role creating", exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Update exsisting entity
        /// </summary>
        /// <param name="model">Dto model</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        public async Task UpdateAsync(RoleDto model, string login) {
            try {
                await ModelValidation(model, login);

                var entity = await _repositoryManager.RoleRepository.GetAsync(model.Id);
                if (entity is null) {
                    throw new Exception("Record not found");
                }
                // Запись до измемнений
                var oldDc = await _repositoryManager.RoleRepository.GetAsync(model.Id);

                // Обновление записи
                entity.Update(model.Name, login);
                await _repositoryManager.RoleRepository.UpdateAsync(entity);

                // Обновленная запись
                var newDc = await _repositoryManager.RoleRepository.GetAsync(model.Id);
                var changes = GetChangeFieldsList(oldDc, newDc);

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Update, $"{string.Join("; ", changes)}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Update, "Role updating", exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Remove entities
        /// </summary>
        /// <param name="ids">Primary keys</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        public async Task DeleteAsync(IEnumerable<Guid> ids, string login) {
            try {
                if (!ids.Any()) {
                    throw new Exception("List of keys is empty");
                }
                bool isHaveRight = await _repositoryManager.UserRepository.PermissionToPerformOperation(login);
                if (!isHaveRight) {
                    throw new Exception("You do not have sufficient rights to perform this operation");
                }
                var entities = await _repositoryManager.RoleRepository.GetRecordsAsync(ids);

                await _repositoryManager.RoleRepository.DeleteAsync(ids);

                var entitiesFields = string.Join("; ", entities.Select(x => $"[{string.Join("; ", GetChangeFieldsList(x))}]"));

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Delete, $"{entitiesFields}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Delete, exp.Format(), login);
                throw;
            }
        }
    }
}
