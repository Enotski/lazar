namespace Lazar.Services.Contracts.Base {
    /// <summary>
    /// Base changed date info dto model 
    /// </summary>
    public abstract class DateChangeDto : BaseDto {
        /// <summary>
        /// Record modification date
        /// </summary>
        public DateTime? DateChange { get; set; }
        public DateChangeDto() : base() { }
        public DateChangeDto(Guid id, DateTime dateChange)
            : base(id) {
            DateChange = dateChange;
        }
    }
}
