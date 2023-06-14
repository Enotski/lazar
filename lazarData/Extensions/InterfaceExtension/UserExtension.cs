using System;
using System.Collections.Generic;
using System.Linq;
using TMK.Utils.Interfaces.Base;

namespace TMK.Utils.Extensions.InterfaceExtension {
	/// <summary>
	/// Расширение для методов расширения
	/// </summary>
	public static class UserExtension {
		/// <summary>
		/// Получить Пользователя по логину
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">Коллекция или запрос</param>
		/// <param name="login">Искомый логин</param>
		/// <param name="conditions"></param>
		/// <param name="includes"></param>
		/// <returns></returns>
		public static T GetUserByLogin<T>(this IEnumerable<T> source, string login)
			where T : class, IUser {
			if(string.IsNullOrEmpty(login)) {
				return null;
			}

			return source.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
		}
		/// <summary>
		/// Получить Пользователя по логину
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">Коллекция или запрос</param>
		/// <param name="login">Искомый логин</param>
		/// <param name="conditions"></param>
		/// <param name="includes"></param>
		/// <returns></returns>
		public static TViewModel GetUserByLogin<T, TViewModel>(this IQueryable<T> source, string login, Func<T, TViewModel> modelToViewModel)
			where T : class, IUser{
			if(string.IsNullOrEmpty(login)) {
				return default(TViewModel);
			}
			source = source.Where(x => x.Login.ToUpper() == login.ToUpper());
			return source.ToArray().Select(modelToViewModel).FirstOrDefault();
		}

        /// <summary>
		/// Получить Пользователя по id
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">Коллекция или запрос</param>
		/// <param name="login">Искомый логин</param>
		/// <param name="conditions"></param>
		/// <param name="includes"></param>
		/// <returns></returns>
		public static TViewModel GetUserById<T, TViewModel>(this IQueryable<T> source, Guid? Id, Func<T, TViewModel> modelToViewModel)
            where T : class, IUser {
            if (!Id.HasValue) {
                return default(TViewModel);
            }
            source = source.Where(x => x.Id == Id);
            return source.ToArray().Select(modelToViewModel).FirstOrDefault();
        }
    }
}
