using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Administration;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Contracts.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Administration;
using System.Data;

namespace Lazar.Services.Administration {
    /// <summary>
    /// Service of users
    /// </summary>
    public class UsersService : IUsersService {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IModelMapper _mapper;

        public UsersService(IRepositoryManager repositoryManager, IModelMapper mapper) {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// Get a list of entity changes
        /// </summary>
        /// <param name="newModel">Updated entity</param>
        /// <param name="model">Current entity</param>
        /// <returns>List of entity changes</returns>
        private List<string> GetChangeFieldsList(UserSelectorModel model, UserSelectorModel newModel = null) {
            try {
                var res = new List<string>();

                if (newModel != null) {
                    if (newModel.Name != model.Name)
                        res.Add($"Наименование: {model.Name} -> {newModel.Name}");
                    if (newModel.Login != model.Login)
                        res.Add($"Логин: {model.Login} -> {newModel.Login}");
                    if (newModel.Email != model.Email)
                        res.Add($"Почта: {model.Email} -> {newModel.Email}");
                    if (newModel.RoleNames != model.RoleNames)
                        res.Add($"Роли: {model.RoleNames} -> {newModel.RoleNames}");
                } else {
                    res.Add($"Наименование: {model.Name}");
                    res.Add($"Логин: {model.Login}");
                    res.Add($"Почта: {model.Email}");
                    res.Add($"Роли: {model.RoleNames}");
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
        private async Task ModelValidation(UserDto model, string login) {
            try {
                bool IsHaveRight = await _repositoryManager.UserRepository.PermissionToPerformOperation(login);
                if (!IsHaveRight) {
                    throw new Exception("У вас недостаточно прав для выполнения данной операции");
                }
                if (string.IsNullOrWhiteSpace(model.Name)) {
                    throw new Exception("Наименование не заполнено");
                }
                bool isExist = await _repositoryManager.UserRepository.NameExistsAsync(model.Name, model.Id);
                if (isExist) {
                    throw new Exception("Запись с таким наименованием уже существует");
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
        public async Task<DataTableDto<UserTableDto>> GetAsync(DataTableRequestDto options) {
            try {
                int totalRecords = await _repositoryManager.UserRepository.CountAsync(options.Filters);
                var records = await _repositoryManager.UserRepository.GetRecordsAsync(options.Filters, options.Sorts, options.Pagination);
                return new DataTableDto<UserTableDto>(totalRecords, _mapper.Mapper.Map<IEnumerable<UserTableDto>>(records).Select((x, i) => { x.Num = ++i; return x; }));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение списка пользователей" + exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Entity for presentation layer
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Entity dto</returns>
        public async Task<EntityResponseDto<UserDto>> GetAsync(Guid id) {
            try {
                var data = await _repositoryManager.UserRepository.GetAsync(id);
                if (data is null) {
                    throw new Exception("Запись не найдена");
                }
                return new EntityResponseDto<UserDto>(_mapper.Mapper.Map<UserDto>(data));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение пользователя" + exp.Format());
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
                var records = await _repositoryManager.UserRepository.GetKeyNameRecordsAsync(options.Search, options.Pagination);
                return new EntityResponseDto<IEnumerable<ListItemDto<Guid>>>(
                     _mapper.Mapper.Map<IEnumerable<ListItemDto<Guid>>>(records));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Read, "Получение списка пар ключ-наименование пользователей" + exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="model">Dto model</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        public async Task CreateAsync(UserDto model, string login) {
            try {
                await ModelValidation(model, login);

                var roles = await _repositoryManager.RoleRepository.GetAsync(model.RoleIds);
                var entity = new User(model.Name, model.Login, model.Password, model.Email, login);
                entity.Roles = roles.ToList();
                await _repositoryManager.UserRepository.AddAsync(entity);

                var newEntity = await _repositoryManager.UserRepository.GetRecordAsync(entity.Id);
                var changes = GetChangeFieldsList(newEntity);

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Create, $"{string.Join("; ", changes)}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Create, "Создание пользователя" + exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Update exsisting entity
        /// </summary>
        /// <param name="model">Dto model</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        public async Task UpdateAsync(UserDto model, string login) {
            try {
                await ModelValidation(model, login);

                var entity = await _repositoryManager.UserRepository.GetAsync(model.Id);
                if (entity is null) {
                    throw new Exception("Запись не найдена");
                }
                var old = await _repositoryManager.UserRepository.GetRecordAsync(model.Id);

                var roles = await _repositoryManager.RoleRepository.GetAsync(model.RoleIds);
                entity.Update(model.Name, model.Login, model.Password, model.Email, roles, login);
                await _repositoryManager.UserRepository.UpdateAsync(entity);

                var newEnt = await _repositoryManager.UserRepository.GetRecordAsync(model.Id);
                var changes = GetChangeFieldsList(old, newEnt);

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Update, $"{string.Join("; ", changes)}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Update, "Обновление" + exp.Format());
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
                    throw new Exception("Полученный список ключей пуст");
                }
                bool isHaveRight = await _repositoryManager.UserRepository.PermissionToPerformOperation(login);
                if (!isHaveRight) {
                    throw new Exception("У вас недостаточно прав для выполнения данной операции");
                }

                var entities = await _repositoryManager.UserRepository.GetRecordsAsync(ids);

                await _repositoryManager.UserRepository.DeleteAsync(ids);

                var entitiesFields = string.Join("; ", entities.Select(x => $"[{string.Join("; ", GetChangeFieldsList(x))}]"));

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Delete, $"{entitiesFields}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Delete, "Удаление пользователя: " + exp.Format(), login);
                throw;
            }
        }
        /// <summary>
        /// Remove role from user
        /// </summary>
        /// <param name="model">Dto model</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        public async Task RemoveRoleFromUser(UserRoleDto model, string login) {
            try {
                var changes = await ChangetUserRoles(model, login, true);
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Update, $"{string.Join("; ", changes)}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Update, "Обновление роли" + exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Add role to user
        /// </summary>
        /// <param name="model">Dto model</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        public async Task SetRoleToUser(UserRoleDto model, string login) {
            try {
                var changes = await ChangetUserRoles(model, login);
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Update, $"{string.Join("; ", changes)}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Update, "Обновление роли" + exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Update user roles
        /// </summary>
        /// <param name="model">User-Role dto</param>
        /// <param name="initiator">Initiator of operation</param>
        /// <param name="removeRole">Flag to remove role from user</param>
        /// <returns></returns>
        private async Task<List<string>> ChangetUserRoles(UserRoleDto model, string initiator, bool removeRole = false) {
            try {
                var entity = await _repositoryManager.UserRepository.GetWithRolesAsync(model.UserId);
                if (entity is null) {
                    throw new Exception("User not found");
                }
                
                var old = await _repositoryManager.UserRepository.GetRecordAsync(model.UserId);
                var role = await _repositoryManager.RoleRepository.GetAsync(model.RoleId);
                if (removeRole) {
                    entity.Roles.Remove(role);
                } else {
                    entity.Roles.Add(role);
                }
                entity.Update(initiator);
                await _repositoryManager.UserRepository.UpdateAsync(entity);

                var newEnt = await _repositoryManager.UserRepository.GetRecordAsync(model.UserId);
                return GetChangeFieldsList(old, newEnt);
            } catch {
                throw;
            }
        }
    }
}
