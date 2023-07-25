using CommonUtils.Utils;
using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Core.SelectorModels.Base;
using Lazar.Domain.Interfaces.Options;
using Lazar.Domain.Interfaces.Repositories.Base;
using Lazar.Infrastructure.Data.Ef.Context;
using Microsoft.EntityFrameworkCore;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Base {
    internal abstract class NameRepository<TEntity> : BaseRepository<TEntity>, INameRepository<TEntity> where TEntity: class, IKey, IName {
        public NameRepository(LazarContext dbContext) : base(dbContext) { }
        /// <summary>
        /// Возвращает наименование сущности
        /// </summary>
        /// <param name="id">Код сущности</param>
        /// <returns></returns>
        public async Task<string> GetNameByIdAsync(Guid id) {
            try {
                return await _dbContext.Set<TEntity>().Where(m => m.Id == id).Select(m => m.Name).FirstOrDefaultAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Возвращает код сущности по наименованию
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        /// Возвращает все наименования
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<string>> GetLowerNamesAsync() {
            try {
                return await _dbContext.Set<TEntity>().Where(m => !string.IsNullOrEmpty(m.Name)).Select(m => m.Name.Trim().ToLower()).ToListAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Возвращает список пар Код-Наименование
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<KeyNameSelectorModel>> GetKeyNameRecordsAsync(string term, IPaginatedOption paginationOption) {
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
    }
}
