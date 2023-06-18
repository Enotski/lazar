using lazarData.Enums;
using lazarData.Models.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.ViewModels
{
    public class EventLogViewModel : BaseResponseModel
    {
        public Guid Id { get; set; }
        public DateTime DateChange { get; set; }
        public string Description { get; set; }
        public EventType EventType { get; set; }
        public string TypeInfo { get; set; }
        public string User { get; set; }
    }
}
