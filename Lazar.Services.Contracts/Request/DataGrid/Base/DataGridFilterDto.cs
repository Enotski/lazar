using Lazar.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazar.Services.Contracts.Request.DataGrid.Base
{
    public class DataGridFilterDto
    {
        public string ColumnName { get; set; }
        public FilterType Type { get; set; }
        public string Value { get; set; }
    }
}
