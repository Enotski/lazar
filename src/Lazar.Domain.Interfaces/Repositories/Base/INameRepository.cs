using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Core.SelectorModels.Base;
using Lazar.Domain.Interfaces.Options;

namespace Lazar.Domain.Interfaces.Repositories.Base {
    /// <summary>
    /// Base repository for entity with name property
    /// </summary>
    /// <typeparam name="TEntity">Type witch implement IKey and IName</typeparam>
    public interface INameRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IKey, IName {
        /// <summary>
        /// Get name property of entity
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Name</returns>
        Task<string> GetNameByIdAsync(Guid id);
        /// <summary>
        /// Get primary key of entity by name property
        /// </summary>
        /// <param name="name">Name property</param>
        /// <returns>Primary key</returns>
        Task<Guid> GetKeyByNameAsync(string name);
        /// <summary>
        /// Check name existance
        /// </summary>
        /// <param name="name">Name property</param>
        /// <param name="id">Primary key</param>
        /// <returns>Existance value</returns>
        Task<bool> NameExistsAsync(string name, Guid? id);
        /// <summary>
        /// Get list of key-name models
        /// </summary>
        /// <param name="term">Search term by login property</param>
        /// <param name="paginationOption">Pagination</param>
        /// <returns>List of keys-names of models</returns>
        Task<IEnumerable<KeyNameSelectorModel>> GetKeyNameRecordsAsync(string term, IPaginatedOption? paginationOption = null);
    }
}