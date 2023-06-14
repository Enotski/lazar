using System.ComponentModel;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Интерфейс для не физического удаления
	/// </summary>
	public interface IDeleted {
		/// <summary>
		/// Флаг о удалении
		/// </summary>
		[DefaultValue(false)]
		bool IsDeleted { get; set; }
	}
}
