using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Utils
{
    internal static class ExceptionHelper
    {
        /// <summary>
        /// Преобразует Exception в строку
        /// </summary>
        /// <param name="exp">Исключение</param>
        /// <returns></returns>
        public static string FormatException(Exception exp, bool isStackTrace = false)
        {
            var msg = "";
            if (exp == null)
            {
                return msg;
            }


            while (exp != null)
            {
                var stTraceMsg = isStackTrace && !string.IsNullOrEmpty(exp.StackTrace)
                    ? " ( StackTrace:" + exp.StackTrace + ")" : string.Empty;
                msg = exp.Message + stTraceMsg + ";" + msg;

                if (exp.InnerException != null)
                {
                    exp = exp.InnerException;
                }
                else
                {
                    exp = null;
                }
            }
            return msg;
        }
    }
}
