using CommonUtils.Utils;
using Lazar.Domain.Core.EntityModels.Logging;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Logging;
using Moq;
using System.Linq.Expressions;

namespace Lazar.Services.Tests.Mocks {
    internal class MockISystemLogRepository {
        public static Mock<ISystemLogRepository> GetMock() {
            var mock = new Mock<ISystemLogRepository>();
            // Set up

            mock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => TestData.Logs.FirstOrDefault(x => x.Id == id));
            mock.Setup(x => x.GetAsync(It.IsAny<List<Guid>>()))
                .ReturnsAsync((List<Guid> ids) => TestData.Logs.Where(x => ids.Contains(x.Id)));
            mock.Setup(x => x.CountAsync(It.IsAny<IEnumerable<ISearchOption>>()))
                .ReturnsAsync((IEnumerable<ISearchOption> options) => {
                    var filter = BuildWherePredicate(options);
                    return BuildQuery(filter).Count();
                });
            mock.Setup(x => x.GetRecordsAsync(It.IsAny<IEnumerable<ISearchOption>>(), It.IsAny<IEnumerable<ISortOption>>(), It.IsAny<IPaginatedOption>()))
                .ReturnsAsync((IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption) => {
                    var filter = BuildWherePredicate(searchOptions);
                    var ordered = BuildSortFunction(sortOptions);
                    return BuildQuery(filter, ordered, paginationOption).ToList();
                });
            mock.Setup(x => x.GetRecordsBySelectorAsync(It.IsAny<IEnumerable<ISearchOption>>(), It.IsAny<IEnumerable<ISortOption>>(), It.IsAny<IPaginatedOption>(), It.IsAny<string>()))
                .ReturnsAsync((IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, string columnSelector) => {
                    var selector = BuildSelectorPredicate(columnSelector);
                    if (selector is null) {
                        return new List<string>();
                    }
                    var filter = BuildWherePredicate(searchOptions);
                    var ordered = BuildSortFunction(sortOptions);
                    return BuildQuery(filter, ordered, paginationOption).Select(selector).Distinct().ToList();
                });
            mock.Setup(x => x.AddAsync(It.IsAny<SubSystemType>(), It.IsAny<EventType>(), It.IsAny<string>(), It.IsAny<string?>()))
                .Callback((SubSystemType subSystem, EventType eventType, string description, string? changedBy) =>
                    TestData.Logs.Add(new SystemLog(description, subSystem, eventType, changedBy))
                );
            mock.Setup(x => x.ClearAsync());
            mock.Setup(x => x.ClearByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()));
            return mock;
        }
        #region private
        /// <summary>
        /// Specifies additional conditions for the selection
        /// </summary>
        /// <param name="options">Selection options</param> 
        /// <returns>Conditional expression</returns>
        private static Expression<Func<SystemLog, bool>> BuildWherePredicate(IEnumerable<ISearchOption> options) {
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
        private static Func<IQueryable<SystemLog>, IOrderedQueryable<SystemLog>> BuildSortFunction(IEnumerable<ISortOption> options) {
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
        private static Expression<Func<SystemLog, string>> BuildSelectorPredicate(string columnSelector) {
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
        protected static IQueryable<SystemLog> BuildQuery(Expression<Func<SystemLog, bool>>? filter = null,
     Func<IQueryable<SystemLog>, IOrderedQueryable<SystemLog>>? ordered = null,
     IPaginatedOption? paginated = null) {
            try {

                IQueryable<SystemLog> query = TestData.Logs.AsQueryable();
                if (filter is not null) {
                    query = query.Where(filter);
                }

                if (ordered is not null) {
                    query = ordered(query);
                }

                if (paginated is not null) {
                    if (paginated.Skip >= 0) {
                        query = query.Skip(paginated.Skip.Value);
                    }

                    if (paginated.Take > 0) {
                        query = query.Take(paginated.Take.Value);
                    }
                }
                return query;
            } catch {
                throw;
            }
        }
        #endregion
    }
}
