using AutoMapper;

namespace Lazar.Infrastructure.Mapper {
    /// <summary>
    /// Mapper of models
    /// </summary>
    public interface IModelMapper {
        IMapper Mapper { get; }
    }
}
