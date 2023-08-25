using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Interfaces.Repositories.Base {
    public interface IBaseRepository<TEntity> where TEntity : class, IKey {
        /// <summary>
        /// Get entity by key
        /// </summary>
        /// <param name="id">Primary key of entity</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetAsync(Guid? id);
        /// <summary>
        /// Get entities by keys
        /// </summary>
        /// <param name="ids">Primary keys of entities</param>
        /// <returns>List of entities</returns>
        Task<IReadOnlyList<TEntity>> GetAsync(IEnumerable<Guid> ids);
        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity">New entity</param>
        /// <returns></returns>
        Task AddAsync(TEntity entity);
        /// <summary>
        /// Create entities
        /// </summary>
        /// <param name="entities">New entities</param>
        /// <returns></returns>
        Task AddAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Changed entity</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);
        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Changed entities</param>
        /// <returns></returns>
        Task UpdateAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// Remove entity by key
        /// </summary>
        /// <param name="id">Primary key of entity</param>
        /// <returns></returns>
        Task DeleteAsync(Guid? id);
        /// <summary>
        /// Remove entities by keys
        /// </summary>
        /// <param name="ids">Primary keys of entities</param>
        /// <returns></returns>
        Task DeleteAsync(IEnumerable<Guid> ids);
    }
}
