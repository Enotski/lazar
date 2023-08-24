using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Interfaces.Repositories.Base;

namespace Lazar.Infrastructure.JwtAuth.Iterfaces.Auth {
    public interface IAuthRepository : ILoginRepository<AuthModel>{
        Task<AuthModel> GetLoginModelAsync(string login);
        Task AddAsync(AuthModel entity);

        Task UpdateAsync(AuthModel entity);

        Task DeleteAsync(AuthModel entity);
        Task DeleteAsync(string login);
    }
}

