using lazarData.Models.Response.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.ResponseModels.Dx.Base
{
    /// <summary>
    /// Базовый класс для строки таблицы
    /// </summary>
    public class DataGridRowResponseModel : BaseResponseModel
    {
        /// <summary>
        /// Ид записи
        /// </summary>
        public Guid Id { get; set; }
    }
}
