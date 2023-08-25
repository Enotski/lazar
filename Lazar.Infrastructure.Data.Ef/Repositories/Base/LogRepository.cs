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
        /// Remove all entities by days 
        /// </summary>
        /// <param name="days">The number of days before the current date after which records are deleted</param>
        /// <returns></returns>
        public async Task ClearAsync(int days) {
            try {
                days = days < 1 ? 1 : days;
                var cutoffDate = DateTime.UtcNow.AddDays(-days);
                await _dbContext.Set<TEntity>().Where(x => x.DateChange < cutoffDate).DeleteAsync();
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
