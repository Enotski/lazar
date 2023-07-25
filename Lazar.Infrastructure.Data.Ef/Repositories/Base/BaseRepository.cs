using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;
using Lazar.Infrastructure.Data.Ef.Context;
using System.Data.Entity;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Base {
    internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IKey {
        protected readonly LazarContext _dbContext;
        public BaseRepository(LazarContext dbContext) {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Удобное создание запроса на выборку сущностей
        /// </summary>
        /// <param name="filter">Фильтр для выборки</param>
        /// <returns></returns>
        protected IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> ordered = null,
             IPaginatedOption paginated = null, IEnumerable<Expression<Func<TEntity, object>>> includes = null) {

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
        }
        public async Task<TEntity> GetAsync(Guid id) {
            return await BuildQuery(m => m.Id == id).FirstOrDefaultAsync();
        }
        protected async Task<TEntity> GetAsync(Guid id, IEnumerable<Expression<Func<TEntity, object>>> includes) {
            return await BuildQuery(m => m.Id == id, null, null, includes).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<TEntity>> GetAsync(IEnumerable<Guid> ids) {
            if (!ids.Any()) {
                return new List<TEntity>();
            }
            return await BuildQuery(m => ids.Contains(m.Id)).ToListAsync();
        }
        protected async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null) {
            return await BuildQuery(filter).CountAsync();
        }
        public async Task AddAsync(TEntity entity) {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddAsync(IEnumerable<TEntity> entities) {
            if (!entities.Any()) {
                return;
            }
            _dbContext.Set<TEntity>().AddRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity) {
            _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(IEnumerable<TEntity> entities) {
            if (!entities.Any()) {
                return;
            }
            foreach (var entity in entities) {
                _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TEntity entity) {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id) {
            var entity = await GetAsync(id);
            if (entity is not null) {
                await DeleteAsync(entity);
            }
        }
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

        public async Task<bool> ExistNameAsync<FEntity>(string name, Guid? id) where FEntity : class, TEntity, IName {
            try {
                if (string.IsNullOrWhiteSpace(name))
                    return false;
                name = name.Trim().ToLower();

                FEntity entity = null;
                if (id.HasValue)
                    entity = await _dbContext.Set<FEntity>().FirstOrDefaultAsync(x => x.Id != id && x.Name.Trim().ToLower() == name);
                else
                    entity = await _dbContext.Set<FEntity>().FirstOrDefaultAsync(x => x.Name.Trim().ToLower() == name);

                return entity != null;
            } catch {
                throw;
            }
        }
    }
}
