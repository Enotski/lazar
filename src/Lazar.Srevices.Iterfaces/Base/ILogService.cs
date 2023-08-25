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
        /// Remove all entities by days 
        /// </summary>
        /// <param name="days">The number of days before the current date after which records are deleted</param>
        /// <returns></returns>
        Task ClearLogAsync(int days);
    }
}
