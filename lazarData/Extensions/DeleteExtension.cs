using System.Linq;
using TMK.Utils.Enums;
using TMK.Utils.Interfaces.Base;

namespace TMK.Utils.Extensions {
	/// <summary>
	/// Расширение для удаляемых через флаг сущностей
	/// </summary>
	public static class DeleteExtension {
		/// <summary>
		/// Получить записи по статусу удаления
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">Запрос</param>
		/// <param name="type">Тип</param>
		/// <returns></returns>
		public static IQueryable<T> GetByStatus<T>(this IQueryable<T> query, DeletedSearchType type)
		where T : class, IDeleted {
			if (type == DeletedSearchType.Actual) {
				return query.Where(x => !x.IsDeleted);
			}
			if (type == DeletedSearchType.Deleted) {
				return query.Where(x => x.IsDeleted);
			}       
			return query;
		}

	}
}
