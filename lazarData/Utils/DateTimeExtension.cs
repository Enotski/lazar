using System;
using System.Runtime.CompilerServices;

namespace lazarData.Utils
{
    /// <summary>
    /// Расширения для дат
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Преобразование даты в формат, необходимый для DataGrid
        /// </summary>
        public static string ToDataGridFormat(this DateTime dateTime)
        {
            if (dateTime == null)
                return "";
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
        }
    }
}