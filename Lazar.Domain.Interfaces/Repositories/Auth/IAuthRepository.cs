using Lazar.Domain.Core.EntityModels.Auth;
using Lazar.Domain.Interfaces.Repositories.Base;

namespace Lazar.Domain.Interfaces.Repositories.Auth {
    public interface IAuthRepository : IBaseRepository<LoginModel> {
        Task<LoginModel> GetLoginModelAsync(string login);
    }
}
