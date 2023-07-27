using CommonUtils.Utils;
using Lazar.Services.Contracts.Response.Base;

namespace Lazar.Services.Contracts.Response.Models {
    public sealed class ErrorResponseDto : BaseResponseDto {
        public ErrorResponseDto() {
            Result = Enums.ResponseResultState.Error;
        }
        public ErrorResponseDto(string message) : this() {
            Message = message;
        }
        public ErrorResponseDto(Exception exception) : this() {
            Message = exception.Format();
        }
    }
}
