using System;
using TMK.Utils.Interfaces.Base;

namespace TMK.Utils.Extensions {
	/// <summary>
	/// Расширение для имени пользователя
	/// </summary>
	public static class UserFullNameExtension {
		/// <summary>
		/// Получение полного имени пользователя
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static string GetFullName<T>(this T entity)
		where T : class, IUserFullName {
			try {
				if(entity is null)
					return string.Format("");

				return string.Format("{0} {1} {2}", entity?.Surname, entity?.Name, entity?.Patronymic);
			} catch (Exception exp) {
				throw exp;
			}
		}

		/// <summary>
		/// Получение полного имени пользователя с логином
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static string GetFullNameWithLogin<T>(this T entity)
			where T : class, IUserFullName {
			try {
				if (entity == null) {
					return "";
				}
				var name = entity.GetFullName();
				if (!string.IsNullOrEmpty(entity?.Login)) {
					name += " (" + entity?.Login + ")";
				} else {
					throw new Exception("Логин пользователя не задан");
				}
				return name;
			} catch (Exception ex) {
				throw ex;
			}
		}

		/// <summary>
		/// Получение полного имени пользователя
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static string GetShortName<T>(this T entity)
			where T : class, IUserFullName {
			try {
				if (entity == null) {
					return "";
				}
				return string.Format("{0} {1}.{2}",
					entity.Surname,
					string.IsNullOrEmpty(entity.Name)? "" : entity.Name.Substring(0, 1),
					string.IsNullOrEmpty(entity.Patronymic) ? "" : entity.Patronymic.Substring(0, 1) + ".");
			} catch (Exception ex) {
				throw ex;
			}
		}
		/// <summary>
		/// Получение фамилии с инициалами и логин
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static string GetShortNameWithLogin<T>(this T entity)
			where T : class, IUserFullName {
			try {
				if (entity == null) {
					return "";
				}
				if (!string.IsNullOrWhiteSpace(entity.Name) && !string.IsNullOrWhiteSpace(entity.Patronymic) && !string.IsNullOrWhiteSpace(entity.Surname)) {
					return entity.Surname + ' ' + entity.Name.Substring(0, 1) + '.' + entity.Patronymic.Substring(0, 1) + '.' + ' ' + entity.Login;
				} else if (string.IsNullOrWhiteSpace(entity.Name) && !string.IsNullOrWhiteSpace(entity.Patronymic) && !string.IsNullOrWhiteSpace(entity.Surname)) {
					return entity.Surname + ' ' + entity.Patronymic + '.' + ' ' + entity.Login;
				} else if (!string.IsNullOrWhiteSpace(entity.Name) && string.IsNullOrWhiteSpace(entity.Patronymic) && !string.IsNullOrWhiteSpace(entity.Surname)) {
					return entity.Surname + ' ' + entity.Name.Substring(0, 1) + '.' + ' ' + entity.Login;
				} else if (!string.IsNullOrWhiteSpace(entity.Name) && !string.IsNullOrWhiteSpace(entity.Patronymic) && string.IsNullOrWhiteSpace(entity.Surname)) {
					return entity.Name.Substring(0, 1) + '.' + entity.Patronymic.Substring(0, 1) + '.' + ' ' + entity.Login;
				} else if (string.IsNullOrWhiteSpace(entity.Name) && string.IsNullOrWhiteSpace(entity.Patronymic) && !string.IsNullOrWhiteSpace(entity.Surname)) {
					return entity.Surname + ' ' + entity.Login;
				}
				return string.Empty;
			} catch (Exception ex) {
				throw ex;
			}
		}
	}
}
