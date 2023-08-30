using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;
using Lazar.Infrastructure.Data.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Base {
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IKey {
        protected readonly LazarContext _dbContext;
        public BaseRepository(LazarContext dbContext) {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Creating a query to select entities
        /// </summary>
        /// <param name="filter">Filter fo select</param>
        /// <param name="ordered">Order predicate</param>
        /// <param name="paginated">Pagination options</param>
        /// <param name="includes">Included entities</param>
        /// <returns></returns>
        protected IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>>? filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? ordered = null,
             IPaginatedOption? paginated = null, params Expression<Func<TEntity, object>>[] includes) {
            try {
                IQueryable<TEntity> query = _dbContext.Set<TEntity>();

                if (includes is not null && includes.Any()) {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }

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
        /// <summary>
        /// Get entity by key
        /// </summary>
        /// <param name="id">Primary key of entity</param>
        /// <returns>Entity</returns>
        public async Task<TEntity> GetAsync(Guid? id) {
            try {
                return await BuildQuery(m => m.Id == id).FirstOrDefaultAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Get entities by key
        /// </summary>
        /// <param name="ids">Primary keys of entities</param>
        /// <returns>Entity</returns>
        public async Task<IEnumerable<TEntity>> GetAsync(IEnumerable<Guid> ids) {
            try {
                if (!ids.Any()) {
                    return new List<TEntity>();
                }
                return await BuildQuery(m => ids.Contains(m.Id)).ToListAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="filter">Concrete filter</param>
        /// <returns></returns>
        protected async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null) {
            try {
                return await BuildQuery(filter).CountAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity">New entity</param>
        /// <returns></returns>
        public async Task AddAsync(TEntity entity) {
            try {
                _dbContext.Set<TEntity>().Add(entity);
                await _dbContext.SaveChangesAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Create entities
        /// </summary>
        /// <param name="entities">New entities</param>
        /// <returns></returns>
        public async Task AddAsync(IEnumerable<TEntity> entities) {
            try {
                if (!entities.Any()) {
                    return;
                }
                _dbContext.Set<TEntity>().AddRange(entities);
                await _dbContext.SaveChangesAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Changed entity</param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity) {
            try {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Changed entities</param>
        /// <returns></returns>
        public async Task UpdateAsync(IEnumerable<TEntity> entities) {
            try {
                if (!entities.Any()) {
                    return;
                }
                foreach (var entity in entities) {
                    _dbContext.Entry(entity).State = EntityState.Modified;
                }
                await _dbContext.SaveChangesAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity) {
            try {
                _dbContext.Set<TEntity>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Remove entity by key
        /// </summary>
        /// <param name="id">Primary key of entity</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid? id) {
            try {
                var entity = await GetAsync(id);
                if (entity is not null) {
                    await DeleteAsync(entity);
                }
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Remove entities by keys
        /// </summary>
        /// <param name="ids">Primary keys of entities</param>
        /// <returns></returns>
        public async Task DeleteAsync(IEnumerable<Guid> ids) {
            try {
                if (!ids.Any()) {
                    return;
                }
                int page = -1, rows = 512;
                IEnumerable<Guid> query = null!;
                do {
                    query = ids.Skip(++page * rows).Take(rows);
                    await _dbContext.Set<TEntity>()
                        .Where(x => query.Contains(x.Id)).DeleteAsync(x => x.BatchSize = rows);
                } while (query.Any());
            } catch { throw; }
        }
    }
}
