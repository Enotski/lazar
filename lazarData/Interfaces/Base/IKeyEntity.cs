using System;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Ключ сущности
	/// </summary>
	public interface IKeyEntity {
		/// <summary>
		/// Ключевое поле сущности
		/// </summary>
		Guid Id { get; set; }
	}
}
