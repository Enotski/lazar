using System;
using System.Linq;
using TMK.Utils.Interfaces.Base;
using TMK.Utils.Interfaces.ExternalSystem;
namespace TMK.Utils.Extensions {
	/// <summary>
	/// расширени для сущностей с наименованием
	/// </summary>
	public static class NameExtension {
		/// <summary>
		/// Проверяем на уникальность имени
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">Запрос</param>
		/// <param name="name">Наименование</param>
		/// <param name="id">ИД записи</param>
		/// <returns></returns>
		public static bool IsUniqueName<T>(this IQueryable<T> query, string name, Guid? id = null)
			where T : class, IKeyEntity, IName {
			try {
				if(string.IsNullOrEmpty(name)) {
					return false;
				}
				if(id.HasValue) {
					query = query.Where(x => x.Id != id);
				}
				return !query.Where(x => !string.IsNullOrEmpty(x.Name)).Any(x => x.Name.Trim().ToUpper() == name.Trim().ToUpper());
			}catch(Exception ex) {
				throw ex;
			}
		}
		/// <summary>
		/// Проверяем на уникальность имени если есть одинаковые имена проверяем на внешний ключ DbLiftId 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="name"></param>
		/// <param name="DbLiftId"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public static bool IsUniqueNameByDbLiftId<T>(this IQueryable<T> query, string name,int? DbLiftId, Guid? id = null)
			where T : class, IKeyEntity, IName,IDbLift {
			try {
				if (string.IsNullOrEmpty(name)) {
					return false;
				}
				if (id.HasValue) {
					query = query.Where(x => x.Id != id);
				}
				return !query.Where(x =>x.DbLiftId== DbLiftId && !string.IsNullOrEmpty(x.Name)).Any(x => x.Name.Trim().ToUpper() == name.Trim().ToUpper());
			} catch (Exception ex) {
				throw ex;
			}
		}
		/// <summary>
		/// Проверяем на уникальность аббревиатуры
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">Запрос</param>
		/// <param name="name">Наименование</param>
		/// <param name="id">ИД записи</param>
		/// <returns></returns>
		public static bool IsUniqueIdent<T>(this IQueryable<T> query, string name, Guid? id = null)
			where T : class, IKeyEntity, IName,IDbLift {
			try {
				if(string.IsNullOrEmpty(name)) {
					return false;
				}
				if(id.HasValue) {
					query = query.Where(x => x.Id != id);
				}
				return !query.Where(x => !string.IsNullOrEmpty(x.Ident)).Any(x => x.Ident.Trim().ToUpper() == name.Trim().ToUpper());
			}catch(Exception ex) {
				throw ex;
			}
		}
        /// <summary>
		/// Проверяем на уникальность шифр
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">Запрос</param>
		/// <param name="name">Наименование</param>
		/// <param name="id">ИД записи</param>
		/// <returns></returns>
		public static bool IsUniqueShifr<T>(this IQueryable<T> query, string name, Guid? id = null)
			where T : class, IKeyEntity, IName,IShifr {
			try {
				if(string.IsNullOrEmpty(name)) {
					return false;
				}
				if(id.HasValue) {
					query = query.Where(x => x.Id != id);
				}
				return !query.Where(x => !string.IsNullOrEmpty(x.Shifr)).Any(x => x.Shifr.Trim().ToUpper() == name.Trim().ToUpper());
			}catch(Exception ex) {
				throw ex;
			}
		}
        /// <summary>
		/// Проверяем на уникальность имени
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">Запрос</param>
		/// <param name="num">Номер</param>
		/// <param name="id">ИД записи</param>
		/// <returns></returns>
		public static bool IsUniqueDBLiftKey<T>(this IQueryable<T> query, string num, Guid? id = null)
            where T : class, IKeyEntity, IDbLift {
            try {
                if (string.IsNullOrEmpty(num)) {
                    return false;
                }
                int temp = 0;
                if (!int.TryParse(num, out temp)) {
                    return false;
                }
                if (id.HasValue) {
                    query = query.Where(x => x.Id != id);
                }
                return !query.Where(x => x.DbLiftId.HasValue).Any(x => x.DbLiftId == temp);
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
