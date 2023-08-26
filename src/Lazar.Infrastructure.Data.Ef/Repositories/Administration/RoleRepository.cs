using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Administration {
    /// <summary>
    /// Repository of role
    /// </summary>
    public class RoleRepository : NameRepository<Role>, IRoleRepository {
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="dbContext">Ef context</param>
        public RoleRepository(LazarContext dbContext) : base(dbContext) { }
        #region private
        /// <summary>
        /// Specifies additional conditions for the selection
        /// </summary>
        /// <param name="options">Selection options</param> 
        /// <returns>Conditional expression</returns>
        private Expression<Func<Role, bool>> BuildWherePredicate(IEnumerable<ISearchOption> options) {
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
        private Func<IQueryable<Role>, IOrderedQueryable<Role>> BuildSortFunction(IEnumerable<ISortOption> options) {
            if (options is null || !options.Any()) {
                return null;
            }
            //TODO: переписать .OrderBy(x => true) делает плохой SQL
            var ordered = _dbContext.Roles.OrderBy(x => true);
            foreach (var opt in options) {
                var column = opt.ColumnName.TrimToUpper();
                switch (column) {
                    case "NAME": {
                            ordered = opt.Type == SortType.Ascending ? ordered.ThenBy(m => m.Name) : ordered.ThenByDescending(m => m.Name);
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
        /// Generates a predicate of the returned data
        /// </summary>
        /// <param name="columnSelector">Property name</param>
        /// <returns>Selector predicate</returns>
        private Expression<Func<Role, string>> BuildSelectorPredicate(string columnSelector) {
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
        #endregion
        /// <summary>
        /// Return records by keys
        /// </summary>
        /// <param name="ids">List of Keys</param>
        /// <returns>List of entities</returns>
        public async Task<IReadOnlyList<Role>> GetRecordsAsync(IEnumerable<Guid> ids) {
            return await BuildQuery(x => ids.Contains(x.Id)).ToListAsync();
        }
        /// <summary>
        /// Returns the number of records according to the search parameters
        /// </summary>
        /// <param name="options">Filtration</param> 
        /// <returns>Count of entities</returns>
        public async Task<int> CountAsync(IEnumerable<ISearchOption> options) {
            try {
                var filter = BuildWherePredicate(options);
                return await CountAsync(filter);
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Returns a list of entities according to the search and sort options
        /// </summary>
        /// <param name="searchOptions">Filtration</param>
        /// <param name="sortOptions">Sorting</param> 
        /// <param name="paginationOption">Pagination</param> 
        /// <returns>List of entities</returns>
        public async Task<IReadOnlyList<Role>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption) {
            try {
                var filter = BuildWherePredicate(searchOptions);
                var ordered = BuildSortFunction(sortOptions);
                return await BuildQuery(filter, ordered, paginationOption).ToListAsync();
            } catch {
                throw;
            }
        }
        public async Task<IReadOnlyList<Role>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption, Guid? userId = null) {
            try {
                var filter = BuildWherePredicate(searchOptions);
                var ordered = BuildSortFunction(sortOptions);
                if (userId.HasValue)
                    filter = filter.And(x => x.Users.Select(u => u.Id).Contains(userId.Value));

                return await BuildQuery(filter, ordered, paginationOption).ToListAsync();
            } catch {
                throw;
            }
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
        /// <summary>
        /// Get list of key-name models of user's roles
        /// </summary>
        /// <param name="term">Search term by login property</param>
        /// <param name="userId">Primary key of user</param>
        /// <param name="paginationOption">Pagination</param>
        /// <returns>List of roles</returns>
        public async Task<IReadOnlyList<Role>> GetRecordsByUserAsync(string term, Guid? userId = null, IPaginatedOption? paginationOption = null) {
            try {
                var predicate = PredicateBuilder.True<Role>();
                if (userId.HasValue)
                    predicate = predicate.And(x => x.Users.Select(u => u.Id).Contains(userId.Value));
                if (!string.IsNullOrWhiteSpace(term)) {
                    term = term.Trim().ToUpper();
                    predicate = predicate.And(m => !string.IsNullOrEmpty(m.Name) && m.Name.ToUpper().Contains(term));
                }
                return await BuildQuery(predicate, m => m.OrderBy(m => m.Name), paginationOption).ToListAsync();
            } catch { throw; }
        }
        //public static void FilterData(ref IEnumerable<Role> source, IEnumerable<DataGridFilter> filters)
        //{
        //    if (filters == null || !filters.Any() || source == null) return;
        //    foreach (var filter in filters)
        //    {
        //        filter.Value = filter.Value.Trim().ToLower();
        //        switch (filter.ColumnName)
        //        {
        //            case "Name":
        //                {
        //                    switch (filter.Type)
        //                    {
        //                        case FilterType.Contains:
        //                            {
        //                                source = source.Where(x => x.Name.ToLower().Contains(filter.Value));
        //                                break;
        //                            }

        //                        case FilterType.NotContains:
        //                            {
        //                                source = source.Where(x => !x.Name.ToLower().Contains(filter.Value));
        //                                break;
        //                            }

        //                        case FilterType.StartsWith:
        //                            {
        //                                source = source.Where(x => x.Name.ToLower().StartsWith(filter.Value));
        //                                break;
        //                            }

        //                        case FilterType.EndsWith:
        //                            {
        //                                source = source.Where(x => x.Name.ToLower().EndsWith(filter.Value));
        //                                break;
        //                            }

        //                        case FilterType.Equals:
        //                            {
        //                                source = source.Where(x => x.Name.ToLower() == filter.Value);
        //                                break;
        //                            }
        //                        case FilterType.NotEquals:
        //                            {
        //                                source = source.Where(x => x.Name.ToLower() != filter.Value);
        //                                break;
        //                            }
        //                    }
        //                    break;
        //                }
        //        }
        //    }
        //}

        //public static void SortData(ref IOrderedEnumerable<Role> source, IEnumerable<DataGridSort> sorts)
        //{
        //    if (sorts == null || !sorts.Any() || source == null) return;
        //    foreach (var sort in sorts)
        //    {
        //        switch (sort.ColumnName)
        //        {
        //            case "Name":
        //                {
        //                    source = sort.Type == SortType.Descending
        //                        ? source.ThenByDescending(x => x.Name)
        //                        : source.ThenBy(x => x.Name);
        //                    break;
        //                }
        //        }
        //    }
        //}
    }
}
