using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMK.Utils.Interfaces;

namespace TMK.Utils.Extensions {
	/// <summary>
	/// Расширения для работы с флагами доступа к операциям на данными
	/// </summary>
	public static class DataAccessExtension {
		/// <summary>
		/// Обновляем флаги доступа
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <param name="newData"></param>
		public static void UpdateDataAccess<T>(this T entity, IDataAccess newData)
		where T : class, IDataAccess{
			entity.ConfirmData = newData.ConfirmData;
			entity.ExportData = newData.ExportData;
			entity.InputData = newData.InputData;
		}
	}
}
