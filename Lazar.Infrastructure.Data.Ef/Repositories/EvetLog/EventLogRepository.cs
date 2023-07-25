using Lazar.Domain.Interfaces.Repositories.EventLog;
using Lazar.Infrastructure.Data.Ef.Context;
using Lazar.Infrastructure.Data.Ef.Repositories.Base;
using Lazar.Domain.Core.Enums;
using Lazar.Domain.Core.Interfaces;
using Lazar.Domain.Core.Models.EventLogs;
using System.Data.Entity;

namespace Lazar.Domain.Core.Repositories.Administration
{
    /// <summary>
    /// Repository of events logs in app
    /// </summary>
    public class EventLogRepository : BaseRepository<EventLog>, ISystemLogRepository
    {
        public EventLogRepository(LazarContext context) : base(context) { }
        /// <summary>
        /// Get data for DxDataGrid
        /// </summary>
        /// <param name="skip">skip items count</param>
        /// <param name="take">take items count</param>
        /// <param name="sorts">list of sorts creterias</param>
        /// <param name="filters">list of filters criterias</param>
        /// <returns></returns>
        public async Task<IHelperResult> GetEventLogsDataGridAsync(int skip, int take, IEnumerable<DataGridSort> sorts, IEnumerable<DataGridFilter> filters)
        {
            try
            {
                DataGridResponseModel<EventLogDataGrid> model = new DataGridResponseModel<EventLogDataGrid>();
                var query = GetAll<EventLog>(true, x => x.ChangedUser);

                FilterData(ref query, filters);
                var orderedQuery = query.OrderBy(x => 0);
                SortData(ref orderedQuery, sorts);
                model.totalCount = orderedQuery.Count();
                model.data = orderedQuery.Skip(skip).Take(take).Select(ModelToDtoDataGrid());

                return model;
            } catch (Exception exp)
            {
                return new BaseResponse(exp.Message);
            }
        }
        /// <summary>
        /// Filter data
        /// </summary>
        /// <param name="source">raw data</param>
        /// <param name="filters">list of criterias</param>
        public static void FilterData(ref IQueryable<EventLog> source, IEnumerable<DataGridFilter> filters)
        {
            try
            {

            }catch(Exception ex)
            {

            }
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
                                case FilterType.Equals:
                                    {
                                        source = source.Where(x => x.SubSystem == subSystemType);
                                        break;
                                    }
                                case FilterType.NotEquals:
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
                                case FilterType.Equals:
                                    {
                                        source = source.Where(x => x.EventType == systemEventType);
                                        break;
                                    }
                                case FilterType.NotEquals:
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
                                case FilterType.Contains:
                                    {
                                        source = source.AsEnumerable().Where(x => x.ChangedUser.Login.ToLower().Contains(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case FilterType.NotContains:
                                    {
                                        source = source.AsEnumerable().Where(x => !x.ChangedUser.Login.ToLower().Contains(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case FilterType.StartsWith:
                                    {
                                        source = source.AsEnumerable().Where(x => x.ChangedUser.Login.ToLower().StartsWith(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case FilterType.EndsWith:
                                    {
                                        source = source.AsEnumerable().Where(x => x.ChangedUser.Login.ToLower().EndsWith(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case FilterType.Equals:
                                    {
                                        source = source.AsEnumerable().Where(x => x.ChangedUser.Login.ToLower() == filter.Value).AsQueryable();
                                        break;
                                    }
                                case FilterType.NotEquals:
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
        /// <summary>
        /// Sort data
        /// </summary>
        /// <param name="source">raw data</param>
        /// <param name="sorts">list of criterias</param>
        public static void SortData(ref IOrderedQueryable<EventLog> source, IEnumerable<DataGridSort> sorts)
        {
            if (sorts == null || !sorts.Any()) return;
            foreach (var sort in sorts)
            {
                switch (sort.ColumnName)
                {
                    case "SubSystemName":
                        {
                            source = sort.Type == SortType.Descending
                                ? source.ThenByDescending(x => x.SubSystem)
                                : source.ThenBy(x => x.SubSystem);
                            break;
                        }
                    case "EventTypeName":
                        {
                            source = sort.Type == SortType.Descending
                                ? source.ThenByDescending(x => x.EventType)
                                : source.ThenBy(x => x.EventType);
                            break;
                        }
                    case "UserName":
                        {
                            source = sort.Type == SortType.Descending
                                ? source.ThenByDescending(x => x.ChangedUser.Login)
                                : source.ThenBy(x => x.ChangedUser.Login);
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
        /// <summary>
        /// Map data to dto
        /// </summary>
        /// <returns></returns>
        public Func<EventLog, EventLogDto> ModelToDto()
        {
            return model => new EventLogDto
            {
                Id = model.Id,
                EventType = model.EventType,
                TypeInfo = model.EventType.GetDescription(),
                Description = model.Description,
            };
        }
        /// <summary>
        /// Map data to data grid dto
        /// </summary>
        /// <returns></returns>
        public Func<EventLog, int, EventLogDataGrid> ModelToDtoDataGrid()
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
        /// <summary>
        /// Remove event logs by period dates
        /// </summary>
        /// <param name="startDate">Start date of period</param>
        /// <param name="endDate">End date of period</param>
        /// <returns></returns>
        public async Task<BaseResponseEnumerable> RemoveLogsByPeriodAsync(DateTime? startDate, DateTime? endDate)
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
        /// Remove logs 
        /// </summary>
        /// <param name="ids">List of keys</param>
        /// <param name="userId">User key</param>
        /// <returns></returns>
        public async Task<BaseResponseEnumerable> RemoveLogsAsync(IEnumerable<Guid> ids, Guid? userId)
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
        /// <summary>
        /// Add log asynchroniously
        /// </summary>
        /// <param name="subSys">SubSystem type</param>
        /// <param name="type">Type of event</param>
        /// <param name="userId">User</param>
        /// <param name="descr">Text information</param>
        /// <returns></returns>
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
        /// <summary>
        /// Add list of logs asynchroniously
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Add log
        /// </summary>
        /// <param name="subSys">SubSystem type</param>
        /// <param name="type">Type of event</param>
        /// <param name="userId">User</param>
        /// <param name="descr">Text information</param>
        /// <returns></returns>
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
        /// <summary>
        /// Add list of logs
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
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
