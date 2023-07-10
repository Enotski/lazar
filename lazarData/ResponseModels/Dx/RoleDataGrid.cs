using lazarData.ResponseModels.Dx.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.ResponseModels.Dx
{
    public class RoleDataGrid : DataGridRowResponseModel
    {
        public string Name { get; set; }
        public int Num { get; set; }
        public string DateChange { get; set; }
    }
}
