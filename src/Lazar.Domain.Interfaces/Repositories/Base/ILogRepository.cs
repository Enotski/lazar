using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Interfaces.Repositories.Base {
    /// <summary>
    /// Base repository for log entity
    /// </summary>
    /// <typeparam name="TEntity">Type witch implement IKey and IDateChange</typeparam>
    public interface ILogRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IKey, IDateChange {
        /// <summary>
        /// Remove all entities by period 
        /// </summary>
        /// <param name="star">Lower bound of period</param>
        /// <param name="end">Upper bound of period</param>
        /// <returns></returns>
        Task ClearByPeriodAsync(DateTime star, DateTime end);
        /// <summary>
        /// Remove all entities
        /// </summary>
        /// <returns></returns>
        Task ClearAsync();
    }
}
