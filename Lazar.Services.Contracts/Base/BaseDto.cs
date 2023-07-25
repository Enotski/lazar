
namespace Lazar.Services.Contracts.Base {
    /// <summary>
    /// Базовая модель сущностей системы  
    /// </summary>
    public abstract class BaseDto {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        public Guid Id { get; set; }
        public BaseDto() { }
        public BaseDto(Guid id) {
            Id = id;
        }
    }
}
