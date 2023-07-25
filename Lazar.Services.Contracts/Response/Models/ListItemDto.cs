using Lazar.Services.Contracts.Response.Base;
using Lazar.Services.Contracts.Response.Enums;

namespace Lazar.Services.Contracts.Response.Models {
    /// <summary>
    /// Элемент для выпадающего списка
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class ListItemDto<TKey> : BaseResponseDto
        where TKey : struct {
        /// <summary>
        /// Ключ
        /// </summary>
        public TKey Key { get; set; }
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
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        public ListItemDto(TKey key, string value) : base() {
            Key = key;
            Text = value;
        }
        public ListItemDto(TKey key, string value, string message) : this(key, value) {
            Message = message;
        }
        public ListItemDto(TKey key, string value, string message, ResponseResultState state) : this(key, value, message) {
            Result = state;
        }
    }
}
