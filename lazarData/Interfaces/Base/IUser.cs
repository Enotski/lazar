using TMK.Utils.Interfaces.ExternalSystem;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Пользователь
	/// </summary>
	public interface IUser : IKeyEntity, ISoEasCode, ICreationDate, IFilialRelation, IUserFullName {
		/// <summary>
		/// Эл почта
		/// </summary>
		string Email { get; set; }
	}
}
