using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMK.Utils.Interfaces.Base {
	public interface IMainKey {
		/// <summary>
		/// Родительский Ид
		/// </summary>
		 Guid MainId { get; set; }	
	}
}
