using System;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Связь с пользователем
	/// </summary>
	public interface IUserOptionalReference {
		/// <summary>
		/// Ид
		/// </summary>
		Guid? UserId { get; set; }
	}
}
