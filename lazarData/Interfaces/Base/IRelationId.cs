using System;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Интерфейс на случай, когда связь устанавливается по Id без конкретной сущности
	/// </summary>
	public interface IRelationId {
		/// <summary>
		/// Ид связаноой записи
		/// </summary>
		Guid? RelationId { get; set; }
	}
}
