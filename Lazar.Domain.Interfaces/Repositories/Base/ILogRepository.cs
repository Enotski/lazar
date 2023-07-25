using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Interfaces.Repositories.Base {
    public interface ILogRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IKey, IDateChange {
        Task ClearAsync(int days);
        Task ClearAsync();
    }
}
