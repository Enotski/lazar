using Lazar.Domain.Core.Models.Administration;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Microsoft.EntityFrameworkCore;

namespace Lazar.Infrastructure.JwtAuth.Repositories {
    /// <summary>
    /// Authentication repository
    /// </summary>
    public class AuthReppository : BaseRepository<AuthModel>, IAuthRepository {
        public AuthReppository(LazarContext context) : base(context) {
        }
        /// <summary>
        /// Get model by login
        /// </summary>
        /// <param name="login">Login of user</param>
        /// <returns>Authentication model</returns>
        public async Task<AuthModel> GetAuthModelAsync(string login) {
            return await _dbContext.AuthModels.FirstOrDefaultAsync(m => m.Login == login);
        }
        /// <summary>
        /// Remove authentication model
        /// </summary>
        /// <param name="login">Value of login in authentication model</param>
        /// <returns></returns>
        public async Task DeleteAsync(string login) {
            var entity = await _dbContext.AuthModels.FirstOrDefaultAsync(m => m.Login == login);
            if(entity != null) {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
