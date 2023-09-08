using CommonUtils.Utils;
using Lazar.Domain.Core.EntityModels.Logging;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Logging;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Logging {
    /// <summary>
    /// Repository of events logs in app
    /// </summary>
    internal class SystemLogRepository : LogRepository<SystemLog>, ISystemLogRepository {
        public SystemLogRepository(LazarContext dbContext) : base(dbContext) { }
        #region private
        /// <summary>
        /// Specifies additional conditions for the selection
        /// </summary>
        /// <param name="options">Selection options</param> 
        /// <returns>Conditional expression</returns>
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
                    case "SUBSYSTEMNAME": {
                            predicate = predicate.And(m => !string.IsNullOrEmpty(m.SubSystem) && m.SubSystem.ToUpper().Contains(val));
                            break;
                        }
                    case "EVENTTYPENAME": {
                            predicate = predicate.And(m => !string.IsNullOrEmpty(m.EventType) && m.EventType.ToUpper().Contains(val));
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
        /// Sort selection
        /// </summary>
        /// <param name="options">Sorting options</param> 
        /// <returns>Sort predicate</returns>
        private Func<IQueryable<SystemLog>, IOrderedQueryable<SystemLog>> BuildSortFunction(IEnumerable<ISortOption> options) {
            if (options is null || !options.Any()) {
                return null;
            }
            return query => {
                IOrderedQueryable<SystemLog> ordered = null;

                foreach (var opt in options) {
                    var column = opt.ColumnName.TrimToUpper();
                    switch (column) {
                        case "SUBSYSTEMNAME": {
                            if (ordered == null)
                                ordered = opt.Type == SortType.Ascending ? query.OrderBy(m => m.SubSystem) : query.OrderByDescending(m => m.SubSystem);
                            else
                                ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.SubSystem) : ordered.ThenByDescending(m => m.SubSystem);
                            break;
                        }
                        case "EVENTTYPENAME": {
                            if (ordered == null)
                                ordered = opt.Type == SortType.Ascending ? query.OrderBy(m => m.EventType) : query.OrderByDescending(m => m.EventType);
                            else
                                ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.EventType) : ordered.ThenByDescending(m => m.EventType);
                            break;
                        }
                        case "DESCRIPTION": {
                            if (ordered == null)
                                ordered = opt.Type == SortType.Ascending ? query.OrderBy(m => m.Description) : query.OrderByDescending(m => m.Description);
                            else
                                ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.Description) : ordered.ThenByDescending(m => m.Description);
                            break;
                        }
                        case "CHANGEDBY": {
                            if (ordered == null)
                                ordered = opt.Type == SortType.Ascending ? query.OrderBy(m => m.ChangedBy) : query.OrderByDescending(m => m.ChangedBy);
                            else
                                ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.ChangedBy) : ordered.ThenByDescending(m => m.ChangedBy);
                            break;
                        }
                        default: {
                            if (ordered == null)
                                ordered = opt.Type == SortType.Ascending ? query.OrderBy(m => m.DateChange) : query.OrderByDescending(m => m.DateChange);
                            else
                                ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.DateChange) : ordered.ThenByDescending(m => m.DateChange);
                            break;
                        }
                    }
                }

                return ordered;
            };
        }
        /// <summary>
        /// Generates a predicate of the returned data
        /// </summary>
        /// <param name="columnSelector">Property name</param>
        /// <returns>Selector predicate</returns>
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
        /// <summary>
        /// Adds an entry to the event log
        /// </summary>
        /// <param name="subSystem">Type of subsystem</param>
        /// <param name="eventType">Type of event</param>
        /// <param name="description">Description of event</param>
        /// <param name="changedBy">Initiator of event</param>
        /// <returns></returns>
        public async Task AddAsync(SubSystemType subSystem, EventType eventType, string description, string? changedBy = null) {
            try {
                await AddAsync(new SystemLog(description, subSystem, eventType, changedBy));
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Get the count of records according to the search parameters
        /// </summary>
        /// <param name="options"></param> 
        /// <returns>Count of records</returns>
        public async Task<int> CountAsync(IEnumerable<ISearchOption> options) {
            try {
                var filter = BuildWherePredicate(options);
                return await CountAsync(filter);
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Get a list of log entries according to search and sort options
        /// </summary>
        /// <param name="searchOptions">Select parameters</param>
        /// <param name="sortOptions">Sort parameters</param> 
        /// <param name="paginationOption">Pagination parameters</param> 
        /// <returns>List of entities</returns>
        public async Task<IEnumerable<SystemLog>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption) {
            try {
                var filter = BuildWherePredicate(searchOptions);
                var ordered = BuildSortFunction(sortOptions);
                return await BuildQuery(filter, ordered, paginationOption).ToListAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Get a list of unique values ​​in a specific column
        /// </summary>
        /// <param name="searchOptions">Select parameters</param>
        /// <param name="sortOptions">Sort parameters</param> 
        /// <param name="paginationOption">Pagination parameters</param> 
        /// <param name="columnSelector">Property for select</param> 
        /// <returns>List of values ​​of a properties</returns>
        public async Task<IEnumerable<string>> GetRecordsBySelectorAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, string columnSelector) {
            try {
                var selector = BuildSelectorPredicate(columnSelector);
                if (selector is null) {
                    return new List<string>();
                }
                var filter = BuildWherePredicate(searchOptions);
                var ordered = BuildSortFunction(sortOptions);
                return await BuildQuery(filter, ordered, paginationOption).Select(selector).Distinct().ToListAsync();
            } catch {
                throw;
            }
        }
    }
}
