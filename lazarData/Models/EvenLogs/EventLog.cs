using lazarData.Enums;
using lazarData.Interfaces;
using lazarData.Models.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
