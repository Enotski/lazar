using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Administration;
using Lazar.Domain.Core.SelectorModels.Base;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Administration;
using Moq;
using System.Data;
using System.Linq.Expressions;

namespace Lazar.Services.Tests.Mocks {
    internal class MockIUsersRepository {
        public static Mock<IUsersRepository> GetMock() {
            var mock = new Mock<IUsersRepository>();
            // Set up
            mock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => TestData.Users.FirstOrDefault(x => x.Id == id));
            mock.Setup(x => x.GetRecordAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => TestData.Users.Select(x => new UserSelectorModel(x.Id, new string[] { "" }, new Guid[] { Guid.Empty }, x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).FirstOrDefault(x => x.Id == id));

            mock.Setup(x => x.GetByLoginAsync(It.IsAny<string>())).ReturnsAsync((string login) => TestData.Users.Select(x => new UserSelectorModel(x.Id, new string[] { "" }, new Guid[] { Guid.Empty }, x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).FirstOrDefault(x => x.Login == login));
            mock.Setup(x => x.GetAsync(It.IsAny<List<Guid>>())).ReturnsAsync((List<Guid> ids) => TestData.Users.Where(x => ids.Contains(x.Id)));

            mock.Setup(x => x.CountAsync(It.IsAny<IEnumerable<ISearchOption>>()))
                .ReturnsAsync((IEnumerable<ISearchOption> options) => {
                    var filter = BuildWherePredicate(options);
                    return BuildQuery(filter).Count();
                });

            mock.Setup(x => x.GetRecordsAsync(It.IsAny<IEnumerable<ISearchOption>>(), It.IsAny<IEnumerable<ISortOption>>(), It.IsAny<IPaginatedOption>()))
                .ReturnsAsync((IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption) => {
                    var filter = BuildWherePredicate(searchOptions);
                    var ordered = BuildSortFunction(sortOptions);
                    return BuildQuery(filter, ordered, paginationOption).Select(x => new UserSelectorModel(x.Id, new string[] {""}, new Guid[] {Guid.Empty}, x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).ToList();
                });

            mock.Setup(x => x.GetRecordsAsync(It.IsAny<IEnumerable<Guid>>()))
                .ReturnsAsync((IEnumerable<Guid> ids) => BuildQuery(x => ids.Contains(x.Id)).Select(x => new UserSelectorModel(x.Id, new string[] { "" }, new Guid[] { Guid.Empty }, x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).ToList());

            mock.Setup(x => x.AddAsync(It.IsAny<User>()))
                .Callback((User User) => TestData.Users.Add(User));

            mock.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .Callback((User upUser) => {
                    var currUser = TestData.Users.FirstOrDefault(x => x.Id == upUser.Id);
                    if (currUser == null)
                        throw new NullReferenceException("user not found");
                    currUser.Update(upUser.Name, upUser.Login, upUser.Password, upUser.Email, null, upUser.ChangedBy);
                });

            mock.Setup(x => x.DeleteAsync(It.IsAny<IEnumerable<Guid>>()))
                .Callback((IEnumerable<Guid> userIds) => { TestData.Users = TestData.Users.Where(x => !userIds.Contains(x.Id)).ToList(); });

            mock.Setup(x => x.PermissionToPerformOperation(It.IsAny<string>()))
                .ReturnsAsync((string login) => true);

            mock.Setup(x => x.LoginExistsAsync(It.IsAny<string>(), It.IsAny<Guid?> ()))
                .ReturnsAsync((string login, Guid? id) => {
                    if (string.IsNullOrWhiteSpace(login)) {
                        return true;
                    }
                    login = login.TrimToUpper();
                    var entityId = TestData.Users.Where(m => m.Login.Trim().ToUpper() == login).Select(x => x.Id).FirstOrDefault();
                    return entityId != Guid.Empty;
                });

            mock.Setup(x => x.GetKeyLoginRecordsAsync(It.IsAny<string>(), It.IsAny<IPaginatedOption?>()))
                .ReturnsAsync((string term, IPaginatedOption? paginationOption) => {
                    var predicate = PredicateBuilder.True<User>();
                    if (!string.IsNullOrWhiteSpace(term)) {
                        term = term.Trim().ToUpper();
                        predicate = predicate.And(m => !string.IsNullOrEmpty(m.Login) && m.Login.ToUpper().Contains(term));
                    }
                    return BuildQuery(predicate, m => m.OrderBy(m => m.Login), paginationOption)
                            .Select(m => new KeyNameSelectorModel(m.Id, m.Login)).ToList();
                });

            return mock;
        }
        #region private
        /// <summary>
        /// Specifies additional conditions for the selection
        /// </summary>
        /// <param name="options">Selection options</param> 
        /// <returns>Conditional expression</returns>
        private static Expression<Func<User, bool>> BuildWherePredicate(IEnumerable<ISearchOption> options) {
            if (options is null || !options.Any()) {
                return null;
            }
            Expression<Func<User, bool>> predicate = PredicateBuilder.True<User>();
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
                    case "LOGIN": {
                        predicate = predicate.And(m => m.Login.ToUpper().Contains(val));
                        break;
                    }
                    case "EMAIL": {
                        predicate = predicate.And(m => m.Email.ToUpper().Contains(val));
                        break;
                    }
                    case "CHANGEDBY": {
                        predicate = predicate.And(m => !string.IsNullOrEmpty(m.ChangedBy) && m.ChangedBy.ToUpper().Contains(val));
                        break;
                    }
                    case "UserS": {
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
        private static Func<IQueryable<User>, IOrderedQueryable<User>> BuildSortFunction(IEnumerable<ISortOption> options) {
            if (options is null || !options.Any()) {
                return null;
            }
            return query => {
                IOrderedQueryable<User> ordered = null;

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
                        case "LOGIN": {
                            if (ordered == null)
                                ordered = opt.Type == SortType.Ascending ? query.OrderBy(m => m.Login) : query.OrderByDescending(m => m.Login);
                            else
                                ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.Login) : ordered.ThenByDescending(m => m.Login);
                            break;
                        }
                        case "EMAIL": {
                            if (ordered == null)
                                ordered = opt.Type == SortType.Ascending ? query.OrderBy(m => m.Email) : query.OrderByDescending(m => m.Email);
                            else
                                ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.Email) : ordered.ThenByDescending(m => m.Email);
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
        private static Expression<Func<User, string>> BuildSelectorPredicate(string columnSelector) {
            if (string.IsNullOrEmpty(columnSelector)) {
                return null;
            }
            switch (columnSelector.TrimToUpper()) {
                case "NAME": {
                    return x => x.Name;
                }
                case "LOGIN": {
                    return x => x.Login;
                }
                case "EMAIL": {
                    return x => x.Email;
                }
                case "CHANGEDBY": {
                    return x => x.ChangedBy;
                }
            }
            return null;
        }
        protected static IQueryable<User> BuildQuery(Expression<Func<User, bool>>? filter = null,
     Func<IQueryable<User>, IOrderedQueryable<User>>? ordered = null,
     IPaginatedOption? paginated = null) {
            try {

                IQueryable<User> query = TestData.Users.AsQueryable();
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
