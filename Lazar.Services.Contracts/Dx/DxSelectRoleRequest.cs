using lazarData.ResponseModels.Dx.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.ResponseModels.Dx
{
    public class DxSelectRoleRequest : DxSelectRequestModel
    {
        public string? selectedUserId { get; set; }
    }
}
