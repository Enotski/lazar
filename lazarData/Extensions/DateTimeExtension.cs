using System;
using System.Runtime.CompilerServices;

namespace TMK.Utils.Extensions {
	/// <summary>
	/// Расширения для дат
	/// </summary>
	public static class DateTimeExtension {
		/// <summary>
		/// Преобразование даты в формат, необходимый для DataGrid
		/// </summary>
		public static string ToDataGridFormat(this DateTime dateTime) {
			if (dateTime == null)
				return "";
			return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
		}
		/// <summary>
		/// Убрать милисекунды из даты и времени
		/// </summary>
		/// <param name="dateTime">Исходная дата со временем, м.б. null</param>
		/// <returns>Исходная дата и время с обрезанными милисекундами; если исходная null, то вернет null</returns>
		public static DateTime? TruncateMilliseconds(this DateTime? dateTime) {
			if (dateTime.HasValue) {
				DateTime dt = dateTime.Value;
				return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Kind);
			}
			else
				return dateTime;
		}
	}
}