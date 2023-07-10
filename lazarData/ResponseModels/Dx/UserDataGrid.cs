using lazarData.ResponseModels.Dx.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.ResponseModels.Dx
{
    public class UserDataGrid : DataGridRowResponseModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string DateChange { get; set; }
        public string Roles { get; set; }
        public int Num { get; set; }
    }
}
