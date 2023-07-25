using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Interfaces.Repositories.Base;
using Lazar.Infrastructure.Data.Ef.Context;
using Z.EntityFramework.Plus;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Base {
    internal abstract class LogRepository<TEntity> : BaseRepository<TEntity>, ILogRepository<TEntity> where TEntity : class, IKey, IDateChange {
        public LogRepository(LazarContext dbContext) : base(dbContext) { }

        public async Task ClearAsync(int days) {
            try {
                days = days < 1 ? 1 : days;
                var cutoffDate = DateTime.UtcNow.AddDays(-days);
                await _dbContext.Set<TEntity>().Where(x => x.DateChange < cutoffDate).DeleteAsync();
            } catch { throw; }
        }
        public async Task ClearAsync() {
            try {
                await _dbContext.Set<TEntity>().DeleteAsync();
            } catch { throw; }
        }
    }
}
