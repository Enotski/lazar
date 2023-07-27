using Lazar.Domain.Core.EntityModels.Auth;
using Lazar.Domain.Interfaces.Repositories.Auth;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Auth {
    public class AuthRepository : BaseRepository<AuthModel>, IAuthRepository
    {
        public AuthRepository(LazarContext context) : base(context) { }
    }
}
