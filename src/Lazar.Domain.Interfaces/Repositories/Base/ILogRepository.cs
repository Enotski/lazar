using Lazar.Domain.Core.Interfaces;

namespace Lazar.Domain.Interfaces.Repositories.Base {
    /// <summary>
    /// Base repository for log entity
    /// </summary>
    /// <typeparam name="TEntity">Type witch implement IKey and IDateChange</typeparam>
    public interface ILogRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IKey, IDateChange {
        /// <summary>
        /// Remove all entities by days 
        /// </summary>
        /// <param name="days">The number of days before the current date after which records are deleted</param>
        /// <returns></returns>
        Task ClearAsync(int days);
        /// <summary>
        /// Remove all entities
        /// </summary>
        /// <returns></returns>
        Task ClearAsync();
    }
}
