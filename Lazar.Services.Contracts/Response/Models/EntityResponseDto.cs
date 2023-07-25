using Lazar.Services.Contracts.Response.Base;
using Lazar.Services.Contracts.Response.Enums;

namespace Lazar.Services.Contracts.Response.Models {
    /// <summary>
    /// Модель ответа для ссылочных типов данных
    /// </summary>
    /// <typeparam name="TModelDto"></typeparam>
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
        public IReadOnlyList<TModelDto> Data { get; set; }
        public EntitiesResponseDto(IReadOnlyList<TModelDto> data) : base() {
            Data = data;
        }
        public EntitiesResponseDto(IReadOnlyList<TModelDto> data, string message) : this(data) {
            Message = message;
        }
        public EntitiesResponseDto(IReadOnlyList<TModelDto> data, string message, ResponseResultState result) : this(data, message) {
            Result = result;
        }
    }
}
