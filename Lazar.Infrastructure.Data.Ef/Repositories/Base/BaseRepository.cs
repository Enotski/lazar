using Lazar.Domain.Interfaces.Repositories.Base;
using Lazar.Infrastructure.Data.Ef.Context;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Base
{
    public abstract class BaseRepository<TModel> : IBaseRepository
            where TModel : class {
        protected readonly LazarContext _dbContext;
        public BaseRepository(LazarContext dbContext) { _dbContext = dbContext; }

        ///// <summary>
        ///// Получить все записи в таблице
        ///// </summary>
        ///// <param name="isNoTracking">Не отслеживать изменения</param>
        ///// <returns></returns>
        //public IQueryable<FModel> GetAll<FModel>(bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
        //    where FModel : class, TModel {
        //    try {
        //        return _dbContext.GetAll(isNoTracking, includes);
        //    } catch (Exception ex) {
        //        throw ex;
        //    }
        //}
        ///// <summary>
        ///// Получить запись по ИД
        ///// </summary>
        ///// <typeparam name="FModel"></typeparam>
        ///// <param name="Id">ИД записи</param>
        ///// <param name="isNoTracking">Отслеживать изменения</param>
        ///// <returns></returns>
        //public virtual TViewModel GetViewById<TModel>(Guid? Id, Func<TModel, TViewModel> toViewModel, bool isNoTracking = false, params Expression<Func<TModel, object>>[] includes)
        //    where TModel : class, IKeyEntity {
        //    try {
        //        return _dbContext.GetViewById(Id, toViewModel, isNoTracking, includes);
        //    } catch (Exception exp) {
        //        return null;
        //    }
        //}
        ///// <summary>
        ///// Получить запись по ИД
        ///// </summary>
        ///// <typeparam name="FModel"></typeparam>
        ///// <param name="Id">ИД записи</param>
        ///// <param name="isNoTracking">Отслеживать изменения</param>
        ///// <returns></returns>
        //public virtual FModel GetEntityById<FModel>(Guid Id, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
        //    where FModel : class, TModel, IKeyEntity {
        //    try {
        //        return _contextRepo.GetEntityById(Id, isNoTracking, includes);
        //    } catch (Exception exp) {
        //        return null;
        //    }
        //}
        ///// <summary>
        ///// Удаление по ид
        ///// </summary>
        ///// <typeparam name="FModel"></typeparam>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //internal IHelperResult RemoveById<FModel>(Guid id)
        //    where FModel : class, TModel, IKeyEntity, new() {
        //    return _contextRepo.RemoveById<FModel>(id);
        //}
        ///// <summary>
        ///// Удаление по ид
        ///// </summary>
        ///// <typeparam name="FModel"></typeparam>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //internal IHelperResult RemoveByIds<FModel>(IEnumerable<Guid> ids)
        //    where FModel : class, TModel, IKeyEntity, new() {
        //    try {
        //        return _contextRepo.RemoveByIds<FModel>(ids);
        //    } catch (Exception ex) {
        //        return new BaseResponse(ex);
        //    }
        //}
    }
}
