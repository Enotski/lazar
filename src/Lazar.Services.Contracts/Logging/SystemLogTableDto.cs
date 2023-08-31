using Lazar.Domain.Core.Enums;

namespace Lazar.Services.Contracts.Logging {
    public class SystemLogTableDto : SystemLogDto {
        public int Num { get; set; }
        public SystemLogTableDto() : base() { }
        public SystemLogTableDto(Guid id, int num, string description, SubSystemType subSystem, EventType eventType, string changedBy, DateTime dateChange)
            : base(id, description, subSystem, eventType, changedBy, dateChange) {
            Num = num;
        }
    }
}
