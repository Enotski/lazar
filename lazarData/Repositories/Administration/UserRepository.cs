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
    public class UserRepository : BaseRepository<UserViewModel, User>
    {
        EventLogRepository logRepo;
        public UserRepository(IContextRepository context) : base(context)
        {
            logRepo = new EventLogRepository(context);
        }
        public IHelperResult GetUsersDataGrid(int skip, int take, IEnumerable<DataGridSort>  sorts, IEnumerable<DataGridFilter> filters)
        {
            try
            {
                DataGridResponseModel<UserDataGrid> model = new DataGridResponseModel<UserDataGrid>();
                var query = GetAll<User>(true, x => x.Roles);
                FilterData(ref query, filters);
                var orderedQuery = query.OrderBy(x => 0);
                SortData(ref orderedQuery, sorts);
                model.totalCount = orderedQuery.Count();
                model.data = orderedQuery.Skip(skip).Take(take).AsEnumerable()
                    .Select(ModelToDataGridViewModel())
                    .ToArray();
                return model;
            } catch (Exception exp)
            {
                return  new BaseResponse(exp);
            }
        }

        public Func<User, int, UserDataGrid> ModelToDataGridViewModel()
        {
            return (x, index) => new UserDataGrid
            {
                Id = x.Id,
                DateChange = x.DateChange.ToLocalTime().ToDataGridFormat(),
                Email = x.Email,
                Login = x.Login,
                Roles = string.Join("; ", x.Roles.Select(r => r.Name)),
                Num = index + 1
            };
        }

        public static void FilterData(ref IQueryable<User> source, IEnumerable<DataGridFilter> filters)
        {
            if (filters == null || !filters.Any()) return;
            foreach (var filter in filters)
            {
                filter.Value = filter.Value.Trim().ToLower();
                switch (filter.ColumnName)
                {
                    case "Login":
                        {
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Contains:
                                    {
                                        source = source.Where(x => x.Login.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.NotContains:
                                    {
                                        source = source.Where(x => !x.Login.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.StartsWith:
                                    {
                                        source = source.Where(x => x.Login.ToLower().StartsWith(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.EndsWith:
                                    {
                                        source = source.Where(x => x.Login.ToLower().EndsWith(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.Equals:
                                    {
                                        source = source.Where(x => x.Login.ToLower() == filter.Value);
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.Where(x => x.Login.ToLower() != filter.Value);
                                        break;
                                    }
                            }
                            break;
                        }
                    case "Email":
                        {
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Contains:
                                    {
                                        source = source.Where(x => x.Email.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.NotContains:
                                    {
                                        source = source.Where(x => !x.Email.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.StartsWith:
                                    {
                                        source = source.Where(x => x.Email.ToLower().StartsWith(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.EndsWith:
                                    {
                                        source = source.Where(x => x.Email.ToLower().EndsWith(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.Equals:
                                    {
                                        source = source.Where(x => x.Email.ToLower() == filter.Value);
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.Where(x => x.Email.ToLower() != filter.Value);
                                        break;
                                    }
                            }
                            break;
                        }
                    case "Roles":
                        {
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Contains:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Name.ToLower().Contains(filter.Value))).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.NotContains:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.All(r => !r.Name.ToLower().Contains(filter.Value))).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.StartsWith:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Name.ToLower().StartsWith(filter.Value))).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.EndsWith:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Name.ToLower().EndsWith(filter.Value))).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.Equals:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Name.ToLower() == filter.Value)).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Name.ToLower() != filter.Value)).AsQueryable();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "DateChange":
                        {
                            var dates = filter.Value.Split(';').Select(DateTime.Parse).ToArray();
                            DateTime date1 = dates[0];
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Less:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) < date1);
                                        break;
                                    }
                                case DataGridFilterType.Greater:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) > date1);
                                        break;
                                    }
                                case DataGridFilterType.LessOrEqual:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) <= date1);
                                        break;
                                    }
                                case DataGridFilterType.GreaterOrEqual:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) >= date1);
                                        break;
                                    }
                                case DataGridFilterType.Equals:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) == date1);
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) != date1);
                                        break;
                                    }
                                case DataGridFilterType.Between:
                                    {
                                        DateTime date2 = dates[1];
                                        source = source.Where(x => date1 <= DbFunctions.TruncateTime(x.DateChange) && DbFunctions.TruncateTime(x.DateChange) <= date2);
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
        }

        public static void SortData(ref IOrderedQueryable<User> source, IEnumerable<DataGridSort> sorts)
        {
            if (sorts == null || !sorts.Any()) return;
            foreach (var sort in sorts)
            {
                switch (sort.ColumnName)
                {
                    case "Login":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.Login)
                                : source.ThenBy(x => x.Login);
                            break;
                        }
                    case "Email":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.Email)
                                : source.ThenBy(x => x.Email);
                            break;
                        }
                    case "Roles":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.Roles.Count())
                                : source.ThenBy(x => x.Roles.Count());
                            break;
                        }
                    case "DateChange":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.DateChange)
                                : source.ThenBy(x => x.DateChange);
                            break;
                        }
                }
            }
        }

        public Func<User, UserViewModel> ModelToViewModel()
        {
            return model => new UserViewModel
            {
                Id = model.Id,
                Email = model.Email,
                Login = model.Login,
                Password = model.Password
            };
        }
        public BaseResponse GetView(Guid? id)
        {
            try
            {
                return new BaseResponse(GetViewById(id, ModelToViewModel(), true));
            } catch (Exception ex) { return new BaseResponse(ex); }
        }
        public BaseResponse<UserViewModel> AddEditUser(UserViewModel model, Guid currentUserId)
        {
            try
            {
                User user = null;
                EventType type;
                if (!model.Id.HasValue)
                {
                    user = new User
                    {
                        Id = Guid.NewGuid(),
                        DateChange = DateTime.UtcNow,
                        Login = model.Login,
                        Email = model.Email,

                    };
                    Context.Users.Add(user);
                    type = EventType.Create;
                } else
                {
                    user = Context.Users.FirstOrDefault(w => w.Id == model.Id);
                    if (user == null)
                    {
                        return new BaseResponse<UserViewModel>(new Exception("Пользователь отсутствует"));
                    }

                    user.Password = model.Password;
                    user.Email = model.Email;
                    user.Login = model.Login;
                    user.DateChange = DateTime.UtcNow;
                    type = EventType.Update;
                }

                Context.SaveChanges();
                logRepo.LogEvent(SubSystemType.Users, type, currentUserId);
                return new BaseResponse<UserViewModel>(ModelToViewModel().Invoke(user));
            } catch (Exception exp)
            {
                logRepo.LogEvent(SubSystemType.Users, EventType.Error, currentUserId);
                return new BaseResponse<UserViewModel>(exp);
            }
        }
        public BaseResponse<UserViewModel> SetRoleToUser(Guid? userId, Guid? roleId)
        {
            try
            {
                EventType type;
                if (userId.HasValue && roleId.HasValue)
                {
                    var user = Context.Users.Include(x => x.Roles).FirstOrDefault(w => w.Id == userId.Value);
                    if (user == null)
                    {
                        return new BaseResponse<UserViewModel>(new Exception("Пользователь отсутствует"));
                    }
                    if(user.Roles.Any(x => x.Id ==  roleId.Value)) { return new BaseResponse<UserViewModel>(new UserViewModel()); }

                    var role = Context.Roles.FirstOrDefault(x => x.Id == roleId.Value);

                    user.Roles.Add(role);
                    user.DateChange = DateTime.UtcNow;
                    type = EventType.Update;
                    Context.SaveChanges();
                    //logRepo.LogEvent(SubSystemType.Users, type, currentUserId);
                }
                return new BaseResponse<UserViewModel>(new UserViewModel());
            } catch (Exception exp)
            {
                //logRepo.LogEvent(SubSystemType.Users, EventType.Error, currentUserId);
                return new BaseResponse<UserViewModel>(exp);
            }
        }
        public BaseResponseEnumerable DeleteUsers(IEnumerable<Guid> userIds)
        {
            try
            {
                BaseResponseEnumerable model = new BaseResponseEnumerable(new Exception());
                var delUsers = Context.Users.Where(x => userIds.Contains(x.Id));
                Context.Users.RemoveRange(delUsers);
                Context.SaveChanges();
                return model;
            } catch (Exception exp)
            {
                return new BaseResponseEnumerable(exp);
            }
        }
    }
}