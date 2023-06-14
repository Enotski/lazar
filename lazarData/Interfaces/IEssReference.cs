using TMK.Utils.Interfaces.Base;

namespace TMK.Utils.Interfaces {
	/// <summary>
	/// Интерфейс для глобального справочника
	/// </summary>
	public interface IEssReference : INameBase, IDateChange, IEssCode, IChangedUserReference,  IDeleted {

	}
}
