using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Interfaces.Repositories.Base;
using Lazar.Infrastructure.Data.Ef.Context;
using Z.EntityFramework.Plus;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Base {
    /// <summary>
    /// Base repository of logging
    /// </summary>
    /// <typeparam name="TEntity">Type witch implement IKey and ILogin</typeparam>
    internal abstract class LogRepository<TEntity> : BaseRepository<TEntity>, ILogRepository<TEntity> where TEntity : class, IKey, IDateChange {
        public LogRepository(LazarContext dbContext) : base(dbContext) { }
        /// <summary>
        /// Remove all entities by period 
        /// </summary>
        /// <param name="star">Lower bound of period</param>
        /// <param name="end">Upper bound of period</param>
        /// <returns></returns>
        public async Task ClearByPeriodAsync(DateTime start, DateTime end) {
            try {
                await _dbContext.Set<TEntity>().Where(x =>x.DateChange >= start && x.DateChange <= end).DeleteAsync();
            } catch { throw; }
        }
        /// <summary>
        /// Remove all entities
        /// </summary>
        /// <returns></returns>
        public async Task ClearAsync() {
            try {
                await _dbContext.Set<TEntity>().DeleteAsync();
            } catch { throw; }
        }
    }
}
