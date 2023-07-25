using Lazar.Services.Contracts.Response.Base;
using Lazar.Services.Contracts.Response.Enums;

namespace Lazar.Services.Contracts.Response.Models {
    public class DataTableDto<TModelDto> : BaseResponseDto
    {
        public int totalCount { get; set; } = 0;
        public IReadOnlyList<TModelDto> data { get; set; } = new List<TModelDto>();
        public DataTableDto(int totalRecords, IReadOnlyList<TModelDto> data) : base() {
            totalCount = totalRecords;
            this.data = data;
        }
        public DataTableDto(int totalRecords, IReadOnlyList<TModelDto> data, string message)
            : this(totalRecords, data) {
            Message = message;
        }
        public DataTableDto(int totalRecords, IReadOnlyList<TModelDto> data, string message, ResponseResultState state) : this(totalRecords, data, message) {
            Result = state;
        }
    }
}
