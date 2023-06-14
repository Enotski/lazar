using System;

namespace TMK.Utils.Interfaces.Base {
	public interface IParentId {
		/// <summary>
		/// Родительский Ид
		/// </summary>
		Guid? ParentId { get; set; }
	}
}
