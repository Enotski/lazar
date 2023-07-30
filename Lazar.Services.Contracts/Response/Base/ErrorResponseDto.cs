using CommonUtils.Utils;

namespace Lazar.Services.Contracts.Response.Base {
    public class ErrorResponseDto : BaseResponseDto{
        public string Description { get; set; }
        public ErrorResponseDto() {
            Result = Enums.ResponseResultState.Error;
        }
        public ErrorResponseDto(string message) : this() {
            Message = message;
        }
        public ErrorResponseDto(Exception exception) : this() {
            Message = exception.Message;
            Description = exception.Format();
        }
    }
}
