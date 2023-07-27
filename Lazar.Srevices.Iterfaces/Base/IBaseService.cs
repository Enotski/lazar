using Lazar.Services.Contracts.Administration;

namespace Lazar.Srevices.Iterfaces.Base {
    public interface IBaseService {
        Task DeleteAsync(IEnumerable<Guid> ids, string login);
    }
}
