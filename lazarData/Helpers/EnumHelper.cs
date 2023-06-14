using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using TMK.Utils.Attributes;
using TMK.Utils.Enums;

namespace TMK.Utils.Helpers {
	/// <summary>
	/// Работа с enum
	/// </summary>
	public static class EnumHelper {
		/// <summary>
		/// Представить enum в виде списка ключ-значение
		/// </summary>
		/// <typeparam name="TEnum"></typeparam>
		/// <returns></returns>
		public static Dictionary<int, string> GetListParam<TEnum>(bool showAll = false)
			where TEnum : struct, IConvertible {
			Dictionary<int, string> res = new Dictionary<int, string>();
			foreach (Enum en in Enum.GetValues(typeof(TEnum))) {
				FieldInfo fi = en.GetType().GetField(en.ToString());

				if (!showAll) {
					var attr = fi.GetCustomAttribute<IgnoreDescriptionAttribute>();
					if (attr != null) {
						continue;
					}
				}

				int key = -1;
				key = (int)Enum.Parse(typeof(TEnum), en + "", true);
				if (key == -1) {
					continue;
				}
				res.Add(key, en.GetDescription());
			}

			return res;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetEnumDescription(Enum value) {
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes =
				(DescriptionAttribute[])fi.GetCustomAttributes(
				typeof(DescriptionAttribute),
				false);

			if (attributes.Length > 0)
				return attributes[0].Description;
			else
				return value.ToString();
		}
		/// <summary>
		/// Получить описание из атрибута Description
		/// </summary>
		/// <param name="value">Enum</param>
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
		/// <summary>
		/// Получить описание из атрибута Description
		/// </summary>
		/// <param name="value">Enum</param>
		/// <returns></returns>
		public static bool TryGetDescription(this Enum value, out string result) {
			result = "";
			try {
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

				return true;
			} catch {
				return false;
			}
		}
		/// <summary>
		/// Получает все перечисленные значения, если перечисление содержит их несколько
		/// </summary>
		/// <param name="value">перечисление</param>
		/// <returns></returns>
		public static int[] GetArrayValues(this Enum value) {
			try {
				List<int> result = new List<int>();
				string[] values = null;
				values = (value + "").Contains(",")
					? (value + "").Split(',')
					.Select(x => x.Trim())
					.ToArray()
					: new[] { value.ToString() };

				foreach (var val in values) {
					FieldInfo fi = value.GetType().GetField(val);
					result.Add((int)fi.GetValue(fi));
				}

				return result.ToArray();
			} catch {
				return new int[0];
			}
		}

		/// <summary>
		/// Вытаскивает все описания по перечислению
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetStringValues(this Enum value) {
			try {
				string result = "";
				string[] values = null;
				values = (value + "").Contains(",")
					? (value + "").Split(',')
					.Select(x => x.Trim())
					.ToArray()
					: new[] { value.ToString() };


				foreach (var val in values) {
					FieldInfo fi = value.GetType().GetField(val);
					result += (int)fi.GetValue(fi) + ",";
				}

				return result.Substring(0, result.Length - 1);
			} catch {
				return null;
			}
		}
		/// <summary>
		/// Получить по строке в атрибуте Description перечисление
		/// </summary>
		/// <typeparam name="TEnum">Перечисление</typeparam>
		/// <param name="descr">Описание</param>
		/// <returns></returns>
		public static TEnum GetEnumByDescription<TEnum>(this string descr)
			where TEnum : struct, IConvertible {
			var tEn = typeof(TEnum);

			foreach (var field in tEn.GetFields()) {
				var attribute = Attribute.GetCustomAttribute(field,
				typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (attribute != null) {
					if (attribute.Description.ToLower() == descr.ToLower()) {
						return (TEnum)field.GetValue(null);
					}
					if (field.Name.ToLower() == descr.ToLower()) {
						return (TEnum)field.GetValue(null);
					}
				}
			}
			return new TEnum();
			//throw new InvalidEnumArgumentException("Не удалось найти сопоставление с указанным типом enum");
		}
		/// <summary>
		/// Получить по строке в атрибуте Description перечисление
		/// </summary>
		/// <typeparam name="TEnum">Перечисление</typeparam>
		/// <param name="descr">Описание</param>
		/// <returns></returns>

		/// <summary>
		/// Получить по строковому виду значение перечисления
		/// </summary>
		/// <typeparam name="TEnum">Перечисление</typeparam>
		/// <param name="str">Строковое значение</param>
		/// <returns></returns>
		public static TEnum GetEnumByString<TEnum>(this string str)
			where TEnum : struct, IConvertible {
			var tEn = typeof(TEnum);

			foreach (var field in tEn.GetEnumValues()) {
				var en = (TEnum)field;
				if ((en + "").ToUpper() == str.ToUpper()) {
					return en;
				}
			}
			return new TEnum();
		}
		/// <summary>
		/// Получить по строковому виду значение перечисления
		/// </summary>
		/// <typeparam name="TEnum">Перечисление</typeparam>
		/// <param name="str">Строковое значение</param>
		/// <returns></returns>
		public static TEnum GetEnumByStringNum<TEnum>(this string num)
			where TEnum : struct, IConvertible {
			var tEn = typeof(TEnum);

			int sKey = 0;
			if (int.TryParse(num, out sKey)) {
				foreach (var field in tEn.GetEnumValues()) {
					var en = (TEnum)field;
					var key = (int)Enum.Parse(typeof(TEnum), en + "");
					if (key == sKey) {
						return en;
					}
				}
			}
			return new TEnum();
		}
	}
}
