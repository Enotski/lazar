using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.ViewModels {
    public class RoleViewModel : BaseResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
