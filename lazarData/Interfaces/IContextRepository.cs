using lazarData.Context;
using System.Linq.Expressions;

namespace lazarData.Interfaces
{
    public interface IContextRepository
    {
        LazarContext Context { get; }
        IQueryable<FModel> GetAll<FModel>(bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class;
        TViewModel GetViewById<FModel, TViewModel>(Guid? Id, Func<FModel, TViewModel> toViewModel, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, IKeyEntity
            where TViewModel : class;
        FModel GetEntityById<FModel>(Guid Id, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, IKeyEntity;
        IHelperResult RemoveById<FModel>(Guid id)
            where FModel : class, IKeyEntity, new();
        IHelperResult RemoveByIds<FModel>(IEnumerable<Guid> ids)
            where FModel : class, IKeyEntity, new();
    }
}
