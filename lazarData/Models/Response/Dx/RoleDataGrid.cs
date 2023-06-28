using lazarData.Models.Response.DataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.ViewModels {
	public class RoleDataGrid : DataGridRowResponseModel {
		public string Name { get; set; }
		public int Num { get; set; }
		public string DateChange { get; set; }
	}
}
