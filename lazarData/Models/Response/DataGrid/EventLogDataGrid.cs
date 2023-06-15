using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.ViewModels {
    public class EventLogDataGrid : DataGridRowResponseModel {
        public int Num;
        public string LocalRecordingTime;
        public string SubSystemName;
        public string EventTypeName;
        public string Message;
        public string Result;
        public string IP;
        public string UserName;
    }
}
