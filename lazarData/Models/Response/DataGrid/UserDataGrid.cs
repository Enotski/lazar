using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.ViewModels {
	public class UserDataGrid : DataGridRowResponseModel {
		public string FullName { get; set; }
		public string CreationDate { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string DepartmentName { get; set; }
		public string FilialName { get; set; }
		public string PostName { get; set; }
		public string Roles { get; set; }
		public int Num { get; set; }
	}
}
