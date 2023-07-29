using Lazar.Services.Contracts.Response.Enums;

namespace Lazar.Services.Contracts.Response.Base {
    public class WarningResponseDto : BaseResponseDto {
        public WarningResponseDto() {
            Result = ResponseResultState.Warning;
        }
        public WarningResponseDto(string message) : this() {
            Message = message;
        }
    }
}
