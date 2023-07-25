using Lazar.Domain.Interfaces.Repositories.Administration;
using Lazar.Domain.Interfaces.Repositories.EventLog;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Core.Models.Administration;
using System.Data.Entity;

namespace Lazar.Infrastructure.Data.Ef.Repositories.Administration
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        ISystemLogRepository logRepo;
        public UsersRepository(LazarContext context) : base(context)
        {
        }

        public IHelperResult GetUsersDataGrid(int skip, int take, IEnumerable<DataGridSort> sorts, IEnumerable<DataGridFilter> filters)
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
                return new BaseResponse(exp);
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
                                case FilterType.Contains:
                                    {
                                        source = source.Where(x => x.Login.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case FilterType.NotContains:
                                    {
                                        source = source.Where(x => !x.Login.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case FilterType.StartsWith:
                                    {
                                        source = source.Where(x => x.Login.ToLower().StartsWith(filter.Value));
                                        break;
                                    }

                                case FilterType.EndsWith:
                                    {
                                        source = source.Where(x => x.Login.ToLower().EndsWith(filter.Value));
                                        break;
                                    }

                                case FilterType.Equals:
                                    {
                                        source = source.Where(x => x.Login.ToLower() == filter.Value);
                                        break;
                                    }
                                case FilterType.NotEquals:
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
                                case FilterType.Contains:
                                    {
                                        source = source.Where(x => x.Email.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case FilterType.NotContains:
                                    {
                                        source = source.Where(x => !x.Email.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case FilterType.StartsWith:
                                    {
                                        source = source.Where(x => x.Email.ToLower().StartsWith(filter.Value));
                                        break;
                                    }

                                case FilterType.EndsWith:
                                    {
                                        source = source.Where(x => x.Email.ToLower().EndsWith(filter.Value));
                                        break;
                                    }

                                case FilterType.Equals:
                                    {
                                        source = source.Where(x => x.Email.ToLower() == filter.Value);
                                        break;
                                    }
                                case FilterType.NotEquals:
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
                                case FilterType.Contains:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Name.ToLower().Contains(filter.Value))).AsQueryable();
                                        break;
                                    }
                                case FilterType.NotContains:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.All(r => !r.Name.ToLower().Contains(filter.Value))).AsQueryable();
                                        break;
                                    }
                                case FilterType.StartsWith:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Name.ToLower().StartsWith(filter.Value))).AsQueryable();
                                        break;
                                    }
                                case FilterType.EndsWith:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Name.ToLower().EndsWith(filter.Value))).AsQueryable();
                                        break;
                                    }
                                case FilterType.Equals:
                                    {
                                        source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Name.ToLower() == filter.Value)).AsQueryable();
                                        break;
                                    }
                                case FilterType.NotEquals:
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
                                case FilterType.Less:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) < date1);
                                        break;
                                    }
                                case FilterType.Greater:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) > date1);
                                        break;
                                    }
                                case FilterType.LessOrEqual:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) <= date1);
                                        break;
                                    }
                                case FilterType.GreaterOrEqual:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) >= date1);
                                        break;
                                    }
                                case FilterType.Equals:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) == date1);
                                        break;
                                    }
                                case FilterType.NotEquals:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.DateChange) != date1);
                                        break;
                                    }
                                case FilterType.Between:
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
                            source = sort.Type == SortType.Descending
                                ? source.ThenByDescending(x => x.Login)
                                : source.ThenBy(x => x.Login);
                            break;
                        }
                    case "Email":
                        {
                            source = sort.Type == SortType.Descending
                                ? source.ThenByDescending(x => x.Email)
                                : source.ThenBy(x => x.Email);
                            break;
                        }
                    case "Roles":
                        {
                            source = sort.Type == SortType.Descending
                                ? source.ThenByDescending(x => x.Roles.Count())
                                : source.ThenBy(x => x.Roles.Count());
                            break;
                        }
                    case "DateChange":
                        {
                            source = sort.Type == SortType.Descending
                                ? source.ThenByDescending(x => x.DateChange)
                                : source.ThenBy(x => x.DateChange);
                            break;
                        }
                }
            }
        }

        public Func<User, UserDto> ModelToViewModel()
        {
            return model => new UserDto
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
        public BaseResponse<UserDto> AddEditUser(UserDto model, Guid currentUserId)
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
                        return new BaseResponse<UserDto>(new Exception("Пользователь отсутствует"));
                    }

                    user.Password = model.Password;
                    user.Email = model.Email;
                    user.Login = model.Login;
                    user.DateChange = DateTime.UtcNow;
                    type = EventType.Update;
                }

                Context.SaveChanges();
                logRepo.LogEvent(SubSystemType.Users, type, currentUserId);
                return new BaseResponse<UserDto>(ModelToViewModel().Invoke(user));
            } catch (Exception exp)
            {
                logRepo.LogEvent(SubSystemType.Users, EventType.Error, currentUserId);
                return new BaseResponse<UserDto>(exp);
            }
        }
        public ResponseModel<UserDto> LoginUser(UserDto model)
        {
            try
            {
                var user = GetAll<User>(true).FirstOrDefault(x => x.Login == model.Login/* && x.Password == model.Password*/);
                if(user == null)
                {
                    return new ResponseModel<UserDto>("Не верный логин или пароль");
                }
                return new ResponseModel<UserDto>(new UserDto()
                {
                    Id = user.Id,
                    Login = user.Login,
                    Email = user.Email,
                });
            } catch(Exception ex)
            {
                return new ResponseModel<UserDto>(ex.Message);
            }
        }
        public UserDto AddUser(UserDto model, Guid? userId = null)
        {
            try
            {
                model.Password = new Random().Next(15000).ToString();
                //if (string.IsNullOrWhiteSpace(model.Login) || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
                //{
                //    throw new Exception("Заполните все поля");
                //}

                if (Context.Users.FirstOrDefault(w => w.Login == model.Login || w.Email == model.Email) != null)
                {
                    throw new Exception("Такой пользователь уже существует");
                }

                var newData = new User
                {
                    Id = Guid.NewGuid(),
                    Login = model.Login,
                    Email = model.Email,
                    Password = model.Password,
                    DateChange = DateTime.UtcNow
                };

                Context.Users.Add(newData);
                Context.SaveChanges();
                logRepo.LogEvent(SubSystemType.Users, EventType.Create, userId.GetValueOrDefault());
                return new UserDto() { Login = newData.Login, Email = newData.Email};
            } catch (Exception exc)
            {
                logRepo.LogEvent(SubSystemType.Users, EventType.Error, userId.GetValueOrDefault(), "Ошибка добавления");
                return new UserDto(exc.Message);
            }
        }
        /// <summary>
        /// Общий метод редактирования и добавлние новой роли
        /// </summary>
        /// <param name="IdRole">Ид Роли выбранной</param>
        /// <param name="NameRole">Название роли</param>
        /// <param name="GroupAD">Группа Ад в которую должна входить данная роль</param>
        /// <returns></returns>
        public BaseResponse UpdateUser(UserDto model, Guid? userId = null)
        {
            try
            {
                var oldData = new User();
                var newData = new User();
                model.Password = new Random().Next(15000).ToString();
                Exception exc = new Exception();
                //if (string.IsNullOrWhiteSpace(model.Login) || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
                //{
                //    throw new Exception("Заполните все поля");
                //}

                newData = GetAll<User>(false).FirstOrDefault(w => w.Id == model.Id);
                oldData = new User
                {
                    Login = newData.Login,
                    Email = newData.Email,
                };

                var user = Context.Users.FirstOrDefault(w => w.Id == model.Id);
                if (user == null)
                {
                    return new BaseResponse(new Exception("Пользователь отсутствует"));
                }

                user.Password = model.Password;
                user.Email = model.Email;
                user.Login = model.Login;
                user.DateChange = DateTime.UtcNow;
                Context.SaveChanges();
                logRepo.LogEvent(SubSystemType.Users, EventType.Update, userId.GetValueOrDefault(), $"Редактирование роли:\n до изменений - {oldData.Login} / {oldData.Email};\n после изменений - {newData.Login} / {newData.Email}");
                return new BaseResponse(new BaseResponseModel());
            } catch (Exception exc)
            {
                logRepo.LogEvent(SubSystemType.Users, EventType.Error, userId.GetValueOrDefault(), "Ошибка редактирования");
                return new BaseResponse(exc);
            }
        }
        public BaseResponse<UserDto> SetRoleToUser(Guid? userId, Guid? roleId)
        {
            try
            {
                if (userId.HasValue && roleId.HasValue)
                {
                    var user = GetAll<User>(false, x => x.Roles).FirstOrDefault(w => w.Id == userId.Value);
                    if (user == null)
                    {
                        return new BaseResponse<UserDto>(new Exception("Пользователь отсутствует"));
                    }
                    if (user.Roles.Any(x => x.Id == roleId.Value)) { return new BaseResponse<UserDto>(new UserDto()); }

                    var role = Context.Roles.FirstOrDefault(x => x.Id == roleId.Value);

                    user.Roles.Add(role);
                    user.DateChange = DateTime.UtcNow;
                    Context.SaveChanges();
                    //logRepo.LogEvent(SubSystemType.Users, EventType.Update, currentUserId);
                }
                return new BaseResponse<UserDto>(new UserDto());
            } catch (Exception exp)
            {
                //logRepo.LogEvent(SubSystemType.Users, EventType.Error, currentUserId);
                return new BaseResponse<UserDto>(exp);
            }
        }
        public BaseResponse<UserDto> RemoveRoleFromUser(Guid? userId, Guid? roleId)
        {
            try
            {
                if (userId.HasValue && roleId.HasValue)
                {
                    var user = GetAll<User>(false, x => x.Roles).FirstOrDefault(w => w.Id == userId.Value);
                    if (user == null)
                    {
                        return new BaseResponse<UserDto>(new Exception("Пользователь отсутствует"));
                    }

                    if (user.Roles.Any(x => x.Id == roleId)) {
                        var role = user.Roles.FirstOrDefault(x => x.Id == roleId.Value);

                        user.Roles.Remove(role);
                        user.DateChange = DateTime.UtcNow;
                        Context.SaveChanges();
                    }
                    //logRepo.LogEvent(SubSystemType.Users, EventType.Update, currentUserId);
                }
                return new BaseResponse<UserDto>(new UserDto());
            } catch (Exception exp)
            {
                //logRepo.LogEvent(SubSystemType.Users, EventType.Error, currentUserId);
                return new BaseResponse<UserDto>(exp);
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
        /// <summary>
        /// Удаление выбранной роли
        /// </summary>
        /// <param name="id">Список выбранных Ид ролей для удаления</param>
        /// <returns></returns>
        public BaseResponse DeleteUser(Guid? id, Guid? userId = null)
        {
            try
            {
                var userModel = GetEntityById<User>(id.Value);
                if (userModel != null)
                {
                    Context.Users.Remove(userModel);
                    Context.SaveChanges();

                    logRepo.LogEvent(SubSystemType.Users, EventType.Delete, userId.GetValueOrDefault(), $"Удаление роли:\n ({userModel.Login})");
                }
                return new BaseResponse(new RoleDto());
            } catch (Exception exc)
            {
                logRepo.LogEvent(SubSystemType.Users, EventType.Error, userId.GetValueOrDefault(), $"Удаление ролей");
                return new BaseResponse(exc);
            }
        }
    }
}