using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Core.SelectorModels.Base;
using Lazar.Domain.Interfaces.Options;

namespace Lazar.Domain.Interfaces.Repositories.Base {
    public interface INameRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IKey, IName {
        Task<string> GetNameByIdAsync(Guid Id);
        Task<Guid> GetKeyByNameAsync(string name);
        Task<IReadOnlyList<KeyNameSelectorModel>> GetKeyNameRecordsAsync(string term, IPaginatedOption paginationOption);
    }
}
