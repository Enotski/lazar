using Lazar.Domain.Core.EntityModels.Auth;
using Lazar.Domain.Interfaces.Repositories.Auth;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Auth {
    public class AuthReppository : BaseRepository<LoginModel>, IAuthRepository {
        public AuthReppository(LazarContext context) : base(context) {
        }
        public async Task<LoginModel> GetLoginModelAsync(string login) {
           return await _dbContext.LoginModels.FirstOrDefaultAsync(m => m.Login == login);
        }
    }
}
