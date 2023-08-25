using Lazar.Domain.Core.Models.Administration;

namespace Lazar.Infrastructure.JwtAuth.Iterfaces.Auth {
    /// <summary>
    /// Authentication repository
    /// </summary>
    public interface IAuthRepository{
        /// <summary>
        /// Get model by login
        /// </summary>
        /// <param name="login">Login of user</param>
        /// <returns>Authentication model</returns>
        Task<AuthModel> GetAuthModelAsync(string login);
        /// <summary>
        /// Create authentication model
        /// </summary>
        /// <param name="entity">New authentication model</param>
        /// <returns></returns>
        Task AddAsync(AuthModel entity);
        /// <summary>
        /// Update authentication model
        /// </summary>
        /// <param name="entity">Existing authentication model</param>
        /// <returns></returns>
        Task UpdateAsync(AuthModel entity);
        /// <summary>
        /// Remove authentication model
        /// </summary>
        /// <param name="entity">Existing authentication model</param>
        /// <returns></returns>
        Task DeleteAsync(AuthModel entity);
        /// <summary>
        /// Remove authentication model
        /// </summary>
        /// <param name="login">Value of login in authentication model</param>
        /// <returns></returns>
        Task DeleteAsync(string login);
    }
}

