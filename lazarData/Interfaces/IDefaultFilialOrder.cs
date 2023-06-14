using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMK.Utils.Interfaces {
    /// <summary>
    /// Сортировка списка филиалов по умолчанию
    /// </summary>
    public interface IDefaultFilialOrder {
        /// <summary>
        /// Порядковый номер филиала для сортировки
        /// </summary>
        int DefaultFilialOrder { get; set; }
        /// <summary>
        /// ИД филиала для сортировки
        /// </summary>
        Guid FilialId { get; set; }
    }
}
