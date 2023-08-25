using Lazar.Services.Contracts.Response.Base;
using Lazar.Services.Contracts.Response.Enums;

namespace Lazar.Services.Contracts.Response.Models {
    public class EntityResponseDto<TModelDto> : BaseResponseDto where TModelDto : class {
        public TModelDto Data { get; set; }
        public EntityResponseDto(TModelDto data) : base() {
            Data = data;
        }
        public EntityResponseDto(TModelDto data, string message) : this(data) {
            Message = message;
        }
        public EntityResponseDto(TModelDto data, string message, ResponseResultState result) : this(data, message) {
            Result = result;
        }
    }
    public sealed class EntitiesResponseDto<TModelDto> : BaseResponseDto where TModelDto : class {
        public IEnumerable<TModelDto> Data { get; set; }
        public EntitiesResponseDto(IEnumerable<TModelDto> data) : base() {
            Data = data;
        }
        public EntitiesResponseDto(IEnumerable<TModelDto> data, string message) : this(data) {
            Message = message;
        }
        public EntitiesResponseDto(IEnumerable<TModelDto> data, string message, ResponseResultState result) : this(data, message) {
            Result = result;
        }
    }
}
