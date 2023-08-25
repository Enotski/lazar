using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Core.SelectorModels.Base;
using Lazar.Domain.Interfaces.Options;

namespace Lazar.Domain.Interfaces.Repositories.Base {
    /// <summary>
    /// Base repository for entity with login property
    /// </summary>
    /// <typeparam name="TEntity">Type witch implement IKey and ILogin</typeparam>
    public interface ILoginRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IKey, ILogin {
        /// <summary>
        /// Get login property of entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Login</returns>
        Task<string> GetLoginByKeyAsync(Guid id);
        /// <summary>
        /// Return primary key of entity by login property
        /// </summary>
        /// <param name="login">Login property</param>
        /// <returns>Primary key</returns>
        Task<Guid> GetKeyByLoginAsync(string login);
        /// <summary>
        /// Check login existance
        /// </summary>
        /// <param name="login">Login property</param>
        /// <param name="id">Primary key</param>
        /// <returns>Existance value</returns>
        Task<bool> LoginExistsAsync(string login, Guid? id = null);
        /// <summary>
        /// Get list of key-login models
        /// </summary>
        /// <param name="term">Search term by login property</param>
        /// <param name="paginationOption">Pagination</param>
        /// <returns>List of key-login models</returns>
        Task<IReadOnlyList<KeyNameSelectorModel>> GetKeyLoginRecordsAsync(string term, IPaginatedOption? paginationOption = null);
    }
}
