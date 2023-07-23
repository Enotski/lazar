using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using lazarData.Models.EventLogs;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Administration
{
    public class UserProfileRepository : BaseRepository<EventLog>, IUserProfileRepository
    {
        public UserProfileRepository(LazarContext context) : base(context) { }
    }
}
