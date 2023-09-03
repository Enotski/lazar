namespace CommonUtils.Utils {
    /// <summary>
    /// Helper class for operations with <see cref="string" />
    /// </summary>
    public static class StringsHelper {
        /// <summary>
        /// Aplly <see cref="String.Trim()" /> and <see cref="String.ToUpper()" />
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimToUpper(this string str) {
            if (string.IsNullOrWhiteSpace(str)) {
                return str;
            }
            return str.Trim().ToUpper();
        }
    }
}
