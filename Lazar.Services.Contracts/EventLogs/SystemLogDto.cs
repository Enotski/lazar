using Lazar.Domain.Core.Enums;
using Lazar.Services.Contracts.Base;
using CommonUtils.Utils;

namespace Lazar.Services.Contracts.EventLogs {
    public class SystemLogDto : ChangedByDto {
        public string Description { get; set; }
        public SubSystemType SubSystem { get; set; }
        public string SubSystemName { get => SubSystem.GetDescription(); }
        public EventType EventType { get; set; }
        public string EventTypeName { get => EventType.GetDescription(); }

        public SystemLogDto() : base() { }
        public SystemLogDto(Guid id, string description, SubSystemType subSystem, EventType eventType, string changedBy, DateTime dateChange)
            : base(id, changedBy, dateChange) {
            Description = description;
            SubSystem = subSystem;
            EventType = eventType;
        }
    }
}
