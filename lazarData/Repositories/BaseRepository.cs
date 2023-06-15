using lazarData.Interfaces;
using lazarData.Models.Response.ViewModels;
using LazarData.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LazarData.Repositories
{
    public abstract class BaseRepository<TViewModel, TModel>
            where TViewModel : BaseResponseModel
            where TModel : class {
        private LazarContext _context;
        /// <summary>
        /// Контекст
        /// </summary>
        public LazarContext Context => _context ?? (_context = new LazarContext(new DbContextOptions<LazarContext>()));
        /// <summary>
        /// Преобразовывает сущность из базы данных в модель представления
        /// </summary>
        /// <returns></returns>
        public abstract Func<TModel, TViewModel> ModelToViewModel();

        /// <summary>
        /// Получить все записи в таблице
        /// </summary>
        /// <param name="isNoTracking">Не отслеживать изменения</param>
        /// <returns></returns>
        public IQueryable<FModel> GetAll<FModel>(bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel {
            try {
                if (Context == null) {
                    throw new ArgumentNullException("Контекст не инициализирован");
                }
                var query = Context.Set<FModel>().AsQueryable();
                if (includes != null && includes.Length > 0) {
                    foreach (var include in includes) {
                        query = query.Include(include);
                    }
                }
                if (isNoTracking) {
                    return query.AsNoTracking();
                }
                return query;
            } catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// Получить запись по ИД
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="Id">ИД записи</param>
        /// <param name="isNoTracking">Отслеживать изменения</param>
        /// <returns></returns>
        public virtual TViewModel GetViewById<FModel>(Guid Id, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IKeyEntity {
            try {
                var query = GetAll<FModel>(isNoTracking, includes);
                var order = query.Where(x => x.Id == Id).Select(ModelToViewModel()).FirstOrDefault();
                return order;
            } catch (Exception exp) {
                return null;
            }
        }
        /// <summary>
        /// Получить запись по ИД
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="Id">ИД записи</param>
        /// <param name="isNoTracking">Отслеживать изменения</param>
        /// <returns></returns>
        public virtual FModel GetEntityById<FModel>(Guid Id, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IKeyEntity {
            try {
                var query = GetAll<FModel>(isNoTracking, includes);
                var order = query.FirstOrDefault(x => x.Id == Id);
                return order;
            } catch (Exception exp) {
                return null;
            }
        }
        /// <summary>
        /// Удаление по ид
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        internal IHelperResult RemoveById<FModel>(Guid id)
            where FModel : class, TModel, IKeyEntity, new() {
            return RemoveByIds<FModel>(new Guid[] { id });
        }
        /// <summary>
        /// Удаление по ид
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        internal IHelperResult RemoveByIds<FModel>(Guid[] ids)
            where FModel : class, TModel, IKeyEntity, new() {
            try {
                foreach (var id in ids) {
                    var entry = Context.Entry<FModel>(new FModel() {
                        Id = id
                    });
                    entry.State = EntityState.Deleted;
                }

                Context.SaveChanges();

                return new BaseResponse(new BaseResponseModel());
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }
        public BaseRepository() { }

        public BaseRepository(LazarContext context) {
            _context = context;
        }
    }
}
