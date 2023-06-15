using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Utils {
    /// <summary>
    /// Возвращает результат работы хелпера
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HelperResult<T> : IHelperResult
        where T : class {
        /// <summary>
        /// Объект действия
        /// </summary>
        public T Result { get; protected set; }
        /// <summary>
        /// Результат выполнения
        /// </summary>
        public ResultState State {
            get {
                if (Result != null && Error != null) {
                    return ResultState.Warning;
                }
                if (Result != null && Error == null) {
                    return ResultState.Success;
                }
                return ResultState.Error;
            }
        }
        /// <summary>
        /// Ошибка
        /// </summary>
        [ScriptIgnore]
        public Exception Error { get; protected set; }

        /// <summary>
        /// Сообщение об ошибке лежащее в exception
        /// </summary>
        public string ErrorMsg {
            get {
                return ExceptionHelper.FormatException(Error);
            }
        }
        /// <summary>
        /// Сообщение об ошибке лежащее в exception вместе с stacktrace
        /// </summary>
        public string ErrorMsgWithStackTrace {
            get {
                return ExceptionHelper.FormatException(Error, true);
            }
        }

        /// <summary>
        /// дополнительная информация
        /// </summary>
        public object AdditionalInfo { get; protected set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="res">Результат</param>
        /// <param name="ex">Ошибка</param>
        /// <param name="additionalInfo">Доп. данные</param>
        public HelperResult(T res, Exception ex, object additionalInfo) {
            Result = res;
            Error = ex;
            AdditionalInfo = additionalInfo;
        }
    }

    /// <summary>
    /// Результат выполнения в хелпере
    /// </summary>
    public enum ResultState {
        /// <summary>
        /// Успех
        /// </summary>
        Success,
        /// <summary>
        /// Успешно, но с ошибками
        /// </summary>
        Warning,
        /// <summary>
        /// Ошибка
        /// </summary>
        Error
    }
}
