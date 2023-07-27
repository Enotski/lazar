using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Contracts.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Administration;
using System.Data;

namespace Lazar.Services.Administration {
    public class UsersService : IUsersService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IModelMapper _mapper;

        public UsersService(IRepositoryManager repositoryManager, IModelMapper mapper) {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private List<string> GetChangeFieldsList(User model, User newModel = null) {
            try {
                var res = new List<string>();

                if (newModel != null) {
                    if (newModel.Name != model.Name)
                        res.Add($"Наименование: {model.Name} -> {newModel.Name}");
                    if (newModel.Login != model.Login)
                        res.Add($"Логин: {model.Login} -> {newModel.Login}");
                    if (newModel.Email != model.Email)
                        res.Add($"Почта: {model.Email} -> {newModel.Email}");
                } else {
                    res.Add($"Наименование: {model.Name}");
                    res.Add($"Логин: {model.Login}");
                    res.Add($"Почта: {model.Email}");
                }
                return res;
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Валидация операции редактирования
        /// </summary>
        /// <param name="model">Модель ДЦ</param>
        /// <param name="login">Пользователь, инициировавший событие</param>
        /// <returns></returns>
        private async Task ModelValidation(UserDto model, string login) {
            try {
                // Проверка на наличие прав для редактирования
                bool IsHaveRight = await _repositoryManager.UserRepository.PermissionToPerformOperation(login);
                if (!IsHaveRight) {
                    throw new Exception("У вас недостаточно прав для выполнения данной операции");
                }
                // Валидация наименования
                if (string.IsNullOrWhiteSpace(model.Name)) {
                    throw new Exception("Наименование не заполнено");
                }
                // Запрет на создание дубликата наименования
                bool isExist = await _repositoryManager.UserRepository.NameExistsAsync(model.Name, model.Id);
                if (isExist) {
                    throw new Exception("Запись с таким наименованием уже существует");
                }
            } catch {
                throw;
            }
        }
        public async Task<DataTableDto<UserDto>> GetAsync(DataTableRequestDto options) {
            try {
                int totalRecords = await _repositoryManager.UserRepository.CountAsync(options.Filters);
                var records = await _repositoryManager.UserRepository.GetRecordsAsync(options.Filters, options.Sorts, options.Pagination);
                return new DataTableDto<UserDto>(totalRecords, _mapper.Mapper.Map<IReadOnlyList<UserDto>>(records));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение списка пользователей", exp.Format());
                throw;
            }
        }
        public async Task<EntityResponseDto<UserDto>> GetAsync(Guid id) {
            try {
                var data = await _repositoryManager.UserRepository.GetAsync(id);
                if (data is null) {
                    throw new Exception("Запись не найдена");
                }
                return new EntityResponseDto<UserDto>(_mapper.Mapper.Map<UserDto>(data));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Read, "Получение пользователя", exp.Format());
                throw;
            }
        }
        public async Task CreateAsync(UserDto model, string login) {
            try {
                await ModelValidation(model, login);

                var roles = await _repositoryManager.RoleRepository.GetAsync(model.RoleIds);
                var entity = new User(model.Name, model.Login, model.Password, model.Email, roles, login);
                await _repositoryManager.UserRepository.AddAsync(entity);

                var newEntity = await _repositoryManager.UserRepository.GetAsync(entity.Id);
                var changes = GetChangeFieldsList(newEntity);

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Create, $"{string.Join("; ", changes)}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Create, "Создание пользователя", exp.Format());
                throw;
            }
        }
        public async Task EditAsync(UserDto model, string login) {
            try {
                await ModelValidation(model, login);

                var entity = await _repositoryManager.UserRepository.GetAsync(model.Id);
                if (entity is null) {
                    throw new Exception("Запись не найдена");
                }
                // Запись до измемнений
                var oldDc = await _repositoryManager.UserRepository.GetAsync(model.Id);

                // Обновление записи
                var roles = await _repositoryManager.RoleRepository.GetAsync(model.RoleIds);
                entity.Update(model.Name, model.Login, model.Password, model.Email, roles, login);
                await _repositoryManager.UserRepository.UpdateAsync(entity);

                // Обновленная запись
                var newDc = await _repositoryManager.UserRepository.GetAsync(model.Id);
                var changes = GetChangeFieldsList(oldDc, newDc);

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Update, $"{string.Join("; ", changes)}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Update, "Обновление роли", exp.Format());
                throw;
            }
        }
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
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Delete, "Удаление пользователя", exp.Format(), login);
                throw;
            }
        }

        public Task<IReadOnlyList<ListItemDto<Guid>>> GetListByUserAsync(ListRequestDto options) {
            throw new NotImplementedException();
        }

        public Task<DataTableDto<RoleDto>> GetRolesByUserAsync(RoleDataTableRequestDto options) {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ListItemDto<Guid>>> GetRolesListByUserAsync(SelectRoleRequestDto options) {
            throw new NotImplementedException();
        }

        public Task SetUserRoleAsync(UserRoleDto model, string login) {
            throw new NotImplementedException();
        }

        public Task RemoveUserRoleAsync(UserRoleDto model, string login) {
            throw new NotImplementedException();
        }

        public Task<EntityResponseDto<UserDto>> GetRecordAsync(Guid id) {
            throw new NotImplementedException();
        }
    }
}
