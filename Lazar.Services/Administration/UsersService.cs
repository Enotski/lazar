using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Administration;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Contracts.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Administration;
using System.Data;
using System.Reflection;

namespace Lazar.Services.Administration {
    public class UsersService : IUsersService {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IModelMapper _mapper;

        public UsersService(IRepositoryManager repositoryManager, IModelMapper mapper) {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

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
        public async Task<EntityResponseDto<IReadOnlyList<ListItemDto<Guid>>>> GetListAsync(KeyNameRequestDto options) {
            try {
                var records = await _repositoryManager.UserRepository.GetKeyNameRecordsAsync(options.Search, options.Pagination);
                return new EntityResponseDto<IReadOnlyList<ListItemDto<Guid>>>(
                     _mapper.Mapper.Map<IReadOnlyList<ListItemDto<Guid>>>(records));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Read, "Получение списка пар ключ-наименование пользователей", exp.Format());
                throw;
            }
        }
        public async Task CreateAsync(UserDto model, string login) {
            try {
                await ModelValidation(model, login);

                var roles = await _repositoryManager.RoleRepository.GetAsync(model.RoleIds);
                var entity = new User(model.Name, model.Login, model.Password, model.Email, login);
                entity.Roles = roles;
                await _repositoryManager.UserRepository.AddAsync(entity);

                var newEntity = await _repositoryManager.UserRepository.GetRecordAsync(entity.Id);
                var changes = GetChangeFieldsList(newEntity);

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Create, $"{string.Join("; ", changes)}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Users, EventType.Create, "Создание пользователя", exp.Format());
                throw;
            }
        }
        public async Task UpdateAsync(UserDto model, string login) {
            try {
                await ModelValidation(model, login);

                var entity = await _repositoryManager.UserRepository.GetAsync(model.Id);
                if (entity is null) {
                    throw new Exception("Запись не найдена");
                }
                // Запись до измемнений
                var oldDc = await _repositoryManager.UserRepository.GetRecordAsync(model.Id);

                // Обновление записи
                var roles = await _repositoryManager.RoleRepository.GetAsync(model.RoleIds);
                entity.Update(model.Name, model.Login, model.Password, model.Email, roles, login);
                await _repositoryManager.UserRepository.UpdateAsync(entity);

                // Обновленная запись
                var newDc = await _repositoryManager.UserRepository.GetRecordAsync(model.Id);
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
    }
}
