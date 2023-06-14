using System;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Связь с пользователем изменившим запись
	/// </summary>
	public interface IChangedUserReference{
		/// <summary>
		/// Ид изменившего пользователя
		/// </summary>
		Guid? ChangedUserId { get; set; }
	}
}
