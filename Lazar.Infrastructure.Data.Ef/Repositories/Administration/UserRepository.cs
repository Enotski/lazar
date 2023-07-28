using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Administration;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Administration {
    public class UserRepository : NameRepository<User>, IUserRepository {
        public UserRepository(LazarContext context) : base(context) {
        }

        #region private
        /// <summary>
        /// Задает дополнительные условия для выборки
        /// </summary>
        /// <param name="options">Параметры выборки</param> 
        /// <returns></returns>
        private Expression<Func<User, bool>> BuildWherePredicate(IEnumerable<ISearchOption> options) {
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
                    case "ROLES": {
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
        /// Используется для сортировки результирующего набора в порядке возрастания или убывания
        /// </summary>
        /// <param name="options">Параметры сортировки</param> 
        /// <returns></returns>
        private Func<IQueryable<User>, IOrderedQueryable<User>> BuildSortFunction(IEnumerable<ISortOption> options) {
            if (options is null || !options.Any()) {
                return null;
            }
            //TODO: переписать .OrderBy(x => true) делает плохой SQL
            var ordered = _dbContext.Users.OrderBy(x => true);
            foreach (var opt in options) {
                var column = opt.ColumnName.TrimToUpper();
                switch (column) {
                    case "NAME": {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.Name) : ordered.ThenByDescending(m => m.Name);
                            break;
                        }
                    case "LOGIN": {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.Login) : ordered.ThenByDescending(m => m.Login);
                            break;
                        }
                    case "EMAIL": {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.Email) : ordered.ThenByDescending(m => m.Email);
                            break;
                        }
                    case "CHANGEDBY": {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.ChangedBy) : ordered.ThenByDescending(m => m.ChangedBy);
                            break;
                        }
                    default: {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.DateChange) : ordered.ThenByDescending(m => m.DateChange);
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
        private Expression<Func<User, string>> BuildSelectorPredicate(string columnSelector) {
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
        #endregion

        public async Task<IReadOnlyList<UserSelectorModel>> GetRecordsAsync(IEnumerable<Guid> ids) {
            return await BuildQuery(x => ids.Contains(x.Id)).Select(x => new UserSelectorModel(x.Id, x.Roles.Select(r => r.Name), x.Roles.Select(r => r.Id), x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).ToListAsync();
        }
        public async Task<User> GetUserAsync(Guid id) {
            var predicates = new[] { PredicateBuilder.Create<User>(x => x.Roles) };
            return await GetAsync(id, predicates);
        }
        public async Task<int> CountAsync(IEnumerable<ISearchOption> options) {
            var filter = BuildWherePredicate(options);
            return await CountAsync(filter);
        }
        public async Task<IReadOnlyList<UserSelectorModel>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption) {
            var filter = BuildWherePredicate(searchOptions);
            var ordered = BuildSortFunction(sortOptions);
            return await BuildQuery(filter, ordered, paginationOption).Select(x => new UserSelectorModel(x.Id, x.Roles.Select(r => r.Name), x.Roles.Select(r => r.Id), x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).ToListAsync();
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
        public async Task<bool> PermissionToPerformOperation(string login) {
            try {
                return true;
            } catch (Exception ex) {
                throw;
            }
        }
    }
}