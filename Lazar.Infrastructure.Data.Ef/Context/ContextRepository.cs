using lazarData.Context;
using lazarData.Interfaces;
using lazarData.Models.Response.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lazar.Infrastructure.Data.Ef.Context
{
    public class ContextRepository : IContextRepository
    {
        private LazarContext _context;

        public ContextRepository(LazarContext context) => _context = context;
        public LazarContext Context { get => _context ?? (_context = new LazarContext(new DbContextOptions<LazarContext>())); private set { } }
        //public BaseRepository(LazarContext context)
        //{
        //    _context = context;
        //}

        /// <summary>
        /// Получить все записи в таблице
        /// </summary>
        /// <param name="isNoTracking">Не отслеживать изменения</param>
        /// <returns></returns>
        public IQueryable<FModel> GetAll<FModel>(bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class
        {
            try
            {
                if (Context == null)
                {
                    throw new ArgumentNullException("Context is not initialized");
                }
                var query = Context.Set<FModel>().AsQueryable();
                if (includes != null && includes.Length > 0)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                if (isNoTracking)
                {
                    return query.AsNoTracking();
                }
                return query;
            }
            catch (Exception ex)
            {
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
        public TViewModel GetViewById<FModel, TViewModel>(Guid? Id, Func<FModel, TViewModel> toViewModel, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, IKeyEntity
            where TViewModel : class
        {
            try
            {
                var query = GetAll(isNoTracking, includes);
                var order = query.Where(x => x.Id == Id).Select(toViewModel).FirstOrDefault();
                return order;
            }
            catch (Exception exp)
            {
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
        public FModel GetEntityById<FModel>(Guid Id, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, IKeyEntity
        {
            try
            {
                var query = GetAll(isNoTracking, includes);
                var order = query.FirstOrDefault(x => x.Id == Id);
                return order;
            }
            catch (Exception exp)
            {
                return null;
            }
        }
        /// <summary>
        /// Удаление по ид
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IHelperResult RemoveById<FModel>(Guid id)
            where FModel : class, IKeyEntity, new()
        {
            return RemoveByIds<FModel>(new Guid[] { id });
        }
        /// <summary>
        /// Удаление по ид
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IHelperResult RemoveByIds<FModel>(IEnumerable<Guid> ids)
            where FModel : class, IKeyEntity, new()
        {
            try
            {
                foreach (var id in ids)
                {
                    var entry = Context.Entry<FModel>(new FModel()
                    {
                        Id = id
                    });
                    entry.State = EntityState.Deleted;
                }

                Context.SaveChanges();

                return new BaseResponse(new BaseResponseModel());
            }
            catch (Exception ex)
            {
                return new BaseResponse(ex);
            }
        }
    }
}
