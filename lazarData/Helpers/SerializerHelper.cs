using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TMK.Utils.Helpers {
	/// <summary>
	/// Помощник в сериализации
	/// </summary>
	public static class SerializerHelper {
		/// <summary>
		/// Сериализация данных
		/// </summary>
		/// <typeparam name="T">Тип</typeparam>
		/// <param name="Error">Ошибки выполнения метода</param>
		/// <param name="FileName">Полный путь к выходному файлу</param>
		/// <param name="ExportedData">Сериализуемые данные</param>
		public static void SerializeData<T>(out Exception Error, string FileName, T ExportedData) {
			Error = null;
			try {
				var FolderPath = Path.GetDirectoryName(FileName);
				if (!Directory.Exists(FolderPath)) {
					Error = new Exception("Указанный каталог не существует");
					return;
				}

				if (System.IO.File.Exists(FileName)) {
					System.IO.File.Delete(FileName);
				}

				XmlTextWriter writer = new XmlTextWriter(FileName, System.Text.Encoding.UTF8);
				writer.Formatting = Formatting.Indented;
				writer.Indentation = 4;
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
				ns.Add("", "");
				serializer.Serialize(writer, ExportedData, ns);
				writer.Close();
			} catch (Exception exp) {
				Error = exp;
			}
		}

		/// <summary>
		/// Десериализация данных
		/// </summary>
		/// <typeparam name="T">Тип данных</typeparam>
		/// <param name="Error">Ошибки выполнения метода</param>
		/// <param name="FileName">Полный путь к входному файлу</param>
		/// <returns></returns>
		public static T DeserializeData<T>(out Exception Error, string FileName) where T : class {
			Error = null;
			try {
				if (!System.IO.File.Exists(FileName)) {
					Error = new Exception("Указанный файл не существует");
					return null;
				}

				XmlReader reader = new XmlTextReader(FileName);
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				T ExportedData = (T)serializer.Deserialize(reader);
				reader.Close();
				return ExportedData;
			} catch (Exception exp) {
				Error = exp;
				return null;
			}
		}
	}
}
