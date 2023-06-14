using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TMK.Utils.Extensions {
	/// <summary>
	/// Расширения для коллекций
	/// </summary>
	public static class CollectionExtension {
		/// <summary>
		/// Преобразует указанные поля из UTC в LocalTime
		/// </summary>
		/// <typeparam name="T">Тип сущности</typeparam>
		/// <param name="collection">Коллекция объектов</param>
		/// <param name="fields">Поля для преобразования</param>
		/// <returns></returns>
		public static IEnumerable<T> ToLocalTime<T>(this IEnumerable<T> collection, params Expression<Func<T, DateTime?>>[] fields) 
			where T : class{
			try {
				foreach(var item in collection) {
					foreach(var field in fields) {
						var date = field.Compile().Invoke(item);
						if(!date.HasValue) {
							continue;
						}
						date = date.Value.ToLocalTime();
						MemberInfo prop = null;
						if(field.Body is UnaryExpression) {
							prop = ((MemberExpression)((UnaryExpression)field.Body).Operand).Member;
						} else {
							prop = ((MemberExpression)field.Body).Member;
						}
						var typeParam = Expression.Parameter(typeof(T));
						var valueParam = Expression.Parameter(typeof(DateTime));
						var setter = Expression.Lambda<Action<T, DateTime>>(
							Expression.Assign(
								Expression.MakeMemberAccess(typeParam, prop),
									valueParam), typeParam, valueParam).Compile();
						setter(item, date.Value);
					}
				}
				return collection;
			}catch(Exception ex) {
				throw ex;
			}
		}

		/// <summary>
		/// Расширение Contains который вытаскивает записи группами
		/// </summary>
		/// <typeparam name="TEntity">Тип сущности</typeparam>
		/// <typeparam name="TKey">Тип ключа</typeparam>
		/// <param name="source">Источник</param>
		/// <param name="proper">Свойство для сравнения в contains</param>
		/// <param name="ids">Список идшников</param>
		/// <param name="countByStep">Кол-во записей за шаг</param>
		/// <returns></returns>
		public static IList<TEntity> ContainsByStep<TEntity, TKey>(this IQueryable<TEntity> source,
			Expression<Func<TEntity, TKey>> proper,
			IEnumerable<TKey> ids,
			int countByStep = 1000)
			where TEntity : class
			where TKey : struct {

			List<TEntity> res = new List<TEntity>();
			try {
				MethodInfo method = typeof(Enumerable).
					GetMethods().
					Where(x => x.Name == "Contains").
					Single(x => x.GetParameters().Length == 2).
					MakeGenericMethod(typeof(TKey));

				var step = 0;
				var stepIds = ids.Skip(step * countByStep).Take(countByStep).ToArray();
				while(stepIds.Length > 0) {
					var arr = Expression.Constant(stepIds);
					var predicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Call(method, new Expression[] { arr, proper.Body }), proper.Parameters);
					res.AddRange(source.Where(predicate).ToArray());

					step++;
					stepIds = ids.Skip(step * countByStep).Take(countByStep).ToArray();
				}

				return res;
			} catch(Exception ex) {
				throw ex;
			}
		}

		public static IList<TEntity> DistinctBy<TEntity, TKey>(this IEnumerable<TEntity> source,
			Expression<Func<TEntity, TKey>> proper)
			where TEntity : class
			where TKey : class {
			try {
				var result = source.AsQueryable().GroupBy(proper).Select(x => x.First()).ToList();
				return result;
			} catch (Exception exp) {
				throw exp;
			}
		}
	}
}
