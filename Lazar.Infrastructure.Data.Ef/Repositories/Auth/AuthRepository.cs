using Lazar.Domain.Interfaces.Repositories.Auth;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using lazarData.Models.EventLogs;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Auth
{
    public class AuthRepository : BaseRepository<EventLog>, IAuthRepository
    {
        public AuthRepository(LazarContext context) : base(context) { }
    }
}
