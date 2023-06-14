using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMK.Utils.Interfaces {
	/// <summary>
	/// Доступ к операциям над данными
	/// </summary>
	public interface IDataAccess {
		/// <summary>
		/// Ввод Данных
		/// </summary>
		bool InputData { get; set; }
		/// <summary>
		/// Подтверждение
		/// </summary>
		bool ConfirmData { get; set; }
		/// <summary>
		/// Экспорт
		/// </summary>
		bool ExportData { get; set; }
	}
}
