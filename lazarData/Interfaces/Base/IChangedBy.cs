namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Последний редактировавший
	/// </summary>
	public interface IChangedBy {
		/// <summary>
		/// Кем изменено
		/// </summary>
		string ChangedBy { get; set; }
	}
}
