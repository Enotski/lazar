using CommonUtils.Utils;
using Lazar.Domain.Core.EntityModels.Base;
using Lazar.Domain.Core.Enums;

namespace Lazar.Domain.Core.EntityModels.Logging {
    public class SystemLog : NameEntity {
        public string Description { get; set; }
        public string Message { get; set; }
        public string SubSystem { get; set; }
        public string EventType { get; set; }

        public SystemLog() : base() { }
        public SystemLog(Guid id, string message, string description, SubSystemType subSystem, EventType eventType, string changedBy, DateTime dateChange)
    : base(id, message, changedBy, dateChange) {
            Description = description;
            SubSystem = subSystem.GetDescription();
            EventType = eventType.GetDescription();
        }
        public SystemLog(string message, string description, SubSystemType subSystem, EventType eventType, string changedBy)
            : this(Guid.NewGuid(), message, description, subSystem, eventType, changedBy, DateTime.UtcNow) {
        }
    }
}
