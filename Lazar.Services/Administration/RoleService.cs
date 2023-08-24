﻿using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Contracts.Administration;
using Lazar.Services.Contracts.Request;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Administration;

namespace Lazar.Services.Administration {
    public class RoleService : IRoleService {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IModelMapper _mapper;

        public RoleService(IRepositoryManager repositoryManager, IModelMapper mapper) {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// Возвращает список изменений
        /// </summary>
        /// <param name="newModel">Обновленная сущность</param>
        /// <param name="model">Текущая сущность</param>
        /// <returns></returns>
        private List<string> GetChangeFieldsList(Role model, Role newModel = null) {
            try {
                var res = new List<string>();

                if (newModel != null) {
                    if (newModel.Name != model.Name)
                        res.Add($"Наименование: {model.Name} -> {newModel.Name}");
                } else {
                    res.Add($"Наименование: {model.Name}");
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
        private async Task ModelValidation(RoleDto model, string login) {
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
                bool isExist = await _repositoryManager.RoleRepository.NameExistsAsync(model.Name, model.Id);
                if (isExist) {
                    throw new Exception("Запись с таким наименованием уже существует");
                }
            } catch {
                throw;
            }
        }
        public async Task<DataTableDto<RoleDto>> GetAsync(DataTableRequestDto options) {
            try {
                int totalRecords = await _repositoryManager.RoleRepository.CountAsync(options.Filters);
                var records = await _repositoryManager.RoleRepository.GetRecordsAsync(options.Filters, options.Sorts, options.Pagination);
                return new DataTableDto<RoleDto>(totalRecords, _mapper.Mapper.Map<IReadOnlyList<RoleDto>>(records));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Read, "Получение списка ролей", exp.Format());
                throw;
            }
        }
        public async Task<EntityResponseDto<RoleDto>> GetAsync(Guid id) {
            try {
                var data = await _repositoryManager.RoleRepository.GetAsync(id);
                if (data is null) {
                    throw new Exception("Запись не найдена");
                }
                return new EntityResponseDto<RoleDto>(_mapper.Mapper.Map<RoleDto>(data));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Read, "Получение роли", exp.Format());
                throw;
            }
        }
        public async Task<EntityResponseDto<IReadOnlyList<ListItemDto<Guid>>>> GetListAsync(KeyNameRequestDto options) {
            try {
                var records = await _repositoryManager.RoleRepository.GetKeyNameRecordsAsync(options.Search, options.Pagination);
                return new EntityResponseDto<IReadOnlyList<ListItemDto<Guid>>>(
                     _mapper.Mapper.Map<IReadOnlyList<ListItemDto<Guid>>>(records));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Read, "Получение списка пар ключ-наименование ролей", exp.Format());
                throw;
            }
        }
        public async Task CreateAsync(RoleDto model, string login) {
            try {
                await ModelValidation(model, login);

                var entity = new Role(model.Name, login);
                await _repositoryManager.RoleRepository.AddAsync(entity);

                var newEntity = await _repositoryManager.RoleRepository.GetAsync(entity.Id);
                var changes = GetChangeFieldsList(newEntity);

                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Create, $"{string.Join("; ", changes)}", login);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Create, "Создание роли", exp.Format());
                throw;
            }
        }
        public async Task UpdateAsync(RoleDto model, string login) {
            try {
                await ModelValidation(model, login);

                var entity = await _repositoryManager.RoleRepository.GetAsync(model.Id);
                if (entity is null) {
                    throw new Exception("Запись не найдена");
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
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Roles, EventType.Update, "Обновление роли", exp.Format());
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
