using lazarData.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Interfaces
{
    /// <summary>
    /// Интерфейс результата
    /// </summary>
    public interface IHelperResult
    {
        /// <summary>
        /// Состояние выполнения
        /// </summary>
        ResultState State { get; }
    }
}
