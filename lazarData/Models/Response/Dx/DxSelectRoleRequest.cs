using lazarData.Models.Response.DataGrid.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.Dx
{
    public class DxSelectRoleRequest : DxSelectRequestModel
    {
        public string? selectedUserId { get; set; }
    }
}
