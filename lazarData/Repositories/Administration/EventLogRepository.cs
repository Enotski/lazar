using lazarData.Context;
using lazarData.Enums;
using lazarData.Interfaces;
using lazarData.Models.Administration;
using lazarData.Models.EventLogs;
using lazarData.Models.Response.DataGrid;
using lazarData.Models.Response.ViewModels;
using lazarData.Utils;
using System.Data.Entity;
using TMK.Utils.Utils;

namespace lazarData.Repositories.Administration
{
    public class EventLogRepository : BaseRepository<EventLogViewModel, EventLog>
    {
        public EventLogRepository(IContextRepository context) : base(context) { }

        /// <summary>
        /// Возвращает список логирования событий
        /// </summary>
        public IHelperResult GetEventLogsDataGrid(int skip, int take, IEnumerable<DataGridSort> sorts, IEnumerable<DataGridFilter> filters)
        {
            try
            {
                DataGridResponseModel<EventLogDataGrid> model = new DataGridResponseModel<EventLogDataGrid>();
                var query = GetAll<EventLog>(true, x => x.ChangedUser);

                FilterData(ref query, filters);
                var orderedQuery = query.OrderBy(x => 0);
                SortData(ref orderedQuery, sorts);
                model.totalCount = orderedQuery.Count();
                model.data = orderedQuery.Skip(skip).Take(take).Select(ModelToDataGridViewModel());

                return model;
            } catch (Exception exp)
            {
                return new BaseResponse(exp.Message);
            }
        }
        public static void FilterData(ref IQueryable<EventLog> source, IEnumerable<DataGridFilter> filters)
        {
            if (filters == null || !filters.Any()) return;
            foreach (var filter in filters)
            {
                filter.Value = filter.Value.Trim().ToLower();
                switch (filter.ColumnName)
                {
                    case "SubSystemName":
                        {
                            var subSystemType = filter.Value.GetEnumByDescription<SubSystemType>();
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Equals:
                                    {
                                        source = source.Where(x => x.SubSystem == subSystemType);
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.Where(x => x.SubSystem != subSystemType);
                                        break;
                                    }
                            }
                            break;
                        }
                    case "EventTypeName":
                        {
                            var systemEventType = filter.Value.GetEnumByDescription<EventType>();
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Equals:
                                    {
                                        source = source.Where(x => x.EventType == systemEventType);
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.Where(x => x.EventType != systemEventType);
                                        break;
                                    }
                            }
                            break;
                        }
                    case "UserName":
                        {
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Contains:
                                    {
                                        source = source.AsEnumerable().Where(x => x.ChangedUser.Login.ToLower().Contains(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.NotContains:
                                    {
                                        source = source.AsEnumerable().Where(x => !x.ChangedUser.Login.ToLower().Contains(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.StartsWith:
                                    {
                                        source = source.AsEnumerable().Where(x => x.ChangedUser.Login.ToLower().StartsWith(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.EndsWith:
                                    {
                                        source = source.AsEnumerable().Where(x => x.ChangedUser.Login.ToLower().EndsWith(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.Equals:
                                    {
                                        source = source.AsEnumerable().Where(x => x.ChangedUser.Login.ToLower() == filter.Value).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.AsEnumerable().Where(x => x.ChangedUser.Login.ToLower() != filter.Value).AsQueryable();
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

        public static void SortData(ref IOrderedQueryable<EventLog> source, IEnumerable<DataGridSort> sorts)
        {
            if (sorts == null || !sorts.Any()) return;
            foreach (var sort in sorts)
            {
                switch (sort.ColumnName)
                {
                    case "SubSystemName":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.SubSystem)
                                : source.ThenBy(x => x.SubSystem);
                            break;
                        }
                    case "EventTypeName":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.EventType)
                                : source.ThenBy(x => x.EventType);
                            break;
                        }
                    case "UserName":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.ChangedUser.Login)
                                : source.ThenBy(x => x.ChangedUser.Login);
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

        /// <summary>
        /// Преобразовывает сущности показателя в модель представления показателя
        /// </summary>
        /// <returns></returns>
        public Func<EventLog, EventLogViewModel> ModelToViewModel()
        {
            return model => new EventLogViewModel
            {
                Id = model.Id,
                EventType = model.EventType,
                TypeInfo = model.EventType.GetDescription(),
                Description = model.Description,
            };
        }
        /// <summary>
        /// Преобразовывает сущности показателя в табличную модель
        /// </summary>
        /// <returns></returns>
        public Func<EventLog, int, EventLogDataGrid> ModelToDataGridViewModel()
        {
            return (model, index) => new EventLogDataGrid
            {
                Id = model.Id,
                EventTypeName = model.EventType.GetDescription(),
                DateChange = model.DateChange.ToLocalTime().ToDataGridFormat(),
                SubSystemName = model.SubSystem.GetDescription(),
                UserName = model.ChangedUser?.Login,
                Num = index + 1
            };
        }

        public BaseResponseEnumerable RemoveLogsByPeriod(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                //if (!user.Roles.Any(x => x.Id == Guids.Roles.Administrator))
                //    return new BaseResponseEnumerable("Ошибка! Удалять записи в журнале может только Администратор");

                if (startDate > endDate)
                    return new BaseResponseEnumerable("Ошибка! Начальная дата должна быть меньше конечной");
                var toDelete = GetAll<EventLog>(false).Where(q => startDate <= q.DateChange && q.DateChange <= endDate).Select(q => q.Id);
                RemoveByIds<EventLog>(toDelete);

                Context.SaveChanges();
                return new BaseResponseEnumerable(new List<BaseResponseModel>());
            } catch (Exception ex)
            {
                return new BaseResponseEnumerable(ex);
            }
        }
        /// <summary>
        /// Удаление выбранных записей
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="filialId"></param>
        /// <returns></returns>
        public BaseResponseEnumerable RemoveLogs(IEnumerable<Guid> ids, Guid? userId)
        {
            try
            {
                var currentUser = Context.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == userId);
                if (currentUser != null || !currentUser.Roles.Any(r => r.Id == Guids.Roles.Administrator))
                    return new BaseResponseEnumerable("Ошибка! Удалять записи в журнале может только Администратор");

                var toDelete = GetAll<EventLog>(false).Where(q => ids.Contains(q.Id)).Select(q => q.Id);
                RemoveByIds<EventLog>(toDelete);

                Context.SaveChanges();
                return new BaseResponseEnumerable(new List<BaseResponseModel>());
            } catch (Exception ex)
            {
                return new BaseResponseEnumerable(ex);
            }
        }
        public async Task<bool> LogEventAsync(SubSystemType subSys, EventType type, Guid userId, string descr = "")
        {
            return await Task.Run(() =>
            {
                Exception error;
                try
                {
                    var log = new EventLog
                    {
                        Id = Guid.NewGuid(),
                        ChangedUserId = userId,
                        DateChange = DateTime.UtcNow,
                        SubSystem = subSys,
                        EventType = type,
                        Description = descr
                    };
                    Context.EventLogs.Add(log);
                    Context.SaveChanges();
                    return true;
                } catch (Exception exp)
                {
                    return false;
                };
            });
        }
        public async Task<bool> LogEventAsync(IEnumerable<EventLog> logs)
        {
            return await Task.Run(() =>
            {
                Exception error;
                try
                {
                    Context.EventLogs.AddRange(logs);
                    Context.SaveChanges();
                    return true;
                } catch (Exception exp)
                {
                    return false;
                };
            });
        }
        public bool LogEvent(SubSystemType subSys, EventType type, Guid userId, string descr = "")
        {
            Exception error;
            try
            {
                var log = new EventLog
                {
                    Id = Guid.NewGuid(),
                    ChangedUserId = userId,
                    DateChange = DateTime.UtcNow,
                    SubSystem = subSys,
                    EventType = type,
                    Description = descr
                };
                Context.EventLogs.Add(log);
                Context.SaveChanges();
                return true;
            } catch (Exception exp)
            {
                return false;
            };
        }
        public bool LogEvent(IEnumerable<EventLog> logs)
        {
            Exception error;
            try
            {
                Context.EventLogs.AddRange(logs);
                Context.SaveChanges();

                return true;
            } catch (Exception exp)
            {
                return false;
            };
        }
    }
}
