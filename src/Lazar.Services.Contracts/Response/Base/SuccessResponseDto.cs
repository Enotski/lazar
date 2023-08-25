using Lazar.Services.Contracts.Response.Enums;

namespace Lazar.Services.Contracts.Response.Base {
    public class SuccessResponseDto : BaseResponseDto {
        public SuccessResponseDto() {
            Result = ResponseResultState.Ok;
        }
        public SuccessResponseDto(string message) : this() {
            Message = message;
        }
    }
}
