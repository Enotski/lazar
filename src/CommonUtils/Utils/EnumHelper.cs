using System.ComponentModel;
using System.Reflection;

namespace CommonUtils.Utils {
    /// <summary>
    /// Helper class for operations with <see cref="Enum" />
    /// </summary>
    public static class EnumHelper {
        /// <summary>
        /// Represent enum as a key-value list
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static Dictionary<int, string> GetListParam<TEnum>()
            where TEnum : struct, IConvertible
        {
            Dictionary<int, string> res = new Dictionary<int, string>();
            foreach (Enum en in Enum.GetValues(typeof(TEnum)))
            {
                FieldInfo fi = en.GetType().GetField(en.ToString());

                var key = (int)Enum.Parse(typeof(TEnum), en + "", true);
                if (key == -1)
                {
                    continue;
                }
                res.Add(key, en.GetDescription());
            }

            return res;
        }
        /// <summary>
        /// Get description from <see cref="DescriptionAttribute" /> attribute
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value) {
			try {
				if (value == null) return "";

				string result = "";
				string[] values = null;
				values = (value + "").Contains(",")
					? (value + "").Split(',')
					.Select(x => x.Trim())
					.ToArray()
					: new[] { value.ToString() };

				foreach (var val in values) {
					FieldInfo fi = value.GetType().GetField(val);

					DescriptionAttribute[] attributes =
					(DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
					if (result != "") {
						result += "; ";
					}

					if (attributes.Length > 0) {
						result += attributes[0].Description;
					} else {
						result += val;
					}
				}

				return result;
			} catch {
				return "";
			}
		}
	}
}
