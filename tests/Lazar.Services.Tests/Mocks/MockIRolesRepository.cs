using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Administration;
using Moq;
using System.Linq.Expressions;

namespace Lazar.Services.Tests.Mocks {
    internal class MockIRolesRepository {
        public static Mock<IRolesRepository> GetMock() {
            var mock = new Mock<IRolesRepository>();
            // Set up

            mock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => TestData.Roles.FirstOrDefault(x => x.Id == id));
            mock.Setup(x => x.GetAsync(It.IsAny<List<Guid>>())).ReturnsAsync((List<Guid> ids) => TestData.Roles.Where(x => ids.Contains(x.Id)));

            mock.Setup(x => x.CountAsync(It.IsAny<IEnumerable<ISearchOption>>(), It.IsAny<Guid?>()))
                .ReturnsAsync((IEnumerable<ISearchOption> options, Guid? id) => {
                    var filter = BuildWherePredicate(options);
                    return BuildQuery(filter).Count();
                });
            mock.Setup(x => x.GetRecordsAsync(It.IsAny<IEnumerable<ISearchOption>>(), It.IsAny<IEnumerable<ISortOption>>(), It.IsAny<IPaginatedOption>()))
                .ReturnsAsync((IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption) => {
                    var filter = BuildWherePredicate(searchOptions);
                    var ordered = BuildSortFunction(sortOptions);
                    return BuildQuery(filter, ordered, paginationOption).ToList();
                });
            mock.Setup(x => x.GetNotUserAsync(It.IsAny<string>(), It.IsAny<Guid?>(), It.IsAny<IPaginatedOption>()))
                .ReturnsAsync((string term, Guid? userId, IPaginatedOption? paginationOption) => {
                    var predicate = PredicateBuilder.True<Role>();
                    if (userId.HasValue)
                        predicate = predicate.And(x => !x.Users.Select(u => u.Id).Contains(userId.Value));
                    if (!string.IsNullOrWhiteSpace(term)) {
                        term = term.Trim().ToUpper();
                        predicate = predicate.And(m => !string.IsNullOrEmpty(m.Name) && m.Name.ToUpper().Contains(term));
                    }
                    return BuildQuery(predicate, m => m.OrderBy(m => m.Name), paginationOption).ToList();
                });
            mock.Setup(x => x.GetRecordsAsync(It.IsAny<IEnumerable<Guid>>()))
                .ReturnsAsync((IEnumerable<Guid> ids) => BuildQuery(x => ids.Contains(x.Id)).ToList());

            mock.Setup(x => x.AddAsync(It.IsAny<Role>()))
                .Callback((Role role) =>
                    TestData.Roles.Add(role)
                );
            mock.Setup(x => x.UpdateAsync(It.IsAny<Role>()))
                .Callback(((Role upRole) => {
                    var currRole = Enumerable.FirstOrDefault<Role>(TestData.Roles, (Func<Role, bool>)(x => x.Id == upRole.Id));
                    if (currRole == null)
                        throw new NullReferenceException("role not found");
                    currRole.Update(upRole.Name, upRole.ChangedBy);
                }));
            mock.Setup(x => x.DeleteAsync(It.IsAny<IEnumerable<Guid>>()))
                .Callback((IEnumerable<Guid> rolesIds) => { TestData.Roles = TestData.Roles.Where(x => !rolesIds.Contains(x.Id)).ToList(); });

            return mock;
        }
        #region private
        /// <summary>
        /// Specifies additional conditions for the selection
        /// </summary>
        /// <param name="options">Selection options</param> 
        /// <returns>Conditional expression</returns>
        private static Expression<Func<Role, bool>> BuildWherePredicate(IEnumerable<ISearchOption> options) {
            if (options is null || !options.Any()) {
                return null;
            }
            Expression<Func<Role, bool>> predicate = PredicateBuilder.True<Role>();
            foreach (var opt in options) {
                if (opt == null || string.IsNullOrWhiteSpace(opt.ColumnName) || string.IsNullOrWhiteSpace(opt.Value)) {
                    continue;
                }
                var val = opt.Value.TrimToUpper();
                var column = opt.ColumnName.TrimToUpper();
                switch (column) {
                    case "NAME": {
                        predicate = predicate.And(m => m.Name.ToUpper().Contains(val));
                        break;
                    }
                    case "CHANGEDBY": {
                        predicate = predicate.And(m => !string.IsNullOrEmpty(m.ChangedBy) && m.ChangedBy.ToUpper().Contains(val));
                        break;
                    }
                    case "DATEOFCHANGE": {
                        var interval = val.Split(';');
                        if (interval.Length == 2) {
                            predicate = predicate.WhereDateBetween(x => x.DateChange, interval[0], interval[1]);
                        }
                        break;
                    }
                }
            }
            return predicate;
        }
        /// <summary>
        /// Sort selection
        /// </summary>
        /// <param name="options">Sorting options</param> 
        /// <returns>Sort predicate</returns>
        private static Func<IQueryable<Role>, IOrderedQueryable<Role>> BuildSortFunction(IEnumerable<ISortOption> options) {
            if (options is null || !options.Any()) {
                return null;
            }
            return query => {
                IOrderedQueryable<Role> ordered = null;

                foreach (var opt in options) {
                    var column = opt.ColumnName.TrimToUpper();
                    switch (column) {
                        case "NAME": {
                            if (ordered == null)
                                ordered = opt.Type == SortType.Ascending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name);
                            else
                                ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.Name) : ordered.ThenByDescending(m => m.Name);
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
        private static Expression<Func<Role, string>> BuildSelectorPredicate(string columnSelector) {
            if (string.IsNullOrEmpty(columnSelector)) {
                return null;
            }
            switch (columnSelector.TrimToUpper()) {
                case "NAME": {
                    return x => x.Name;
                }
                case "CHANGEDBY": {
                    return x => x.ChangedBy;
                }
            }
            return null;
        }
        protected static IQueryable<Role> BuildQuery(Expression<Func<Role, bool>>? filter = null,
     Func<IQueryable<Role>, IOrderedQueryable<Role>>? ordered = null,
     IPaginatedOption? paginated = null) {
            try {

                IQueryable<Role> query = TestData.Roles.AsQueryable();
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
