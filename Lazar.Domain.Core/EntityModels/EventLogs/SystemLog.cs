using Lazar.Domain.Core.EntityModels.Base;
using Lazar.Domain.Core.Enums;

namespace Lazar.Domain.Core.EntityModels.EventLogs {
    public class SystemLog : ChangedByEntity {
        public string Description { get; set; }
        public SubSystemType SubSystem { get; set; }
        public EventType EventType { get; set; }

        public SystemLog() : base() { }
        public SystemLog(Guid id, string description, SubSystemType subSystem, EventType eventType, string changedBy, DateTime dateChange)
            : base(id, changedBy, dateChange) {
            Description = description;
            SubSystem = subSystem;
            EventType = eventType;
        }
    }
}
