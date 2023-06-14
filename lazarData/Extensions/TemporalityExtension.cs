using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMK.Utils.Enums;
using TMK.Utils.Interfaces.Base;

namespace TMK.Utils.Extensions {
	/// <summary>
	/// Расширение для работы с датами
	/// </summary>
	public static class TemporalityExtension {
		/// <summary>
		/// Запрос с поиском по периоду
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="getDate"></param>
		/// <param name="fromDate"></param>
		/// <param name="toDate"></param>
		/// <returns></returns>
		public static IQueryable<T> WhereDateBetweenUtc<T>(this IQueryable<T> source,
		Expression<Func<T, DateTime>> getDate,
		DateTime? fromDate, DateTime? toDate) {
			if(fromDate == null && toDate == null)
				return source;

			if(toDate.HasValue) {
				toDate = toDate.Value.AddDays(1);
				toDate = toDate.Value.ToUniversalTime();
			}
			if(fromDate.HasValue) {
				fromDate = fromDate.Value.ToUniversalTime();
			}

			var predicate = DateBetween(fromDate, toDate);
			return source.Where(getDate.Chain(predicate));
		}
		/// <summary>
		/// Запрос с поиском по периоду
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="getDate"></param>
		/// <param name="fromDate"></param>
		/// <param name="toDate"></param>
		/// <returns></returns>
		public static IQueryable<T> WhereDateBetweenUtc<T>(this IQueryable<T> source,
		Expression<Func<T, DateTime?>> getDate,
		DateTime? fromDate, DateTime? toDate) {
			if(fromDate == null && toDate == null)
				return source;

			if(toDate.HasValue) {
				toDate = toDate.Value.AddDays(1);
				toDate = toDate.Value.ToUniversalTime();
			}
			if(fromDate.HasValue) {
				fromDate = fromDate.Value.ToUniversalTime();
			}

			var predicate = DateNullBetween(fromDate, toDate);
			return source.Where(getDate.Chain(predicate));
		}

		public static Expression<Func<TIn, TOut>> Chain<TIn, TInterstitial, TOut>(
			this Expression<Func<TIn, TInterstitial>> inner,
			Expression<Func<TInterstitial, TOut>> outer) {
			var visitor = new SwapVisitor(outer.Parameters[0], inner.Body);
			return Expression.Lambda<Func<TIn, TOut>>(visitor.Visit(outer.Body), inner.Parameters);
		}

		/// <summary>
		/// Задаем ключ для темпоральной сущности
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">Сущность</param>
		/// <param name="id">ИД</param>
		/// <param name="date">Дата</param>
		/// <returns></returns>
		public static T SetKeyUtc<T>(this T obj, Guid? id = null, DateTime? date = null)
	where T : class, IDateChange, IKeyEntity {
			try {
				if(!date.HasValue) {
					date = DateTime.UtcNow;
				}
				if(!id.HasValue) {
					id = Guid.NewGuid();
				}
                //Убирало милисекунды
				//date = date.Value.AddMilliseconds(-date.Value.Millisecond);
				obj.Id = id.Value;
				obj.DateChange = date.Value;
				return obj;
			} catch(Exception ex) {
				return default(T);
			}
		}
		/// <summary>
		/// Удаление узла дерева темпоральной сущности
		/// </summary>
		/// <typeparam name="T">Тип сущности</typeparam>
		/// <param name="obj">Сущность</param>
		/// <param name="parent">Родительская сущность</param>
		/// <param name="isDeleted">Удалена ли</param>
		/// <returns></returns>
		public static T RemoveTemporaryTree<T>(this T obj, T parent = null, bool isDeleted = true)
			where T : class, IDateChange, IKeyEntity, IDeleted, IParentDateChange, IParentId,IChangedUserReference {
			try {
				obj = obj.SetKeyUtc(obj.Id);

				if(parent != null) {
					obj.ParentDateChange = parent.DateChange;
					obj.ParentId = parent.Id;
                    obj.ChangedUserId = parent.ChangedUserId;
				}

				obj.IsDeleted = isDeleted;

				return obj;
			} catch(Exception ex) {
				return null;
			}
		}
		/// <summary>
		/// Удаление темпоральной сущности
		/// </summary>
		/// <typeparam name="T">Тип сущности</typeparam>
		/// <param name="obj">Сущность</param>
		/// <param name="isDeleted">Удалена ли</param>
		/// <returns></returns>
		public static T RemoveTemporary<T>(this T obj, Guid? userId = null, bool isDeleted = true)
			where T : class, IDateChange, IKeyEntity, IDeleted {
			try {
				obj = obj.SetKeyUtc(obj.Id);
				obj.IsDeleted = isDeleted;

				if(obj is IChangedUserReference) {
					(obj as IChangedUserReference).ChangedUserId = userId ?? Guid.Empty;
				}

				return obj;
			} catch(Exception ex) {
				return null;
			}
		}
		/// <summary>
		/// Удаление темпоральной сущности
		/// </summary>
		/// <typeparam name="T">Тип сущности</typeparam>
		/// <param name="obj">Сущность</param>
		/// <param name="isDeleted">Удалена ли</param>
		/// <returns></returns>
		public static T RemoveTemporaryWithChangedUser<T>(this T obj, Guid? userId, bool isDeleted = true)
			where T : class, IDateChange, IKeyEntity, IDeleted, IChangedUserReference {
			try {
				obj = obj.SetKeyUtc(obj.Id);
				obj.ChangedUserId = userId ?? Guid.Empty;
				obj.IsDeleted = isDeleted;

				return obj;
			} catch(Exception ex) {
				return null;
			}
		}
		/// <summary>
		/// Удаление множества темпоральных сущностей
		/// </summary>
		/// <typeparam name="T">Тип сущности</typeparam>
		/// <param name="query">Запрос</param>
		/// <returns></returns>
		public static IEnumerable<T> RemoveTemporaryRange<T>(this IQueryable<T> query, Guid? userId)
			where T : class, IDateChange, IKeyEntity, IDeleted {
			try {
				List<T> result = new List<T>();
				var oldOrders = query.ToArray();
				result.AddRange(oldOrders.Select(x => x.RemoveTemporary(userId)));
				return result;
			} catch(Exception ex) {
				throw ex;
			}
		}
		/// <summary>
		/// Получить актуальные темпоральные сущности
		/// </summary>
		/// <typeparam name="T">Тип сущности</typeparam>
		/// <param name="query">Запрос</param>
		/// <returns></returns>
		public static IQueryable<T> GetAllTemporary<T>(this IQueryable<T> query)
			where T : class, IDateChange, IKeyEntity {
			try {
				query = query.Join(
                            query.Select(m => new { m.Id, m.DateChange})
                                .GroupBy(m => m.Id)
                                .Select(m => new { m.Key, LastDataChange = m.Max(k => k.DateChange) }),
				            SourceData => new { Key = SourceData.Id, LastDataChange = SourceData.DateChange },
				            KeyData => new { KeyData.Key, KeyData.LastDataChange },
				            (SourceData, KeyData) => SourceData);
				return query;
			} catch(Exception ex) {
				throw ex;
			}
		}
        //public static IQueryable<T> GetAllTemporarys<T>(this IQueryable<T> query)
        //    where T : class, IDateChange,IGroupTemporaryReference {
        //    try {
        //        query = query.Join(query.
        //        Select(m => new {
        //            m.SecondId,
        //            m.FirstId,
        //            m.DateChange
        //        }).GroupBy(m => m.FirstId).
        //        Select(m => new { m.Key, second = m.GroupBy(g=>g.SecondId) }).Select(s=>new {s.Key }),

        //        SourceData => new { Key = SourceData.FirstId, LastDataChange = SourceData.DateChange },
        //        KeyData => new { KeyData.Key, KeyData.LastDataChange },
        //        (SourceData, KeyData) => SourceData);

        //        return query;
        //    } catch (Exception ex) {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Получить актуальные на заданную дату/время записи справочника
        /// </summary>
        /// <typeparam name="T">Тип сущности (IKeyEntity, IActualDate) </typeparam>
        /// <param name="query">Запрос</param>
        /// <param name="actualTime">Дата/время актуальности, если не указана, берется DateTime.UtcNow</param>
        /// <returns>Модифицированный запрос для выбора только актуальных данных</returns>
        public static IQueryable<T> GetActual<T>(this IQueryable<T> query, DateTime? actualTime = null)
            where T : class, IKeyEntity, IActualDate {
            try {
				if (!actualTime.HasValue)
					actualTime = DateTime.UtcNow;
                query = query.Join(
                            query.Where(x => x.ActualDate <= actualTime)
								.Select(m => new { m.Id, m.ActualDate })
                                .GroupBy(m => m.Id)
                                .Select(m => new { m.Key, LastDate = m.Max(k => k.ActualDate) }),
                            SourceData => new { Key = SourceData.Id, LastDate = SourceData.ActualDate },
                            KeyData => new { KeyData.Key, KeyData.LastDate },
                            (SourceData, KeyData) => SourceData);
                return query;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Получить записи определенного типа
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="query">Запрос</param>
        /// <param name="type">Статус сущностей</param>
        /// <returns></returns>
        public static IQueryable<T> GetAllTemporaryByStatus<T>(this IQueryable<T> query, DeletedSearchType type = DeletedSearchType.Actual)
			where T : class, IDateChange, IKeyEntity, IDeleted {
			try {
				return query.GetAllTemporary().GetByStatus(type);
			} catch(Exception ex) {
				throw ex;
			}
		}

		/// <summary> 
		/// Вычисляет, находится ли значение в пределах заданного промежутка включительно 
		/// </summary> 
		/// <typeparam name="T">Простой тип(int, DateTime и т.д.)</typeparam> 
		/// <param name="value">Занчение</param> 
		/// <param name="from">Начало диапазона</param> 
		/// <param name="to">Конец диапазона</param> 
		/// <returns></returns> 
		public static bool BetweenUtc(this DateTime value, Nullable<DateTime> from, Nullable<DateTime> to) {
			if(!from.HasValue && !to.HasValue) {
				return true;
			}

			if(to.HasValue) {
				to = to.Value.AddDays(1);
				to = to.Value.ToUniversalTime();
			}
			if(from.HasValue) {
				from = from.Value.ToUniversalTime();
			}

			if(from.HasValue && !to.HasValue) {
				return value.CompareTo(from.Value) >= 0;
			} else if(to.HasValue && !from.HasValue) {
				return value.CompareTo(to.Value) <= 0;
			} else {
				return value.CompareTo(from.Value) >= 0 && value.CompareTo(to.Value) <= 0;
			}
		}

		/// <summary> 
		/// Выражение для выбора записей в промежутке 
		/// </summary> 
		/// <param name="fromDate"></param> 
		/// <param name="toDate"></param> 
		/// <returns></returns> 
		private static Expression<Func<DateTime, bool>> DateBetween(DateTime? fromDate, DateTime? toDate) {

			if(!toDate.HasValue && !fromDate.HasValue) {
				return date => true;
			}

			if(toDate == null) {
				return date => fromDate <= date;
			}

			if(fromDate == null) {
				return date => toDate >= date;
			}

			return date => fromDate <= date && toDate >= date;
		}
		/// <summary> 
		/// Выражение для выбора записей в промежутке 
		/// </summary> 
		/// <param name="fromDate"></param> 
		/// <param name="toDate"></param> 
		/// <returns></returns> 
		private static Expression<Func<DateTime?, bool>> DateNullBetween(DateTime? fromDate, DateTime? toDate) {
			if(!toDate.HasValue && !fromDate.HasValue) {
				return date => true;
			}

			if(toDate == null) {
				return date => date.HasValue && fromDate <= date;
			}

			if(fromDate == null) {
				return date => date.HasValue && toDate >= date;
			}

			return date => date.HasValue && fromDate <= date && toDate >= date;
		}
	}

	internal class SwapVisitor : ExpressionVisitor {
		private readonly Expression _source, _replacement;

		public SwapVisitor(Expression source, Expression replacement) {
			_source = source;
			_replacement = replacement;
		}

		public override Expression Visit(Expression node) {
			return node == _source ? _replacement : base.Visit(node);
		}
	}
}
