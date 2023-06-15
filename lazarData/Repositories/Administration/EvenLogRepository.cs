using lazarData.Models.EventLogs;
using lazarData.Models.Response.DataGrid;
using lazarData.Models.Response.ViewModels;
using lazarData.Utils;
using LazarData.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazarData.Repositories.Administration
{
    internal class EventLogRepository : BaseRepository<EventLogViewModel, EventLog>
    {
        public EventLogRepository() : base() { }
        public EventLogRepository(LazarContext context) : base(context) { }

        /// <summary>
        /// Возвращает список логирования событий
        /// </summary>
        public IHelperResult GetEventLogsDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters, Guid? filial)
        {
            try
            {
                DataGridResponseModel<EventLogDataGrid> model = new DataGridResponseModel<EventLogDataGrid>();
                IQueryable<EventLog> query;
                if (filial == Guid.Empty)
                {
                    query = GetAll<EventLog>(true);
                }
                else
                {
                    query = GetAll<EventLog>(true).Where(x => x.FilialId == filial);
                }

                FilterData(ref query, filters);
                var orderedQuery = query.OrderBy(x => 0);
                SortData(ref orderedQuery, sorts);
                model.totalCount = orderedQuery.Count();
                model.data = orderedQuery.Skip(skip).Take(take).AsEnumerable()
                    .Select(ModelToDataGridViewModel())
                    .ToArray();
                return model;
            }
            catch (Exception exp)
            {
                return DataGridResponseModel<EventLogDataGrid>.ErrorResponse(exp);
            }
        }
        public static void FilterData(ref IQueryable<EventLog> source, DataGridFilter[] filters)
        {
            if (filters == null || !filters.Any()) return;
            foreach (var filter in filters)
            {
                filter.Value = filter.Value.Trim().ToLower();
                switch (filter.ColumnName)
                {
                    case "SubSystemName":
                        {
                            SubSystemType subSystemType = filter.Value.GetEnumByDescription<SubSystemType>();
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
                            SystemEventType systemEventType = filter.Value.GetEnumByDescription<SystemEventType>();
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
                    case "Message":
                        {
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Contains:
                                    {
                                        source = source.Where(x => x.Message.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.NotContains:
                                    {
                                        source = source.Where(x => !x.Message.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.StartsWith:
                                    {
                                        source = source.Where(x => x.Message.ToLower().StartsWith(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.EndsWith:
                                    {
                                        source = source.Where(x => x.Message.ToLower().EndsWith(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.Equals:
                                    {
                                        source = source.Where(x => x.Message.ToLower() == filter.Value);
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.Where(x => x.Message.ToLower() != filter.Value);
                                        break;
                                    }
                            }
                            break;
                        }
                    case "Result":
                        {
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Contains:
                                    {
                                        source = source.Where(x => x.Result.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.NotContains:
                                    {
                                        source = source.Where(x => !x.Result.ToLower().Contains(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.StartsWith:
                                    {
                                        source = source.Where(x => x.Result.ToLower().StartsWith(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.EndsWith:
                                    {
                                        source = source.Where(x => x.Result.ToLower().EndsWith(filter.Value));
                                        break;
                                    }

                                case DataGridFilterType.Equals:
                                    {
                                        source = source.Where(x => x.Result.ToLower() == filter.Value);
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.Where(x => x.Result.ToLower() != filter.Value);
                                        break;
                                    }
                            }
                            break;
                        }
                    case "LocalRecordingTime":
                        {
                            var dates = filter.Value.Split(';').Select(DateTime.Parse).ToArray();
                            DateTime date1 = dates[0];
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Less:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.RecordingTime) < date1);
                                        break;
                                    }
                                case DataGridFilterType.Greater:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.RecordingTime) > date1);
                                        break;
                                    }
                                case DataGridFilterType.LessOrEqual:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.RecordingTime) <= date1);
                                        break;
                                    }
                                case DataGridFilterType.GreaterOrEqual:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.RecordingTime) >= date1);
                                        break;
                                    }
                                case DataGridFilterType.Equals:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.RecordingTime) == date1);
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.Where(x => DbFunctions.TruncateTime(x.RecordingTime) != date1);
                                        break;
                                    }
                                case DataGridFilterType.Between:
                                    {
                                        DateTime date2 = dates[1];
                                        source = source.Where(x => date1 <= DbFunctions.TruncateTime(x.RecordingTime) && DbFunctions.TruncateTime(x.RecordingTime) <= date2);
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
                                        source = source.AsEnumerable().Where(x => x.UserName.ToLower().Contains(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.NotContains:
                                    {
                                        source = source.AsEnumerable().Where(x => !x.UserName.ToLower().Contains(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.StartsWith:
                                    {
                                        source = source.AsEnumerable().Where(x => x.UserName.ToLower().StartsWith(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.EndsWith:
                                    {
                                        source = source.AsEnumerable().Where(x => x.UserName.ToLower().EndsWith(filter.Value)).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.Equals:
                                    {
                                        source = source.AsEnumerable().Where(x => x.UserName.ToLower() == filter.Value).AsQueryable();
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.AsEnumerable().Where(x => x.UserName.ToLower() != filter.Value).AsQueryable();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "IP":
                        {
                            switch (filter.Type)
                            {
                                case DataGridFilterType.Contains:
                                    {
                                        source = source.Where(x => x.IP.ToLower().Contains(filter.Value));
                                        break;
                                    }
                                case DataGridFilterType.NotContains:
                                    {
                                        source = source.Where(x => !x.IP.ToLower().Contains(filter.Value));
                                        break;
                                    }
                                case DataGridFilterType.StartsWith:
                                    {
                                        source = source.Where(x => x.IP.ToLower().StartsWith(filter.Value));
                                        break;
                                    }
                                case DataGridFilterType.EndsWith:
                                    {
                                        source = source.Where(x => x.IP.ToLower().EndsWith(filter.Value));
                                        break;
                                    }
                                case DataGridFilterType.Equals:
                                    {
                                        source = source.Where(x => x.IP.ToLower() == filter.Value);
                                        break;
                                    }
                                case DataGridFilterType.NotEquals:
                                    {
                                        source = source.Where(x => x.IP.ToLower() != filter.Value);
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
        }

        public static void SortData(ref IOrderedQueryable<EventLog> source, DataGridSort[] sorts)
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
                    case "Message":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.Message)
                                : source.ThenBy(x => x.Message);
                            break;
                        }
                    case "Result":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.Result)
                                : source.ThenBy(x => x.Result);
                            break;
                        }
                    case "LocalRecordingTime":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.RecordingTime)
                                : source.ThenBy(x => x.RecordingTime);
                            break;
                        }
                    case "UserName":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.UserName)
                                : source.ThenBy(x => x.UserName);
                            break;
                        }
                    case "IP":
                        {
                            source = sort.Type == DataGridSortType.Descending
                                ? source.ThenByDescending(x => x.IP)
                                : source.ThenBy(x => x.IP);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Преобразовывает сущности показателя в модель представления показателя
        /// </summary>
        /// <returns></returns>
        public override Func<EventLog, EventLogViewModel> ModelToViewModel()
        {
            return model => new EventLogViewModel
            {
                Id = model.Id,
                EventType = model.EventType,
                Message = model.Message,
                RecordingTime = model.RecordingTime,
                UserName = model.UserName,
                Result = model.Result
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
                IP = model.IP,
                Message = model.Message,
                LocalRecordingTime = model.RecordingTime.ToLocalTime().ToDataGridFormat(),
                SubSystemName = model.SubSystem.GetDescription(),
                UserName = model.UserName,
                Result = model.Result,
                Num = index + 1
            };
        }

        public BaseResponseEnumerable RemoveLogsByPeriod(DateTime startDate, DateTime endDate, Guid? filialId, bool allFilials, IEnumerable<Guid> userRoles)
        {
            try
            {
                if (!userRoles.Contains(Guids.Role.AdministratorIA))
                    return new BaseResponseEnumerable("Ошибка! Удалять записи в журнале может только Администратор ИА");

                if (startDate > endDate)
                    return new BaseResponseEnumerable("Ошибка! Начальная дата должна быть меньше конечной");
                Guid[] toDelete;
                if (allFilials)
                    toDelete = GetAll<EventLog>(false).Where(q => startDate <= q.RecordingTime && q.RecordingTime <= endDate).Select(q => q.Id).ToArray();
                else
                    toDelete = GetAll<EventLog>(false).Where(q => startDate <= DbFunctions.TruncateTime(q.RecordingTime) && DbFunctions.TruncateTime(q.RecordingTime) <= endDate && q.FilialId == filialId).Select(q => q.Id).ToArray();

                RemoveByIds<EventLog>(toDelete);

                Context.SaveChanges();
                return new BaseResponseEnumerable(new List<BaseResponseModel>());
            }
            catch (Exception ex)
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
        public BaseResponseEnumerable RemoveLogs(Guid[] ids, Guid filialId, IEnumerable<Guid> userRoles)
        {
            try
            {
                if (!userRoles.Contains(Guids.Role.AdministratorIA))
                    return new BaseResponseEnumerable("Ошибка! Удалять записи в журнале может только Администратор ИА");

                var toDelete = GetAll<EventLog>(false).Where(q => q.FilialId == filialId && ids.Contains(q.Id)).Select(q => q.Id).ToArray();
                RemoveByIds<EventLog>(toDelete);

                Context.SaveChanges();
                return new BaseResponseEnumerable(new List<BaseResponseModel>());
            }
            catch (Exception ex)
            {
                return new BaseResponseEnumerable(ex);
            }
        }
        /// <summary>
        /// Добавляет запись в лог-файл 
        /// </summary>
        /// <param name="subSystem">Тип подсистемы</param>
        /// <param name="eventType">Тип события</param>
        /// <param name="message">Сообщение</param>
        /// <param name="result">Метрики</param>
        /// <param name="pathToEventLogFile">Путь к лог-файлу</param>
        /// <returns></returns>
        private bool AddRecordToFile(SubSystemType subSystem, SystemEventType eventType, string message, string result, string pathToEventLogFile, out Exception error)
        {
            return FileHelper.WriteFile(pathToEventLogFile, FileMode.Append,
                    Environment.NewLine + string.Format("Дата: {0}; Модуль системы: {1}; Тип события: {2}; Результат: {3}; Исключение: {4}",
                    DateTime.UtcNow, subSystem.GetDescription(), eventType.GetDescription(), result, message), out error);
        }

        /// <summary>
        /// Добавляет запись в лог-файл
        /// </summary>
        /// <param name="subSystem">Тип подсистемы</param>
        /// <param name="eventType">Тип события</param>
        /// <param name="exp">Исключение</param>
        /// <param name="result">Метрики</param>
        /// <param name="pathToEventLogFile">Путь к лог-файлу</param>
        /// <returns></returns>
        private bool AddRecordToFile(SubSystemType subSystem, SystemEventType eventType, Exception exp, string result, string pathToEventLogFile, out Exception error)
        {
            return AddRecordToFile(subSystem, eventType, TMK.Utils.Helpers.ExceptionHelper.FormatException(exp), result, pathToEventLogFile, out error);
        }

        /// <summary>
        /// Добавляет запись в БД (асинхронно)
        /// </summary>
        /// <param name="subSystem">Тип подсистемы</param>
        /// <param name="eventType">Тип события</param>
        /// <param name="exp">Исключение</param>
        /// <param name="result">Метрики</param>
        /// <param name="curUser">Пользователь</param>
        /// <param name="pathToEventLogFile">Путь к лог-файлу</param>
        /// <returns></returns>
        public void AddRecordAsync(SubSystemType subSystem, SystemEventType eventType, Exception exp, string result, UserInfo curUser, string pathToEventLogFile = null)
        {
            AddRecordAsync(subSystem, eventType, ExceptionHelper.FormatException(exp), result, curUser, pathToEventLogFile);
        }

        /// <summary>
        /// Добавляет запись в БД (асинхронно)
        /// </summary>
        /// <param name="subSystem">Тип подсистемы</param>
        /// <param name="eventType">Тип события</param>
        /// <param name="exp">Исключение</param>
        /// <param name="result">Метрики</param>
        /// <param name="curUser">Пользователь</param>
        /// <param name="pathToEventLogFile">Путь к лог-файлу</param>
        /// <returns></returns>
        public async Task<bool> AddRecordAsync(SubSystemType subSystem, SystemEventType eventType, string message, string result, UserInfo curUser, string pathToEventLogFile = null)
        {
            if (string.IsNullOrEmpty(pathToEventLogFile))
                pathToEventLogFile = FileHelper.GetPathToEventLogFile;
            return await Task.Run(() => {
                Exception error;
                try
                {
                    if (curUser == null)
                    {
                        AddRecordToFile(subSystem, eventType, message, result, pathToEventLogFile, out error);
                    }
                    else
                    {
                        Context.EventLogs.Add(new EventLog(curUser.FilialId, ConvertStringFormatDataChangehaveMlisecond(curUser.FilialDateChangeView), subSystem, eventType,
                            message, result, curUser.UserFIO, curUser.IP, DateTime.UtcNow));
                        Context.SaveChanges();
                    }
                    return true;
                }
                catch (Exception exp)
                {
                    AddRecordToFile(subSystem, eventType, message, result, pathToEventLogFile, out error);
                    AddRecordToFile(SubSystemType.Logging, SystemEventType.Create, exp, "Метод: EventLogHelper.AddRecordAsync()", pathToEventLogFile, out error);
                    return false;
                };
            });
        }

        /// <summary>
        /// Логирование события
        /// </summary>
        /// <param name="SubSystem">Тип подсистемы</param>
        /// <param name="EventType">Тип события</param>
        /// <param name="msg">Сообщение</param>
        /// <param name="result">Результат</param>
        /// <param name="CurUser">Текущий пользователь</param>
        /// <param name="PathToEventLogFile">Путь к файлу, в который запишется событие 
        /// в случае ошибки метода</param>
        /// <returns></returns>
        public bool LogEvent(SubSystemType SubSystem, SystemEventType EventType, string msg,
            string result, UserInfo CurUser, out string additionalInfo, Guid? FilialId = null, DateTime? FilialDateChange = null, string PathToEventLogFile = null)
        {
            if (string.IsNullOrEmpty(PathToEventLogFile))
            {
                PathToEventLogFile = FileHelper.GetPathToEventLogFile;
            }
            Exception error;
            try
            {
                if (CurUser == null)
                {
                    AddRecordToFile(SubSystem, EventType, msg,
                        result, PathToEventLogFile, out error);
                }
                else
                {
                    //var parsedData = ConvertStringFormatDataChangehaveMlisecond(CurUser.FilialDateChangeView);
                    Guid FilId = FilialId ?? CurUser.FilialId.Value;
                    DateTime FilDC = FilialDateChange ?? CurUser.FilialDateChange.Value;
                    Context.EventLogs.Add(new EventLog(FilId, FilDC, // parsedData,
                        SubSystem, EventType, msg, result, CurUser.UserFIO, CurUser.IP, DateTime.UtcNow));
                }
                Context.SaveChanges();
                additionalInfo = "Success";
                return true;
            }
            catch (Exception exp)
            {
                AddRecordToFile(SubSystem, EventType, msg, result, PathToEventLogFile, out error);
                AddRecordToFile(SubSystemType.Logging, SystemEventType.Create, exp, string.Format("{0}",
                    DiagnosticsHelper.GetFullMethodName(System.Reflection.MethodBase.GetCurrentMethod())), PathToEventLogFile, out error);
                if (error != null)
                {
                    additionalInfo = "Действие не было записано по причине ошибки: " + ExceptionHelper.FormatException(exp)
                        + "так же не удалось записать файл по причине ошибки: " + ExceptionHelper.FormatException(error);
                }
                else
                {
                    additionalInfo = "Действие не было записано по причине ошибки: " + ExceptionHelper.FormatException(exp);
                }
                return false;
            };
        }
        public bool LogEventList(IEnumerable<EventLog> logs, UserInfo CurUser, out string additionalInfo, string PathToEventLogFile = null)
        {
            if (string.IsNullOrEmpty(PathToEventLogFile))
            {
                PathToEventLogFile = FileHelper.GetPathToEventLogFile;
            }
            Exception error;
            try
            {
                if (CurUser == null)
                {
                    foreach (var log in logs)
                        AddRecordToFile(log.SubSystem, log.EventType, log.Message, log.Result, PathToEventLogFile, out error);
                }
                else
                {

                    Context.EventLogs.AddRange(logs);
                }
                Context.SaveChanges();
                additionalInfo = "Success";
                return true;
            }
            catch (Exception exp)
            {
                foreach (var log in logs)
                    AddRecordToFile(log.SubSystem, log.EventType, log.Message, log.Result, PathToEventLogFile, out error);
                AddRecordToFile(SubSystemType.Logging, SystemEventType.Create, exp, string.Format("{0}",
                    DiagnosticsHelper.GetFullMethodName(System.Reflection.MethodBase.GetCurrentMethod())), PathToEventLogFile, out error);
                if (error != null)
                {
                    additionalInfo = "Действие не было записано по причине ошибки: " + ExceptionHelper.FormatException(exp)
                        + "так же не удалось записать файл по причине ошибки: " + ExceptionHelper.FormatException(error);
                }
                else
                {
                    additionalInfo = "Действие не было записано по причине ошибки: " + ExceptionHelper.FormatException(exp);
                }
                return false;
            };
        }
    }
}
