using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.DataGrid.Base
{
    public class DataGridRequestModel
    {
        public int skip { get; set; }
        public int take { get; set; }
        public IEnumerable<DataGridFilter>? filters { get; set; }
        public IEnumerable<DataGridSort>? sorts { get; set; }
    }
}
