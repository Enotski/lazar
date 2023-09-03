namespace CommonUtils.Utils {
    /// <summary>
    /// Helper class for operations with <see cref="Exceptions" />
    /// </summary>
    public static class Exceptions {
        /// <summary>
        /// Formatted error message output
        /// </summary>
        /// <param name="exc">Exception</param>
        /// <param name="AddStackTrace">Include stack traces in output</param>
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
