using System;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Связь с пользователем
	/// </summary>
	public interface IUserReference {
		/// <summary>
		/// Ид
		/// </summary>
		Guid UserId { get; set; }
	}
}
