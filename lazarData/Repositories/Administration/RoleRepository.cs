using lazarData.Context;
using lazarData.Enums;
using lazarData.Interfaces;
using lazarData.Models.Administration;
using lazarData.Models.Response.DataGrid;
using lazarData.Models.Response.ViewModels;
using lazarData.Utils;
using System.Data.Entity;

namespace lazarData.Repositories.Administration
{
    public class RoleRepository : BaseRepository<RoleViewModel, Role>
    {
        /// <summary>
        /// Репозиторий логирования
        /// </summary>
        EventLogRepository logRepo = new EventLogRepository();
        /// <summary>
        /// Конструктор
        /// </summary>
        public RoleRepository() : base() { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public RoleRepository(LazarContext context) : base(context) { }

        public static void FilterData(ref IQueryable<Role> source, IEnumerable<DataGridFilter> filters)
        {
            if (filters == null || !filters.Any()) return;
            foreach (var filter in filters)
            {
                filter.Value = filter.Value.Trim().ToLower();
                switch (filter.ColumnName)
                {
                    case "Name":
                        {
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Contains:
                                    {
                                        source = source.Where(x => x.Name.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.NotContains:
                                    {
                                        source = source.Where(x => !x.Name.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.StartsWith:
                                    {
                                        source = source.Where(x => x.Name.ToLower().StartsWith(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.EndsWith:
                                    {
                                        source = source.Where(x => x.Name.ToLower().EndsWith(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.Equals:
                                    {
                                        source = source.Where(x => x.Name.ToLower() == filter.Value);
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.Where(x => x.Name.ToLower() != filter.Value);
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
        }

        public static void SortData(ref IOrderedQueryable<Role> source, IEnumerable<DataGridSort> sorts)
        {
            if (sorts == null || !sorts.Any()) return;
            foreach (var sort in sorts)
            {
                switch (sort.ColumnName)
                {
                    case "Name":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.Name)
                                : source.ThenBy(x => x.Name);
                            break;
                        }
                }
            }
        }

        public IHelperResult GetRolesDataGrid(int skip, int take, IEnumerable<DataGridSort> sorts, IEnumerable<DataGridFilter> filters)
        {
            try
            {
                DataGridResponseModel<RoleDataGrid> model = new DataGridResponseModel<RoleDataGrid>();
                var query = GetAll<Role>(true);
                FilterData(ref query, filters);
                var orderedQuery = query.OrderBy(x => 0);
                SortData(ref orderedQuery, sorts);
                model.totalCount = orderedQuery.Count();
                var res = orderedQuery.Skip(skip).Take(take).Select(ModelToDataGridViewModel()).ToList();
                model.data = res;
                return model;
            } catch (Exception exp)
            {
                return new BaseResponse(exp);
            }
        }
        public Func<Role, int, RoleDataGrid> ModelToDataGridViewModel()
        {
            return (x, index) => new RoleDataGrid
            {
                Id = x.Id,
                Name = x.Name,
                DateChange = x.DateChange.ToLocalTime().ToDataGridFormat(),
                Num = index + 1
            };
        }
        /// <summary>
        /// Преобразовывает сущности роли в модель представления роли
        /// </summary>
        /// <returns></returns>
        public override Func<Role, RoleViewModel> ModelToViewModel()
        {
            return model => new RoleViewModel
            {
                Id = model.Id,
                Name = model.Name,
            };
        }
        /// <summary>
        /// Общий метод редактирования и добавлние новой роли
        /// </summary>
        /// <param name="IdRole">Ид Роли выбранной</param>
        /// <param name="NameRole">Название роли</param>
        /// <param name="GroupAD">Группа Ад в которую должна входить данная роль</param>
        /// <returns></returns>
        public BaseResponse UpdateRole(Guid? IdRole, string NameRole, Guid userId)
        {
            Role oldData = new Role();
            Role newData = new Role();
            EventType logType;
            try
            {
                Exception exc = new Exception();
                if (string.IsNullOrWhiteSpace(NameRole))
                {
                    throw new Exception("Заполните все поля");
                }
                var CurrentUserId = userId;
                if (IdRole == Guid.Empty || IdRole == null)
                {
                    if (Context.Roles.FirstOrDefault(w => w.Name == NameRole) != null)
                    {
                        throw new Exception("Такая роль уже существует");
                    }

                    newData = new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = NameRole,
                        DateChange = DateTime.UtcNow
                    };

                    Context.Roles.Add(newData);
                    logType = EventType.Create;
                } else
                {
                    newData = GetAll<Role>(false).FirstOrDefault(w => w.Id == IdRole);
                    oldData = new Role
                    {
                        Name = newData.Name,
                    };
                    newData.Name = NameRole;
                    newData.DateChange = DateTime.UtcNow;

                    logType = EventType.Create;
                }

                Context.SaveChanges();

                if (logType == EventType.Create)
                {
                    logRepo.LogEvent(SubSystemType.Users, EventType.Create, userId);
                } else
                {
                    logRepo.LogEvent(SubSystemType.Users, EventType.Update, userId, $"Редактирование роли:\n до изменений - {oldData.Name};\n после изменений - {newData.Name}");
                }

                return new BaseResponse(new BaseResponseModel());
            } catch (Exception exc)
            {
                logRepo.LogEvent(SubSystemType.Users, EventType.Error, userId, "Ошибка добавления/редактирования");
                return new BaseResponse(exc);
            }
        }
        /// <summary>
        /// Удаление выбранных ролей
        /// </summary>
        /// <param name="ListIdRoles">Список выбранных Ид ролей для удаления</param>
        /// <returns></returns>
        public BaseResponse DeleteRoles(List<Guid> ListIdRoles, Guid userId)
        {
            try
            {
                Role role = new Role();
                List<Role> ListRols = new List<Role>();
                foreach (var Id in ListIdRoles)
                {
                    var roleModel = Context.Roles.FirstOrDefault(w => w.Id == Id);
                    ListRols.Add(roleModel);
                }
                var rolesLog = string.Join(", ", ListRols.Select(x => x.Name));
                Context.Roles.RemoveRange(ListRols);
                Context.SaveChanges();

                logRepo.LogEvent(SubSystemType.Users, EventType.Delete, userId, $"Удаление ролей:\n ({rolesLog})");
                return new BaseResponse(new RoleViewModel());
            } catch (Exception exc)
            {
                logRepo.LogEvent(SubSystemType.Users, EventType.Error, userId, $"Удаление ролей");
                return new BaseResponse(exc);
            }
        }
    }
}
