using Lazar.Services.Contracts.Response.Base;
using Lazar.Services.Contracts.Response.Enums;

namespace Lazar.Services.Contracts.Response.Models {
    /// <summary>
    /// Элемент для выпадающего списка
    /// </summary>
    /// <typeparam name="Tid"></typeparam>
    public class ListItemDto<TKey> : BaseResponseDto
        where TKey : struct {
        /// <summary>
        /// Ключ
        /// </summary>
        public TKey Id { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public ListItemDto() {
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Ключ</param>
        /// <param name="name">Значение</param>
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
