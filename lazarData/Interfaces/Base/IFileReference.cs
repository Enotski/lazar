using TMK.Utils.Enums;

namespace TMK.Utils.Interfaces.Base {
	/// <summary>
	/// Файл
	/// </summary>
	public interface IFileReference {
		/// <summary>
		/// Расширение файла
		/// </summary>
		FileExtension Extension { get; set; }
		/// <summary>
		/// Файл
		/// </summary>
		byte[] FileData { get; set; }
	}
}
