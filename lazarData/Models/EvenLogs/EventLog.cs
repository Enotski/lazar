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
    internal class EventLog: IKeyEntity, IChangedUserReference
    {
        public Guid Id { get; set; }
        public DateTime DateChange { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public Guid? ChangedUserId { get; set; }
        public User ChangedUser { get; set; }
    }
}
