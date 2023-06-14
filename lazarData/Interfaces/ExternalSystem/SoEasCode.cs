namespace TMK.Utils.Interfaces.ExternalSystem {
	/// <summary>
	/// ID для связи с порталом СО ЕЭС
	/// </summary>
	public interface ISoEasCode {
		/// <summary>
		/// ID на портале АО «СО ЕЭС»
		/// </summary>
		string SoEesPortalId { get; set; }
	}
}
