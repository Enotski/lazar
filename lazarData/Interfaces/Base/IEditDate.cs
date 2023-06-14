using System;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Дата редактирования
	/// </summary>
	public interface IEditDate {
		/// <summary>
		/// Дата последнего редактирования
		/// </summary>
		DateTime DateLastEdit { get; set; }
	}
}
