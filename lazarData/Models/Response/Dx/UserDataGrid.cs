using lazarData.Models.Response.DataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.ViewModels {
	public class UserDataGrid : DataGridRowResponseModel {
		public string Login { get; set; }
		public string Email { get; set; }
		public string DateChange { get; set; }
		public string Roles { get; set; }
		public int Num { get; set; }
	}
}
