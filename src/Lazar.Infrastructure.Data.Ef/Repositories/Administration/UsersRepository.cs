﻿using CommonUtils.Utils;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Administration;
using Lazar.Domain.Core.SelectorModels.Base;
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
    public class UsersRepository : NameRepository<User>, IUsersRepository {
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="dbContext">Ef context</param>
        public UsersRepository(LazarContext context) : base(context) { }

        #region private
        /// <summary>
        /// Specifies additional conditions for the selection
        /// </summary>
        /// <param name="options">Selection options</param> 
        /// <returns>Conditional expression</returns>
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
        /// Sort selection
        /// </summary>
        /// <param name="options">Sorting options</param> 
        /// <returns>Sort predicate</returns>
        private Func<IQueryable<User>, IOrderedQueryable<User>> BuildSortFunction(IEnumerable<ISortOption> options) {
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
        public async Task<IEnumerable<UserSelectorModel>> GetRecordsAsync(IEnumerable<Guid> ids) {
            try {
                return await BuildQuery(x => ids.Contains(x.Id), null, null).Select(x => new UserSelectorModel(x.Id, x.Roles.Select(r => r.Name), x.Roles.Select(r => r.Id), x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).ToListAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Return record by key
        /// </summary>
        /// <param name="id">Key of record</param>
        /// <returns>Entity selector model</returns>
        public async Task<UserSelectorModel> GetRecordAsync(Guid? id) {
            try {
                return await BuildQuery(x => x.Id == id, null, null)
                    .Select(x => new UserSelectorModel(x.Id, x.Roles.Select(r => r.Name), x.Roles.Select(r => r.Id), x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).FirstOrDefaultAsync();
            } catch {
                throw;
            }
        }
        public async Task<User> GetWithRolesAsync(Guid? id) {
            try {
                return await BuildQuery(x => x.Id == id, null, null, x => x.Roles).FirstOrDefaultAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Return user by login
        /// </summary>
        /// <param name="login">Login of user</param>
        /// <returns>Entity selector model</returns>
        public async Task<UserSelectorModel> GetByLoginAsync(string login) {
            try {
                if (string.IsNullOrEmpty(login)) {
                    return null;
                }
                var entity = await _dbContext.Users.Include(x => x.Roles).FirstOrDefaultAsync(m => m.Login == login);
                return new UserSelectorModel(entity.Id, entity.Roles.Select(r => r.Name), entity.Roles.Select(r => r.Id), entity.Name, entity.Login, entity.Password, entity.Email, entity.ChangedBy, entity.DateChange);
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Returns the number of records according to the search parameters
        /// </summary>
        /// <param name="options">Filtration</param> 
        /// <returns>Number of entities</returns>
        public async Task<int> CountAsync(IEnumerable<ISearchOption> options) {
            try {
                var filter = BuildWherePredicate(options);
                return await CountAsync(filter);
            } catch { throw; }
        }
        /// <summary>
        /// Returns a list of records according to the search and sort options
        /// </summary>
        /// <param name="searchOptions">Filtration</param>
        /// <param name="sortOptions">Sorting</param> 
        /// <param name="paginationOption">Pagination</param> 
        /// <returns>Entity selector model</returns>
        public async Task<IEnumerable<UserSelectorModel>> GetRecordsAsync(IEnumerable<ISearchOption> searchOptions, IEnumerable<ISortOption> sortOptions, IPaginatedOption paginationOption) {
            try {
                var filter = BuildWherePredicate(searchOptions);
                var ordered = BuildSortFunction(sortOptions);
                return await BuildQuery(filter, ordered, paginationOption).Select(x => new UserSelectorModel(x.Id, x.Roles.Select(r => r.Name), x.Roles.Select(r => r.Id), x.Name, x.Login, x.Password, x.Email, x.ChangedBy, x.DateChange)).ToListAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Operation Permissions
        /// </summary>
        /// <param name="login">Login of user</param>
        /// <returns>Permission value</returns>
        public async Task<bool> PermissionToPerformOperation(string login) {
            try {
                return true;
            } catch {
                throw;
            }
        }
        // <summary>
        /// Get login property of entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Login</returns>
        public async Task<string> GetLoginByKeyAsync(Guid id) {
            try {
                return await _dbContext.Users.Where(m => m.Id == id).Select(m => m.Login).FirstOrDefaultAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Return primary key of entity by login property
        /// </summary>
        /// <param name="login">Login property</param>
        /// <returns>Primary key</returns>
        public async Task<Guid> GetKeyByLoginAsync(string login) {
            try {
                if (string.IsNullOrEmpty(login)) {
                    return Guid.Empty;
                }
                login = login.Trim().ToUpper();
                return await _dbContext.Users.Where(m => m.Login.ToUpper() == login).Select(m => m.Id).FirstOrDefaultAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Get list of key-login models
        /// </summary>
        /// <param name="term">Search term by login property</param>
        /// <param name="paginationOption">Pagination</param>
        /// <returns>List of key-login models</returns>
        public async Task<IEnumerable<KeyNameSelectorModel>> GetKeyLoginRecordsAsync(string term, IPaginatedOption? paginationOption) {
            try {
                var predicate = PredicateBuilder.True<User>();
                if (!string.IsNullOrWhiteSpace(term)) {
                    term = term.Trim().ToUpper();
                    predicate = predicate.And(m => !string.IsNullOrEmpty(m.Login) && m.Login.ToUpper().Contains(term));
                }
                return await BuildQuery(predicate, m => m.OrderBy(m => m.Login), paginationOption)
                    .Select(m => new KeyNameSelectorModel(m.Id, m.Login)).ToListAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Check login existance
        /// </summary>
        /// <param name="login">Login property</param>
        /// <param name="id">Primary key</param>
        /// <returns>Existance value</returns>
        public async Task<bool> LoginExistsAsync(string login, Guid? id) {
            try {
                if (string.IsNullOrWhiteSpace(login)) {
                    return true;
                }
                login = login.TrimToUpper();
                var entityId = await _dbContext.Users.Where(m => m.Login.Trim().ToUpper() == login).Select(x => x.Id).FirstOrDefaultAsync();
                return entityId != Guid.Empty;
            } catch {
                throw;
            }
        }
    }
}