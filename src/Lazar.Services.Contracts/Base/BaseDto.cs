
namespace Lazar.Services.Contracts.Base {
    /// <summary>
    /// Base dto model
    /// </summary>
    public abstract class BaseDto {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid? Id { get; set; }
        public BaseDto() { }
        public BaseDto(Guid id) {
            Id = id;
        }
    }
}
