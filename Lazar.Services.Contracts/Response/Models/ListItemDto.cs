using Lazar.Services.Contracts.Response.Base;
using Lazar.Services.Contracts.Response.Enums;

namespace Lazar.Services.Contracts.Response.Models {
    /// <summary>
    /// Элемент для выпадающего списка
    /// </summary>
    /// <typeparam name="Tid"></typeparam>
    public class ListItemDto<Tid> : BaseResponseDto
        where Tid : struct {
        /// <summary>
        /// Ключ
        /// </summary>
        public Tid Id { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public ListItemDto() {
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Ключ</param>
        /// <param name="value">Значение</param>
        public ListItemDto(Tid id, string value) : base() {
            Id = id;
            Text = value;
        }
        public ListItemDto(Tid id, string value, string message) : this(id, value) {
            Message = message;
        }
        public ListItemDto(Tid id, string value, string message, ResponseResultState state) : this(id, value, message) {
            Result = state;
        }
    }
}
