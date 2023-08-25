using Lazar.Services.Contracts.Response.Enums;
using Lazar.Services.Contracts.Response.Interfaces;

namespace Lazar.Services.Contracts.Response.Base {
    public abstract class BaseResponseDto : IResponse {
        public string Message { get; set; }
        public ResponseResultState Result { get; set; }
        public BaseResponseDto() { }
        public BaseResponseDto(string message) {
            Message = message;
        }
        public BaseResponseDto(string message, ResponseResultState result) : this(message) {
            Result = result;
        }

    }
}
