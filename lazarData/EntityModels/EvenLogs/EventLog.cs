using lazarData.EntityModels;
using lazarData.Enums;
using lazarData.Interfaces;
using lazarData.Models.Administration;

namespace lazarData.Models.EventLogs
{
    public class EventLog: EntityBase, IChangedUserReference
    {
        public string Description { get; set; }
        public SubSystemType SubSystem { get; set; }
        public EventType EventType { get; set; }
        public Guid? ChangedUserId { get; set; }
        public User ChangedUser { get; set; }
    }
}
