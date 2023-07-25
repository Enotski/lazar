using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Interfaces.Repositories.Base {
    public interface IBaseRepository<TEntity> where TEntity : class, IKey {
        Task<TEntity> GetAsync(Guid id);
        Task<IReadOnlyList<TEntity>> GetAsync(IEnumerable<Guid> ids);
        Task<bool> ExistNameAsync<FEntity>(string name, Guid? id) where FEntity : class, TEntity, IName;
        Task AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(IEnumerable<Guid> ids);
    }
}
