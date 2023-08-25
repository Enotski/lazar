namespace CommonUtils.Utils {
    public static class Exceptions {
        /// <summary>
        /// Форматированный вывод сообщения об ошибке
        /// </summary>
        /// <param name="exc">Исключение</param>
        /// <param name="AddStackTrace">Включать в вывод трассировку стека</param>
        /// <returns></returns>
        public static string Format(this Exception exc, bool AddStackTrace = true) {
            if (exc == null) {
                return string.Empty;
            }
            string StackTrace = string.Empty;
            if (AddStackTrace && exc.StackTrace != null) {
                string[] tempTrace = exc.StackTrace.Split('\n');
                foreach (string trace in tempTrace) {
                    StackTrace += trace;
                }
            }
            return exc.Message + (exc.InnerException != null ? " (" + Format(exc.InnerException) + ")" : "") + (!string.IsNullOrEmpty(StackTrace) ? ". Трассировки стека:" + StackTrace : "");
        }
    }
}
