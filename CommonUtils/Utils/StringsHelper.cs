namespace CommonUtils.Utils {
    public static class StringsHelper {
        public static string TrimToUpper(this string str) {
            if (string.IsNullOrWhiteSpace(str)) {
                return str;
            }
            return str.Trim().ToUpper();
        }
    }
}
