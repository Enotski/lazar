using CommonUtils.Utils;
using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Core.SelectorModels.Base;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;
using Lazar.Infrastructure.Data.Ef.Context;
using Microsoft.EntityFrameworkCore;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Base {
    /// <summary>
    /// Repository of named entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class NameRepository<TEntity> : BaseRepository<TEntity>, INameRepository<TEntity> where TEntity : class, IKey, IName {
        public NameRepository(LazarContext dbContext) : base(dbContext) { }
        /// <summary>
        /// Get name property of entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Name</returns>
        public async Task<string> GetNameByIdAsync(Guid id) {
            try {
                return await _dbContext.Set<TEntity>().Where(m => m.Id == id).Select(m => m.Name).FirstOrDefaultAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Get primary key of entity by name property
        /// </summary>
        /// <param name="name">Name property</param>
        /// <returns>Primary key</returns>
        public async Task<Guid> GetKeyByNameAsync(string name) {
            try {
                if (string.IsNullOrEmpty(name)) {
                    return Guid.Empty;
                }
                name = name.Trim().ToUpper();
                return await _dbContext.Set<TEntity>().Where(m => m.Name.ToUpper() == name).Select(m => m.Id).FirstOrDefaultAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Get list of key-name models
        /// </summary>
        /// <param name="term">Search term by login property</param>
        /// <param name="paginationOption">Pagination</param>
        /// <returns>List of keys-names of models</returns>
        public async Task<IEnumerable<KeyNameSelectorModel>> GetKeyNameRecordsAsync(string term, IPaginatedOption paginationOption) {
            try {
                var predicate = PredicateBuilder.True<TEntity>();
                if (!string.IsNullOrWhiteSpace(term)) {
                    term = term.Trim().ToUpper();
                    predicate = predicate.And(m => !string.IsNullOrEmpty(m.Name) && m.Name.ToUpper().Contains(term));
                }
                return await BuildQuery(predicate, m => m.OrderBy(m => m.Name), paginationOption)
                    .Select(m => new KeyNameSelectorModel(m.Id, m.Name)).ToListAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Check name existance
        /// </summary>
        /// <param name="name">Name property</param>
        /// <param name="id">Primary key</param>
        /// <returns>Existance value</returns>
        public async Task<bool> NameExistsAsync(string name, Guid? id) {
            try {
                if (string.IsNullOrWhiteSpace(name)) {
                    return true;
                }
                name = name.TrimToUpper();
                var predicate = PredicateBuilder.True<TEntity>();
                predicate = predicate.And(m => !string.IsNullOrEmpty(m.Name) && m.Name.Trim().ToUpper().Contains(name));
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
