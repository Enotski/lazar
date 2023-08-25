namespace Lazar.Services.Contracts.Base {
    /// <summary>
    /// Base changed by info dto model
    /// </summary>
    public abstract class ChangedByDto : DateChangeDto {
        /// <summary>
        /// The user who modified the entry
        /// </summary>
        public string? ChangedBy { get; set; }
        public ChangedByDto() : base() { }
        public ChangedByDto(Guid id, string changedBy, DateTime dateChange) : base(id, dateChange) {
            ChangedBy = changedBy;
        }
    }
}
