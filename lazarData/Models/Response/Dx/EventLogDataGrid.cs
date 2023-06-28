using lazarData.Models.Response.DataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.ViewModels {
    public class EventLogDataGrid : DataGridRowResponseModel {
        public int Num { get; set; }
        public string SubSystemName { get; set; }
        public string EventTypeName { get; set; }
        public string UserName { get; set; }
        public string DateChange { get; set; }
    }
}
