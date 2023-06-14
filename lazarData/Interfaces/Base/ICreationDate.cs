using System;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Дата создания записи
	/// </summary>
	public interface ICreationDate {
		/// <summary>
		/// Дата создания
		/// </summary>
		DateTime CreationDate { get; set; }
	}
}
