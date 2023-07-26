using CommonUtils.Utils;
using Lazar.Domain.Core.EntityModels.EventLogs;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.EventLogs;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lazar.Infrastructure.Data.Ef.Repositories.EventLogs {
    /// <summary>
    /// Repository of events logs in app
    /// </summary>
    internal class SystemLogRepository : LogRepository<SystemLog>, ISystemLogRepository {
        public SystemLogRepository(LazarContext dbContext) : base(dbContext) { }
        #region private
        /// <summary>
        /// Задает дополнительные условия для выборки
        /// </summary>
        /// <param name="options">Параметры выборки</param> 
        /// <returns></returns>
        private Expression<Func<SystemLog, bool>> BuildWherePredicate(IEnumerable<ISearchOption> options) {
            if (options is null || !options.Any()) {
                return null;
            }
            Expression<Func<SystemLog, bool>> predicate = PredicateBuilder.True<SystemLog>();
            foreach (var opt in options) {
                if (opt == null || string.IsNullOrWhiteSpace(opt.ColumnName) || string.IsNullOrWhiteSpace(opt.Value)) {
                    continue;
                }
                var val = opt.Value.Trim().ToUpper();
                var column = opt.ColumnName.Trim().ToUpper();
                switch (column) {
                    case "SUBSYSTEM": {
                            predicate = predicate.And(m => !string.IsNullOrEmpty(m.SubSystem) && m.SubSystem.ToUpper().Contains(val));
                            break;
                        }
                    case "EVENTTYPE": {
                            predicate = predicate.And(m => !string.IsNullOrEmpty(m.EventType) && m.EventType.ToUpper().Contains(val));
                            break;
                        }
                    case "MESSAGE": {
                            predicate = predicate.And(m => !string.IsNullOrEmpty(m.Message) && m.Message.ToUpper().Contains(val));
                            break;
                        }
                    case "DESCRIPTION": {
                            predicate = predicate.And(m => !string.IsNullOrEmpty(m.Description) && m.Description.ToUpper().Contains(val));
                            break;
                        }
                    case "CHANGEDBY": {
                            predicate = predicate.And(m => !string.IsNullOrEmpty(m.ChangedBy) && m.ChangedBy.ToUpper().Contains(val));
                            break;
                        }
                    case "DATECHANGE": {
                            var interval = val.Split(';');
                            if (interval.Length == 2) {
                                predicate = predicate.WhereDateBetween(x => x.DateChange, interval[0], interval[1]);
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            return predicate;
        }
        /// <summary>
        /// Используется для сортировки результирующего набора в порядке возрастания или убывания
        /// </summary>
        /// <param name="option">Параметры сортировки</param> 
        /// <returns></returns>
        private Func<IQueryable<SystemLog>, IOrderedQueryable<SystemLog>> BuildSortFunction(IEnumerable<ISortOption> options) {
            if (options is null || !options.Any()) {
                return null;
            }
            //TODO: переписать .OrderBy(x => true) делает плохой SQL
            var ordered = _dbContext.SystemLogs.OrderBy(x => true);
            foreach (var opt in options) {
                var column = opt.ColumnName.TrimToUpper();
                switch (column) {
                    case "SUBSYSTEM": {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.SubSystem)
                                : ordered.ThenByDescending(m => m.SubSystem);
                            break;
                        }
                    case "EVENTTYPE": {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.EventType)
                                : ordered.ThenByDescending(m => m.EventType);
                            break;
                        }
                    case "MESSAGE": {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.Message)
                                : ordered.ThenByDescending(m => m.Message);
                            break;
                        }
                    case "DESCRIPTION": {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.Description)
                                : ordered.ThenByDescending(m => m.Description);
                            break;
                        }
                    case "CHANGEDBY": {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.ChangedBy)
                                : ordered.ThenByDescending(m => m.ChangedBy);
                            break;
                        }
                    default: {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.DateChange)
                                : ordered.ThenByDescending(m => m.DateChange);
                            break;
                        }
                }
            }
            return orb => ordered;
        }
        /// <summary>
        /// Формирует предикат возвращаемого набора данных
        /// </summary>
        /// <param name="columnSelector"></param>
        /// <returns></returns>
        private Expression<Func<SystemLog, string>> BuildSelectorPredicate(string columnSelector) {
            if (string.IsNullOrEmpty(columnSelector)) {
                return null;
            }
            switch (columnSelector.Trim().ToUpper()) {
                case "SUBSYSTEM": {
                        return x => x.SubSystem;
                    }
                case "EVENTTYPE": {
                        return x => x.EventType;
                    }
                case "MESSAGE": {
                        return x => x.Message;
                    }
                case "DESCRIPTION": {
                        return x => x.Description;
                    }
                case "CHANGEDBY": {
                        return x => x.ChangedBy;
                    }
            }
            return null;
        }
        #endregion
        public async Task AddAsync(SubSystemType subSystem, EventType eventType, string message, string description, string changedBy = null) {
            await AddAsync(new SystemLog(message, description, subSystem, eventType, changedBy));
        }
        public async Task<int> CountAsync(IEnumerable<ISearchOption> options) {
            var filter = BuildWherePredicate(options);
            return await CountAsync(filter);
        }
        public async Task<IReadOnlyList<SystemLog>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption) {
            var filter = BuildWherePredicate(searchOptions);
            var ordered = BuildSortFunction(sortOptions);
            return await BuildQuery(filter, ordered, paginationOption).ToListAsync();
        }
        public async Task<IReadOnlyList<string>> GetRecordsBySelectorAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, string columnSelector) {
            var selector = BuildSelectorPredicate(columnSelector);
            if (selector is null) {
                return new List<string>();
            }
            var filter = BuildWherePredicate(searchOptions);
            var ordered = BuildSortFunction(sortOptions);
            return await BuildQuery(filter, ordered, paginationOption).Select(selector).Distinct().ToListAsync();
        }
    }
}
