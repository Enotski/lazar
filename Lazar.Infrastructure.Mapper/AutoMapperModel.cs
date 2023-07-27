using AutoMapper;
using Lazar.Domain.Core.EntityModels.Logging;
using Lazar.Domain.Core.Models.Administration;
using Lazar.Domain.Core.SelectorModels.Administration;
using Lazar.Services.Contracts.Administration;
using Lazar.Services.Contracts.Logging;

namespace Lazar.Infrastructure.Mapper {
    public class AutoModelMapper : IModelMapper {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() => {
            var config = new MapperConfiguration(cfg => {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                cfg.AddProfile<ModelMapperProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });
        public IMapper Mapper => Lazy.Value;
    }
    public class ModelMapperProfile : Profile {
        public ModelMapperProfile() {
            CreateMap<SystemLog, SystemLogDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<UserSelectorModel, UserDto>().ReverseMap();
        }
    }
}
