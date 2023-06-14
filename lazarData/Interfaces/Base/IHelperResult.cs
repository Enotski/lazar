using System;
using System.Web.Script.Serialization;
using TMK.Utils.Helpers;
using TMK.Utils.Models.Result;

namespace TMK.Utils.Interfaces.Base
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