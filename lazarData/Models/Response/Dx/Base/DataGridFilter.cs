using lazarData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.DataGrid {
    public class DataGridFilter {
        public string ColumnName { get; set; }
        public DataGridFilterType Type { get; set; }
        public string Value { get; set; }
    }
}
