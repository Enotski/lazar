using lazarData.Enums;
using lazarData.Interfaces;
using lazarData.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.LogEvents
{
    internal class LogEvent: IKeyEntity
    {
        public Guid Id { get; set; }
        public DateTime DateChange { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public Guid ChangedUserId { get; set; }
        public User ChangedUser { get; set; }
    }
}
