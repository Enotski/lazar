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
    /// <summary>
    /// Repository of user
    /// </summary>
    public class UserRepository : NameRepository<User>, IUserRepository {
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="dbContext">Ef context</param>
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
        /// <summary>
        /// Return records by keys
        /// </summary>
        /// <param name="ids">List of Keys</param>
        /// <returns>List of entities selector models</returns>
        public async Task<IReadOnlyList<UserSelectorModel>> GetRecordsAsync(IEnumerable<Guid> ids) {
            return await BuildQuery(x => ids.Contains(x.Id), null, null).Select(x => new UserSelectorModel(x.Id, x.Roles.Select(r => r.Name), x.Roles.Select(r => r.Id), x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).ToListAsync();
        }
        /// <summary>
        /// Return record by key
        /// </summary>
        /// <param name="id">Key of record</param>
        /// <returns>Entity selector model</returns>
        public async Task<UserSelectorModel> GetRecordAsync(Guid? id) {
            var entity = await GetAsync(id);
            return new UserSelectorModel(entity.Id, entity.Roles.Select(r => r.Name), entity.Roles.Select(r => r.Id), entity.Name, entity.Login, entity.Password, entity.Email, entity.ChangedBy, entity.DateChange);
        }
        /// <summary>
        /// Return user by login
        /// </summary>
        /// <param name="login">Login of user</param>
        /// <returns>Entity selector model</returns>
        public async Task<UserSelectorModel> GetByLoginAsync(string login) {
            if (string.IsNullOrEmpty(login)) {
                return null;
            }
            var entity = await _dbContext.Users.FirstOrDefaultAsync(m => m.Login == login);
            return new UserSelectorModel(entity.Id, entity.Roles.Select(r => r.Name), entity.Roles.Select(r => r.Id), entity.Name, entity.Login, entity.Password, entity.Email, entity.ChangedBy, entity.DateChange);
        }
        /// <summary>
        /// Returns the number of records according to the search parameters
        /// </summary>
        /// <param name="options">Filtration</param> 
        /// <returns>Number of entities</returns>
        public async Task<int> CountAsync(IEnumerable<ISearchOption> options) {
            var filter = BuildWherePredicate(options);
            return await CountAsync(filter);
        }
        /// <summary>
        /// Returns a list of records according to the search and sort options
        /// </summary>
        /// <param name="searchOptions">Filtration</param>
        /// <param name="sortOptions">Sorting</param> 
        /// <param name="paginationOption">Pagination</param> 
        /// <returns>Entity selector model</returns>
        public async Task<IReadOnlyList<UserSelectorModel>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption) {
            var filter = BuildWherePredicate(searchOptions);
            var ordered = BuildSortFunction(sortOptions);
            return await BuildQuery(filter, ordered, paginationOption).Select(x => new UserSelectorModel(x.Id, x.Roles.Select(r => r.Name), x.Roles.Select(r => r.Id), x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).ToListAsync();
        }
        /// <summary>
        /// Returns a list of unique values ​​in a specific column
        /// </summary>
        /// <param name="searchOptions">Filtration</param>
        /// <param name="sortOptions">Sorting</param> 
        /// <param name="paginationOption">Pagination</param> 
        /// <param name="columnSelector">Name of specific column</param> 
        /// <returns>List of entities specific property values</returns>
        public async Task<IReadOnlyList<string>> GetRecordsBySelectorAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, string columnSelector) {
            var selector = BuildSelectorPredicate(columnSelector);
            if (selector is null) {
                return new List<string>();
            }
            var filter = BuildWherePredicate(searchOptions);
            var ordered = BuildSortFunction(sortOptions);
            return await BuildQuery(filter, ordered, paginationOption).Select(selector).Distinct().ToListAsync();
        }
        /// <summary>
        /// Operation Permissions
        /// </summary>
        /// <param name="login">Login of user</param>
        /// <returns>Permission value</returns>
        public async Task<bool> PermissionToPerformOperation(string login) {
            try {
                return true;
            } catch (Exception ex) {
                throw;
            }
        }
    }
}