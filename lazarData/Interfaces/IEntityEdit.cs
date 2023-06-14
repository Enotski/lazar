using TMK.Utils.Interfaces.Base;

namespace TMK.Utils.Interfaces {
	/// <summary>
	/// Для сущностей которым нужно помнить дату и кем отредактировано
	/// </summary>
	public interface IEntityEdit : IDateChange, IChangedUserReference {
	}
}
