using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Типы, в которых отслеживается текущий редактирующий пользователь
	/// </summary>
	public interface ICurrentEditUser {
		/// <summary>
		/// Ид редактирующего пользователя
		/// </summary>
		Guid? CurrentEditUserId { get; set; }
	}
}
