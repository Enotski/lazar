using Lazar.Services.Contracts.Request;

namespace Lazar.Srevices.Iterfaces.Base {
    /// <summary>
    /// Base service of logging
    /// </summary>
    public interface ILogService {
        /// <summary>
        /// Remove records
        /// </summary>
        /// <param name="ids">Primary keys</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        Task DeleteRecordsAsync(IEnumerable<Guid> ids, string login);
        /// <summary>
        /// Clear all log in storage
        /// </summary>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        Task ClearLogAsync(string login);
        /// <summary>
        /// Remove all entities by period 
        /// </summary>
        /// <param name="period">Period of dates to clear log</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        Task RemoveByPeriodAsync(PeriodDto period, string login);
    }
}
