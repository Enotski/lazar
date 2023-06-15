using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response.DataGrid {
	public class DataGridResponseModel<TViewModel> : IHelperResult {
		public int totalCount { get; set; } = 0;
		public IEnumerable<TViewModel> data { get; set; } = new List<TViewModel>();
		public string error { get; set; } = string.Empty;
		public ResultState State { get; set; } = ResultState.Success;

		public static DataGridResponseModel<TViewModel> ErrorResponse(Exception exc) {
			return new DataGridResponseModel<TViewModel> {
				State = ResultState.Success,
				error = exc.Message
			};
		}
	}
}
