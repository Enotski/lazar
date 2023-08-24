using CommonUtils.Utils;
using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Core.SelectorModels.Base;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Administration {
    /// <summary>
    /// Base repository for entity with login property
    /// </summary>
    /// <typeparam name="TEntity">Type witch implement IKey and ILogin</typeparam>
    public abstract class LoginRepository<TEntity> : BaseRepository<TEntity>, ILoginRepository<TEntity> where TEntity : class, IKey, ILogin {
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="dbContext">Ef context</param>
        public LoginRepository(LazarContext dbContext) : base(dbContext) { }
        /// <summary>
        /// Get login property of entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Login</returns>
        public async Task<string> GetLoginByKeyAsync(Guid id) {
            try {
                return await _dbContext.Set<TEntity>().Where(m => m.Id == id).Select(m => m.Login).FirstOrDefaultAsync();
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
                return await _dbContext.Set<TEntity>().Where(m => m.Login.ToUpper() == login).Select(m => m.Id).FirstOrDefaultAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Get list of key-login models
        /// </summary>
        /// <param name="term">Search term by login property</param>
        /// <param name="paginationOption">Pagination</param>
        /// <returns>List of key-login models</returns>
        public async Task<IReadOnlyList<KeyNameSelectorModel>> GetKeyLoginRecordsAsync(string term, IPaginatedOption? paginationOption) {
            try {
                var predicate = PredicateBuilder.True<TEntity>();
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
                var predicate = PredicateBuilder.True<TEntity>();
                predicate = predicate.And(m => !string.IsNullOrEmpty(m.Login) && m.Login.Trim().ToUpper().Contains(login));
                if (id.HasValue) {
                    predicate = predicate.And(m => m.Id != id);
                }
                var entityId = await BuildQuery(predicate).Select(x => x.Id).FirstOrDefaultAsync();
                return entityId != Guid.Empty;
            } catch {
                throw;
            }
        }

    }
}
