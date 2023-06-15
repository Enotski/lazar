using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.ViewModels {
	public class RoleDataGrid : DataGridRowResponseModel {
		public string Name { get; set; }
		public string GroupAD { get; set; }
		public bool IsDefault { get; set; }
		public string DateLastEdit { get; set; }
		public string ChangedBy { get; set; }
		public int Num { get; set; }
	}
}
