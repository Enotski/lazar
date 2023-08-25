namespace Lazar.Srevices.Iterfaces.Base {
    /// <summary>
    /// Base service
    /// </summary>
    public interface IBaseService {
        /// <summary>
        /// Remove entities
        /// </summary>
        /// <param name="ids">Primary keys</param>
        /// <param name="login">Login of the user who triggered the event</param>
        /// <returns></returns>
        Task DeleteAsync(IEnumerable<Guid> ids, string login);
    }
}
