using lazarData.Interfaces;
using lazarData.Models.Response.ViewModels;
using lazarData.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace lazarData.Repositories
{
    public abstract class BaseRepository<TViewModel, TModel>
            where TViewModel : BaseResponseModel
            where TModel : class {
        private ContextRepository _contextRepo;

        public BaseRepository(ContextRepository context) => _contextRepo = context;
        public LazarContext Context { get => _contextRepo.Context; }

        /// <summary>
        /// Получить все записи в таблице
        /// </summary>
        /// <param name="isNoTracking">Не отслеживать изменения</param>
        /// <returns></returns>
        public IQueryable<FModel> GetAll<FModel>(bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel {
            try {
                return _contextRepo.GetAll(isNoTracking, includes);
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
        public virtual TViewModel GetViewById<TModel>(Guid? Id, Func<TModel, TViewModel> toViewModel, bool isNoTracking = false, params Expression<Func<TModel, object>>[] includes)
            where TModel : class, IKeyEntity {
            try {
                return _contextRepo.GetViewById(Id, toViewModel, isNoTracking, includes);
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
                return _contextRepo.GetEntityById(Id, isNoTracking, includes);
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
            return _contextRepo.RemoveById<FModel>(id);
        }
        /// <summary>
        /// Удаление по ид
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        internal IHelperResult RemoveByIds<FModel>(IEnumerable<Guid> ids)
            where FModel : class, TModel, IKeyEntity, new() {
            try {
                return _contextRepo.RemoveByIds<FModel>(ids);
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }
    }
}
