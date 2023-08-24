using Lazar.Domain.Core.Models.Administration;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Administration;
using Lazar.Infrastructure.JwtAuth.Iterfaces.Auth;
using Microsoft.EntityFrameworkCore;

namespace Lazar.Infrastructure.JwtAuth.Repositories {
    public class AuthReppository : LoginRepository<AuthModel>, IAuthRepository {
        public AuthReppository(LazarContext context) : base(context) {
        }
        public async Task<AuthModel> GetLoginModelAsync(string login) {
            return await _dbContext.AuthModels.FirstOrDefaultAsync(m => m.Login == login);
        }
        public async Task DeleteAsync(string login) {
            var entity = await _dbContext.AuthModels.FirstOrDefaultAsync(m => m.Login == login);
            if(entity != null) {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
