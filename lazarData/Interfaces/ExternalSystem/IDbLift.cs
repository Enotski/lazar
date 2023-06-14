using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMK.Utils.Interfaces.ExternalSystem {
	public interface IDbLift {
		/// <summary>
		/// Ид внешней системы 
		/// </summary>
		int? DbLiftId { get; set; }
		string Ident { get; set; }
	}
}
