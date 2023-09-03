using Lazar.Services.Contracts.Request;

namespace Lazar.Srevices.Iterfaces.Base {
    /// <summary>
    /// Base service of logging
    /// </summary>
    public interface ILogService {
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
        Task ClearByPeriodAsync(PeriodDto period, string login);
    }
}
