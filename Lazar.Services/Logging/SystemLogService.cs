﻿using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Infrastructure.Mapper;
using Lazar.Services.Contracts.Logging;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Logging;

namespace Lazar.Services.Logging {
    /// <summary>
    /// Service of system events logging
    /// </summary>
    public class Loggingervice : ILoggingervice
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IModelMapper _mapper;

        public Loggingervice(IRepositoryManager repositoryManager, IModelMapper mapper) {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// System event records for presentation layer
        /// </summary>
        /// <param name="options">Параметры фильтрации и поиска</param>
        /// <returns></returns>
        public async Task<DataTableDto<SystemLogDto>> GetRecordsAsync(DataTableRequestDto options) {
            try {
                int totalRecords = await _repositoryManager.SystemLogRepository.CountAsync(options.Filters);
                var records = await _repositoryManager.SystemLogRepository.GetRecordsAsync(options.Filters, options.Sorts, options.Pagination);
                return new DataTableDto<SystemLogDto>(totalRecords,
                    _mapper.Mapper.Map<IReadOnlyList<SystemLogDto>>(records));
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Logging, EventType.Read, "Получение списка записей журнала системных событий: " + exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Clear all log in storage
        /// </summary>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        public async Task ClearLogAsync(string login) {
            try {
                bool IsHaveRight = await _repositoryManager.UserRepository.PermissionToPerformOperation(login);
                if (!IsHaveRight) {
                    throw new Exception("У вас недостаточно прав для выполнения данной операции");
                }
                await _repositoryManager.SystemLogRepository.ClearAsync();
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Logging, EventType.Delete, "Очистка журнала системных событий: " + exp.Format(), login);
                throw;
            }
        }
        /// <summary>
        /// Remove all entities by days 
        /// </summary>
        /// <param name="days">The number of days before the current date after which records are deleted</param>
        /// <returns></returns>
        public async Task ClearLogAsync(int days) {
            try {
                await _repositoryManager.SystemLogRepository.ClearAsync(days);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Logging, EventType.Delete, "Очистка журнала системных событий: " + exp.Format());
                throw;
            }
        }
        /// <summary>
        /// Remove records
        /// </summary>
        /// <param name="ids">Primary keys</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        public async Task DeleteRecordsAsync(IEnumerable<Guid> ids, string login) {
            try {
                if (!ids.Any()) {
                    throw new Exception("Полученный список ключей пуст");
                }
                bool IsHaveRight = await _repositoryManager.UserRepository.PermissionToPerformOperation(login);
                if (!IsHaveRight) {
                    throw new Exception("У вас недостаточно прав для выполнения данной операции");
                }
                await _repositoryManager.SystemLogRepository.DeleteAsync(ids);
            } catch (Exception exp) {
                await _repositoryManager.SystemLogRepository.AddAsync(SubSystemType.Logging, EventType.Delete, "Удаление записей журнала системных событий: " + exp.Format(), login);
                throw;
            }
        }
    }
}
