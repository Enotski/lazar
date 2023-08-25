using Lazar.Services.Contracts.Response.Base;
using Lazar.Services.Contracts.Response.Enums;

namespace Lazar.Services.Contracts.Response.Models {
    public class ListItemDto<TKey> : BaseResponseDto
        where TKey : struct {
        public TKey Id { get; set; }
        public string Name { get; set; }
        public ListItemDto() {
        }
        public ListItemDto(TKey id, string name) : base() {
            Id = id;
            Name = name;
        }
        public ListItemDto(TKey id, string name, string message) : this(id, name) {
            Message = message;
        }
        public ListItemDto(TKey id, string name, string message, ResponseResultState state) : this(id, name, message) {
            Result = state;
        }
    }
}
