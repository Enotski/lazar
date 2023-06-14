// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace LazarData.Repositories.Administration
// {
//     internal class UserProfileRepository
//     {
//     }
// }
// using SysRM.Data.Context;
// using SysRM.Entities.Enums;
// using SysRM.Entities.Models.Indicators;
// using SysRM.Entities.Models.References;
// using SysRM.Entities.Models.References.Administration;
// using SysRM.Entities.Views.Response;
// using SysRM.Entities.Views.ViewModels;
// using SysRM.Entities.Views.ViewModels.Base;
// using SysRM.Entities.Views.ViewModels.DataGrid;
// using SysRM.Entities.Views.ViewModels.DataTable;
// using SysRM.Entities.Views.ViewModels.Indicators;
// using SysRM.Entities.Views.ViewModels.User;
// using System;
// using System.Collections.Generic;
// using System.Data.Entity;
// using System.Linq;
// using System.Linq.Expressions;
// using System.Text.RegularExpressions;
// using TMK.Utils.AD;
// using TMK.Utils.Enums;
// using TMK.Utils.Extensions;
// using lazarData.Interfaces.Base;

// namespace SysRM.Data.Repositories {
//     public class UserRepository : BaseRepository<UserViewModel, User> {
//         EventLogRepository eventLogRepository = new EventLogRepository();

//         /// <summary>
//         /// Филиалы
//         /// </summary>
//         private readonly FilialRepository filialRepository = new FilialRepository();
//         private readonly IndicatorRepository indicatorRepository = new IndicatorRepository();
//         /// <summary>
//         /// Конструктор
//         /// </summary>
//         public UserRepository() : base() {
//         }

//         /// <summary>
//         /// Конструктор
//         /// </summary>
//         /// <param name="context"></param>
//         public UserRepository(RmContext context) : base(context) {
//             Context.Configuration.LazyLoadingEnabled = false;
//         }

//         private EventLogRepository _eventLogRepository = new EventLogRepository();

//         /// <summary>
//         /// Возвращает список пользователей
//         /// </summary>
//         /// <param name="page">Текущая страница (отсчитываемая от нуля)</param>
//         /// <param name="length">Количество строк</param>
//         /// <param name="order">Массив порядков сортировки</param>
//         /// <param name="search">Строка поиска</param>
//         /// <returns></returns>
//         public IHelperResult GetUsersDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters) {
//             try {
//                 DataGridResponseModel<UserDataGrid> model = new DataGridResponseModel<UserDataGrid>();
//                 var query = GetAll<User>(DeletedSearchType.Actual, true, x => x.Department, x => x.Post, x => x.Filial, x => x.Roles.Select(r => r.Role))
//                     .Where(w => w.Id != UserGuid.SystemPGOSUserId);
//                 FilterData(ref query, filters);
//                 var orderedQuery = query.OrderBy(x => 0);
//                 SortData(ref orderedQuery, sorts);
//                 model.totalCount = orderedQuery.Count();
//                 model.data = orderedQuery.Skip(skip).Take(take).AsEnumerable()
//                     .Select(ModelToDataGridViewModel())
//                     .ToArray();
//                 return model;
//             } catch (Exception exp) {
//                 return DataGridResponseModel<UserDataGrid>.ErrorResponse(exp);
//             }
//         }

//         public Func<User, int, UserDataGrid> ModelToDataGridViewModel() {
//             return (x, index) => new UserDataGrid {
//                 Id = x.Id,
//                 FullName = x.GetFullName(),
//                 CreationDate = x.CreationDate.ToLocalTime().ToDataGridFormat(),
//                 Email = x.Email,
//                 Login = x.Login,
//                 FilialName = x.Filial?.Name,
//                 DepartmentName = x.Department?.Name,
//                 PostName = x.Post?.Name,
//                 Roles = string.Join("; ", x.Roles.Select(r => r.Role.Name)),
//                 Num = index + 1
//             };
//         }

//         public static void FilterData(ref IQueryable<User> source, DataGridFilter[] filters) {
//             if (filters == null || !filters.Any()) return;
//             foreach (var filter in filters) {
//                 filter.Value = filter.Value.Trim().ToLower();
//                 switch (filter.ColumnName) {
//                     case "FullName": {
//                             switch (filter.Type) {
//                                 case DataGridFilterType.Contains: {
//                                         source = source.AsEnumerable().Where(x => x.GetFullName().ToLower().Contains(filter.Value)).AsQueryable();
//                                         break;
//                                     }

//                                 case DataGridFilterType.NotContains: {
//                                         source = source.AsEnumerable().Where(x => !x.GetFullName().ToLower().Contains(filter.Value)).AsQueryable();
//                                         break;
//                                     }

//                                 case DataGridFilterType.StartsWith: {
//                                         source = source.AsEnumerable().Where(x => x.GetFullName().ToLower().StartsWith(filter.Value)).AsQueryable();
//                                         break;
//                                     }

//                                 case DataGridFilterType.EndsWith: {
//                                         source = source.AsEnumerable().Where(x => x.GetFullName().ToLower().EndsWith(filter.Value)).AsQueryable();
//                                         break;
//                                     }

//                                 case DataGridFilterType.Equals: {
//                                         source = source.AsEnumerable().Where(x => x.GetFullName().ToLower() == filter.Value).AsQueryable();
//                                         break;
//                                     }
//                                 case DataGridFilterType.NotEquals: {
//                                         source = source.AsEnumerable().Where(x => x.GetFullName().ToLower() != filter.Value).AsQueryable();
//                                         break;
//                                     }
//                             }
//                             break;
//                         }
//                     case "Login": {
//                             switch (filter.Type) {
//                                 case DataGridFilterType.Contains: {
//                                         source = source.Where(x => x.Login.ToLower().Contains(filter.Value));
//                                         break;
//                                     }

//                                 case DataGridFilterType.NotContains: {
//                                         source = source.Where(x => !x.Login.ToLower().Contains(filter.Value));
//                                         break;
//                                     }

//                                 case DataGridFilterType.StartsWith: {
//                                         source = source.Where(x => x.Login.ToLower().StartsWith(filter.Value));
//                                         break;
//                                     }

//                                 case DataGridFilterType.EndsWith: {
//                                         source = source.Where(x => x.Login.ToLower().EndsWith(filter.Value));
//                                         break;
//                                     }

//                                 case DataGridFilterType.Equals: {
//                                         source = source.Where(x => x.Login.ToLower() == filter.Value);
//                                         break;
//                                     }
//                                 case DataGridFilterType.NotEquals: {
//                                         source = source.Where(x => x.Login.ToLower() != filter.Value);
//                                         break;
//                                     }
//                             }
//                             break;
//                         }
//                     case "Email": {
//                             switch (filter.Type) {
//                                 case DataGridFilterType.Contains: {
//                                         source = source.Where(x => x.Email.ToLower().Contains(filter.Value));
//                                         break;
//                                     }

//                                 case DataGridFilterType.NotContains: {
//                                         source = source.Where(x => !x.Email.ToLower().Contains(filter.Value));
//                                         break;
//                                     }

//                                 case DataGridFilterType.StartsWith: {
//                                         source = source.Where(x => x.Email.ToLower().StartsWith(filter.Value));
//                                         break;
//                                     }

//                                 case DataGridFilterType.EndsWith: {
//                                         source = source.Where(x => x.Email.ToLower().EndsWith(filter.Value));
//                                         break;
//                                     }

//                                 case DataGridFilterType.Equals: {
//                                         source = source.Where(x => x.Email.ToLower() == filter.Value);
//                                         break;
//                                     }
//                                 case DataGridFilterType.NotEquals: {
//                                         source = source.Where(x => x.Email.ToLower() != filter.Value);
//                                         break;
//                                     }
//                             }
//                             break;
//                         }
//                     case "FilialName": {
//                             switch (filter.Type) {
//                                 case DataGridFilterType.Equals: {
//                                         source = source.Where(x => x.Filial.Name.ToLower() == filter.Value);
//                                         break;
//                                     }
//                                 case DataGridFilterType.NotEquals: {
//                                         source = source.Where(x => x.Filial.Name.ToLower() != filter.Value);
//                                         break;
//                                     }
//                             }
//                             break;
//                         }
//                     case "Roles": {
//                             switch (filter.Type) {
//                                 case DataGridFilterType.Contains: {
//                                         source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Role.Name.ToLower().Contains(filter.Value))).AsQueryable();
//                                         break;
//                                     }
//                                 case DataGridFilterType.NotContains: {
//                                         source = source.AsEnumerable().Where(x => x.Roles.All(r => !r.Role.Name.ToLower().Contains(filter.Value))).AsQueryable();
//                                         break;
//                                     }
//                                 case DataGridFilterType.StartsWith: {
//                                         source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Role.Name.ToLower().StartsWith(filter.Value))).AsQueryable();
//                                         break;
//                                     }
//                                 case DataGridFilterType.EndsWith: {
//                                         source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Role.Name.ToLower().EndsWith(filter.Value))).AsQueryable();
//                                         break;
//                                     }
//                                 case DataGridFilterType.Equals: {
//                                         source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Role.Name.ToLower() == filter.Value)).AsQueryable();
//                                         break;
//                                     }
//                                 case DataGridFilterType.NotEquals: {
//                                         source = source.AsEnumerable().Where(x => x.Roles.Any(r => r.Role.Name.ToLower() != filter.Value)).AsQueryable();
//                                         break;
//                                     }
//                             }
//                             break;
//                         }
//                 }
//             }
//         }

//         public static void SortData(ref IOrderedQueryable<User> source, DataGridSort[] sorts) {
//             if (sorts == null || !sorts.Any()) return;
//             foreach (var sort in sorts) {
//                 switch (sort.ColumnName) {
//                     case "FullName": {
//                             source = sort.Type == DataGridSortType.Descending
//                                 ? source.ThenByDescending(x => x.Surname)
//                                 : source.ThenBy(x => x.Surname);
//                             break;
//                         }
//                     case "Login": {
//                             source = sort.Type == DataGridSortType.Descending
//                                 ? source.ThenByDescending(x => x.Login)
//                                 : source.ThenBy(x => x.Login);
//                             break;
//                         }
//                     case "Email": {
//                             source = sort.Type == DataGridSortType.Descending
//                                 ? source.ThenByDescending(x => x.Email)
//                                 : source.ThenBy(x => x.Email);
//                             break;
//                         }
//                     case "FilialName": {
//                             source = sort.Type == DataGridSortType.Descending
//                                 ? source.ThenByDescending(x => x.Filial.Name)
//                                 : source.ThenBy(x => x.Filial.Name);
//                             break;
//                         }
//                     case "Roles": {
//                             source = sort.Type == DataGridSortType.Descending
//                                 ? source.ThenByDescending(x => x.Roles.Count())
//                                 : source.ThenBy(x => x.Roles.Count());
//                             break;
//                         }
//                 }
//             }
//         }
//         /// <summary>
//         /// Преобразовывает сущности пользователя в модель представления пользователя
//         /// </summary>
//         /// <param name="context">Контекст базы данных</param>
//         /// <param name="model">Сущность пользователя</param>
//         /// <returns></returns>
//         public override Func<User, UserViewModel> ModelToViewModel() {
//             return model => new UserViewModel {
//                 Id = model.Id,
//                 FilialId = model.FilialId,
//                 FilialDateChange = model.FilialDateChange,
//                 FilialDateChangeView = ConvertDateToStringHaveMilissecond(model.FilialDateChange),
//                 Name = model.Name,
//                 Surname = model.Surname,
//                 Patronymic = model.Patronymic,
//                 Email = model.Email,
//                 Login = model.Login,
//                 DepartmentName = model.Department?.Name,
//                 PostName = model.Post?.Name,
//                 FilialName = model.Filial?.Name,
//                 RoleNames = model.Roles != null && model.Roles.All(x => x.Role != null) ? string.Join("; ", model.Roles.Select(x => x.Role.Name)).TrimEnd(';') : "",
//                 Roles = model.Roles != null ? model.Roles.Select(x => x.RoleId).ToList() : null,
//                 PostId = model.PostId ?? Guid.Empty,
//                 DepartmentId = model.DepartmentId ?? Guid.Empty,
//                 NotificationSettings = new UserProfileNotificationSettings() {
//                     ChangeStatus = new NotificationSendType { Email = model.ChangeStatusNotificationEmail, System = model.ChangeStatusNotificationSystem },
//                     StartStage = new NotificationSendType { Email = model.StartStageNotificationEmail, System = model.StartStageNotificationSystem },
//                     StopStage = new NotificationSendType { Email = model.StopStageNotificationEmail, System = model.StopStageNotificationSystem },
//                     StartTechProcess = new NotificationSendType { Email = model.StartTechProcessNotificationEmail, System = model.StartTechProcessNotificationSystem },
//                     EndTechProcess = new NotificationSendType { Email = model.EndTechProcessNotificationEmail, System = model.EndTechProcessNotificationSystem },
//                     NotActivity = new NotificationSendType { Email = model.NotActivityNotificationEmail, System = model.NotActivityNotificationSystem }
//                 }
//             };
//         }

//         /// <summary>
//         /// Добавление редактирование пользователя
//         /// </summary>
//         /// <param name="userId"></param>
//         /// <param name="filialId"></param>
//         /// <param name="userLogin"></param>
//         /// <param name="userEmail"></param>
//         /// <param name="userName"></param>
//         /// <param name="userSurname"></param>
//         /// <param name="userPatronymic"></param>
//         /// <returns></returns>
//         public BaseResponse<UserViewModel> AddEditUser(Guid? userId, Guid filialId, DateTime? dateChangeFilial,
//             Guid? departmentId, Guid? postId, string userLogin, string userEmail, string userName, string userSurname,
//             string userPatronymic, UserInfo currentUser) {
//             try {
//                 User user = null;
//                 string msg;
//                 SystemEventType type;
//                 if (!userId.HasValue) {
//                     msg = "Создание";
//                     user = Context.Users.FirstOrDefault(w => w.Login == userLogin && !w.IsDeleted);
//                     if (user != null) {
//                         return new BaseResponse<UserViewModel>(
//                             new Exception("Пользователь с данным логином уже существует"));
//                     }

//                     FilialViewModel filial = null;
//                     if (!dateChangeFilial.HasValue) {
//                         filial = filialRepository.GetViewByTemporaryKey<Filial>(filialId, null, true);
//                     }

//                     if (filial == null && !dateChangeFilial.HasValue) {
//                         return new BaseResponse<UserViewModel>("Не указан филиал для пользователя");
//                     }

//                     user = new User(
//                         userLogin,
//                         userSurname,
//                         userName,
//                         userPatronymic,
//                         userEmail,
//                         filialId,
//                         filial?.DateChange ?? dateChangeFilial.Value,
//                         departmentId,
//                         postId,
//                         false,
//                         null);
//                     Context.Users.Add(user);
//                     type = SystemEventType.Create;
//                 } else {
//                     msg = "Редактирование";
//                     user = Context.Users.FirstOrDefault(w => w.Id == userId);
//                     if (user == null) {
//                         return new BaseResponse<UserViewModel>(new Exception("Пользователь отсутствует"));
//                     }

//                     user.Name = userName;
//                     user.Surname = userSurname;
//                     user.Patronymic = userPatronymic;
//                     user.Email = userEmail;
//                     user.Login = userLogin;
//                     user.FilialId = filialId;
//                     user.DepartmentId = departmentId;
//                     user.PostId = postId;

//                     type = SystemEventType.Update;
//                 }

//                 Context.SaveChanges();
//                 if (currentUser != null)
//                     eventLogRepository.AddRecordAsync(SubSystemType.Users, type, msg + " пользователя", "", currentUser);
//                 return new BaseResponse<UserViewModel>(ModelToViewModel().Invoke(user));
//             } catch (Exception exp) {
//                 if (currentUser != null)
//                     eventLogRepository.AddRecordAsync(SubSystemType.Users, SystemEventType.Error, exp, "", currentUser);
//                 return new BaseResponse<UserViewModel>(exp);
//             }
//         }

//         public BaseResponseEnumerable DeleteUsers(IEnumerable<Guid> userIds) {
//             try {
//                 BaseResponseEnumerable model = new BaseResponseEnumerable(new Exception());
//                 var delUsers = Context.Users.Where(x => userIds.Contains(x.Id)).ToList();
//                 foreach (var user in delUsers) {
//                     user.IsDeleted = true;
//                 }

//                 model = new BaseResponseEnumerable(delUsers.Select(ModelToViewModel()));
//                 Context.SaveChanges();
//                 return model;
//             } catch (Exception exp) {
//                 return new BaseResponseEnumerable(exp);
//             }
//         }

//         public BaseResponse SynchronizeWithADFromController(UserInfo userInfo) {
//             SynchronizeWithAD(out Exception error);
//             string logResult;
//             if (error == null) {
//                 bool result = _eventLogRepository.LogEvent(Entities.Enums.SubSystemType.Users, Entities.Enums.SystemEventType.Create,
//                     "Синхронизация пользователей с Active Directory", "Синхронизация прошла успешно", userInfo, out logResult);
//                 if (!result) {
//                     return new BaseResponse(logResult);
//                 }
//                 return new BaseResponse(new BaseResponseModel());
//             }
//             _eventLogRepository.LogEvent(Entities.Enums.SubSystemType.Users, Entities.Enums.SystemEventType.Error, "Синхронизация пользователей с Active Directory",
//                 "Ошибка при синхронизации", userInfo, out logResult);
//             return new BaseResponse(error);
//         }

//         public bool SynchronizeWithAD(out Exception error) {
//             //string dbg = "";
//             try {
//                 //dbg = "111";
//                 RoleRepository roleRepository = new RoleRepository();
//                 FilialRepository filialRepository = new FilialRepository();
//                 //dbg = "222";
//                 var ADGroups = roleRepository.GetAllADGroups();
//                 //dbg = "333";
//                 var filials = filialRepository.GetAllTemporary<Filial>(true).GetByStatus(DeletedSearchType.Actual).ToList();
//                 //dbg = "444";
//                 return SynchronizeWithAD(out error, ADGroups, filials);
//             } catch (Exception exp) {
//                 error = exp;
//                 //error = new Exception(dbg + "|" + exp.ToString());
//                 return false;
//             }
//         }

//         public bool SynchronizeWithAD(out Exception error, List<string> ADGroups, List<Filial> filials) {
//             error = null;
//             try {

//                 // Self referencing loop detected for property 'User' with type 'SysRM.Entities.Models.References.Administration.User'. Path 'Error.SafeSerializationManager.m_serializedStates[0].EntityValidationErrors[0].Entry.Entity.Roles[0]'.


//                 // Отладка 01.07.2021

//                 //#if DEBUG
//                 //				Console.WriteLine("SynchronizeWithAD start");
//                 //#endif
//                 //				System.IO.File.AppendAllText("D:\\Temp\\adlog.txt", $"SynchronizeWithAD start\n\r");

//                 if (!ADGroups.Any()) {
//                     error = new Exception("Список групп пуст");
//                     return false;
//                 }

//                 if (ADGroups.Any(x => string.IsNullOrEmpty(x))) {
//                     error = new Exception("Список групп имеет пустую группу");
//                     return false;
//                 }

//                 var ADSearcher = new ADSearcher();
//                 List<KeyValuePair<Guid, string>> AllRoles = new List<KeyValuePair<Guid, string>>();
//                 var Roles = Context.Roles.Where(w => w.GroupAd != null).ToList();
//                 foreach (var role in Roles) {
//                     var data = role.GroupAd.Split(';').ToList();
//                     foreach (var r in data) {
//                         var newPair = new KeyValuePair<Guid, string>(role.Id, r.ToLower().Trim());
//                         AllRoles.Add(newPair);
//                     }
//                 }

//                 //#if DEBUG
//                 //				Console.WriteLine($"Roles: {AllRoles.Count}");
//                 //#endif
//                 //				System.IO.File.AppendAllText("D:\\Temp\\adlog.txt", $"Roles: {AllRoles.Count}\n\r");

//                 //foreach (var data in ADGroups) {
//                 //	 List<string> list = new List<string>();
//                 //	list.Add(data);
//                 var ADUsers = ADSearcher.FindByGroup(out error, ADGroups);

//                 //#if DEBUG
//                 //foreach (var adu in ADUsers) {
//                 //	Console.WriteLine($"[{adu.WinLogin}]: {string.Join(",", adu.Groups)}");
//                 //}
//                 //#endif

//                 if (error != null) {
//                     return false;
//                 }


//                 // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//                 //string fileName = System.IO.Path.Combine("D:\\Temp\\", "ADUsers" + DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(".", "") + ".xml");
//                 //TMK.Utils.Helpers.SerializerHelper.SerializeData<List<ADUser>>(out error, fileName, ADUsers);
//                 //if (error != null) {
//                 //	return false;
//                 //}




//                 //#if DEBUG
//                 //				Console.WriteLine($"ADSearcher.FindByGroup found: {ADUsers.Count}");
//                 //				foreach(var asdu in ADUsers.Where(x => x.WinLogin.ToLower().Contains("asdu"))) {
//                 //					Console.WriteLine($"GetFindUser groups [{asdu.WinLogin}]: {string.Join(",", asdu.Groups)}");
//                 //				}
//                 //#endif
//                 //				System.IO.File.AppendAllText("D:\\Temp\\adlog.txt", $"ADSearcher.FindByGroup found: {ADUsers.Count}\n\r");
//                 //				foreach (var asdu in ADUsers.Where(x => x.WinLogin.ToLower().Contains("asdu"))) {
//                 //					System.IO.File.AppendAllText("D:\\Temp\\adlog.txt", $"GetFindUser groups [{asdu.WinLogin}]: {string.Join(",", asdu.Groups)}\n\r");
//                 //				}

//                 //WriteFileUserAD(ADUsers);
//                 var newUsers = new List<User>();
//                 var DBUsers = GetAll<User>()
//                                 .Include(i => i.Roles)
//                                 .Include(i => i.Roles.Select(s => s.Role))
//                                 .Where(x => !x.IsDeleted)
//                                 .Where(w => w.Id != UserGuid.SystemPGOSUserId)
//                                 .ToList();

//                 foreach (var ADUser in ADUsers) {
//                     var user = DBUsers.FirstOrDefault(x => x.Login == ADUser.WinLogin);

//                     var nameAndPatronymic = GetNameAndPatronymic(ADUser.GivenName);
//                     ADUser.Surname = string.IsNullOrWhiteSpace(ADUser.Surname) ? "Не указана" : ADUser.Surname;
//                     if (user == null) {
//                         //ADUser.CompanyName = (ADUser.CompanyName == "Исполнительный аппарат" ? "СО ЕЭС" : ADUser.CompanyName);
//                         var userFilial = filials.FirstOrDefault(x => x.Name.ToLower() == ADUser.CompanyName.ToLower());
//                         if (userFilial != null) {
//                             if (!string.IsNullOrEmpty(ADUser.Surname)) {
//                                 newUsers.Add(new User(
//                                     ADUser.WinLogin,
//                                     ADUser.Surname,
//                                     nameAndPatronymic.Name,
//                                     nameAndPatronymic.Patronymic,
//                                     ADUser.EmailAddress,
//                                     userFilial.Id,
//                                     userFilial.DateChange,
//                                     null,
//                                     null));
//                             }
//                         }
//                     } else {
//                         var userFilial = filials.FirstOrDefault(x => x.Name.ToLower() == ADUser.CompanyName.ToLower());
//                         if (user.Email != ADUser.EmailAddress ||
//                                 user.Name != nameAndPatronymic.Name ||
//                                 user.Patronymic != nameAndPatronymic.Patronymic ||
//                                 user.Surname != ADUser.Surname ||
//                                 (userFilial != null && user.FilialId != userFilial.Id) ||
//                                 (userFilial != null && user.FilialDateChange != userFilial.DateChange)) {
//                             user.Email = ADUser.EmailAddress;
//                             user.Name = nameAndPatronymic.Name;
//                             user.Patronymic = nameAndPatronymic.Patronymic;
//                             user.Surname = ADUser.Surname;
//                             if (userFilial != null) {
//                                 user.FilialId = userFilial.Id;
//                                 user.FilialDateChange = userFilial.DateChange;
//                             }
//                             Context.SaveChanges();
//                         }

//                         //#if DEBUG
//                         //						if (user.Login == "cdu\\asdu-test")
//                         //							Console.WriteLine($"asdu-test old roles: {string.Join(",", user.Roles?.Select(s => s.Role?.Name ?? "NULL")) ?? "UNDEF"}");
//                         //#endif
//                         //						if (user.Login == "cdu\\asdu-test")
//                         //							System.IO.File.AppendAllText("D:\\Temp\\adlog.txt", $"asdu-test old roles: {string.Join(",", user.Roles?.Select(s => s.Role?.Name ?? "NULL")) ?? "UNDEF"}\n\r");


//                         UpdateRoleNewUserSyncAD(out error, user, ADUser, AllRoles);
//                         if (error != null)
//                             return false;

//                         //#if DEBUG
//                         //						if (user.Login == "cdu\\asdu-test")
//                         //							Console.WriteLine($"asdu-test new roles: {string.Join(",", user.Roles?.Select(s => s.Role?.Name ?? "NULL")) ?? "UNDEF"}");
//                         //#endif
//                         //						if (user.Login == "cdu\\asdu-test")
//                         //							System.IO.File.AppendAllText("D:\\Temp\\adlog.txt", $"asdu-test new roles: {string.Join(",", user.Roles?.Select(s => s.Role?.Name ?? "NULL")) ?? "UNDEF"}\n\r");

//                     }
//                 }

//                 //#if DEBUG
//                 //				Console.WriteLine($"newUsers: {newUsers.Count} DBUsers: {DBUsers.ToList().Count}");
//                 //#endif
//                 //				System.IO.File.AppendAllText("D:\\Temp\\adlog.txt", $"newUsers: {newUsers.Count} DBUsers: {DBUsers.ToList().Count}\n\r");

//                 if (newUsers.Any()) {
//                     Context.Users.AddRange(newUsers);
//                     Context.SaveChanges();
//                     UpdateRoleNewListUsersSyncAD(out error, newUsers, ADUsers, AllRoles);
//                     if (error != null)
//                         return false;
//                 }

//                 // Убрать все роли пользователей, которых нет в AD
//                 var DBUsers2 = GetAll<User>().Include(i => i.Roles).Where(w => w.Id != UserGuid.SystemPGOSUserId).ToList();
//                 var delUsers = DBUsers2.Where(x => !ADUsers.Any(y => y.WinLogin == x.Login) && (x.Roles.Any() || !x.IsDeleted));
//                 foreach (var u in delUsers) {
//                     var roles = Context.UserRoles.Where(x => x.UserId == u.Id).ToList();
//                     if (roles.Any())
//                         Context.UserRoles.RemoveRange(roles);
//                     u.IsDeleted = true;
//                     Context.SaveChanges();
//                 }

//                 //#if DEBUG
//                 //				Console.WriteLine($"SynchronizeWithAD end");
//                 //#endif
//                 //				System.IO.File.AppendAllText("D:\\Temp\\adlog.txt", $"SynchronizeWithAD end\n\r");

//                 return true;
//             } catch (Exception exp) {
//                 error = exp;
//                 //error = new Exception(exp.ToString());
//                 return false;
//             }
//         }

//         public BaseResponse UpdateRoleNewUserSyncAD(out Exception error, User user, ADUser aDUser, List<KeyValuePair<Guid, string>> AllRoles) {
//             error = null;
//             try {
//                 var groupUsers = aDUser.Groups.Select(s => s.ToLower().Trim());
//                 var actualRoleUser = AllRoles.Where(w => groupUsers.Contains(w.Value)).Select(s => s.Key).Distinct().ToList();
//                 var oldRole = user.Roles.Where(w => !actualRoleUser.Contains(w.RoleId));
//                 if (oldRole.Any()) {
//                     Context.UserRoles.RemoveRange(oldRole);
//                     Context.SaveChanges();
//                 }
//                 //var userRole = Context.UserRoles.Where(w => w.UserId == user.Id).ToList();
//                 List<UserRole> newRoles = new List<UserRole>();
//                 foreach (var role in actualRoleUser) {
//                     //if (userRole.FirstOrDefault(f => f.RoleId == role) == null) {
//                     if (user.Roles.FirstOrDefault(f => f.RoleId == role) == null) {
//                         newRoles.Add(new UserRole() {
//                             UserId = user.Id,
//                             RoleId = role
//                         });
//                     }
//                 }
//                 if (newRoles.Any()) {
//                     Context.UserRoles.AddRange(newRoles);
//                     Context.SaveChanges();
//                 }
//                 return new BaseResponse(new BaseResponseModel());
//             } catch (Exception exc) {
//                 error = exc;
//                 return new BaseResponse(exc);
//             }
//         }

//         public BaseResponse UpdateRoleNewListUsersSyncAD(out Exception error, List<User> ListUser, List<ADUser> ListADUsers, List<KeyValuePair<Guid, string>> AllRoles) {
//             error = null;
//             try {
//                 foreach (var user in ListUser) {
//                     var userRole = Context.UserRoles.Where(w => w.UserId == user.Id);
//                     Context.UserRoles.RemoveRange(userRole);
//                 }
//                 foreach (var user in ListUser) {
//                     var groupUsers = ListADUsers.FirstOrDefault(s => s.WinLogin == user.Login)?.Groups.Select(s => s.ToLower().Trim());
//                     var actualRoleUser = AllRoles.Where(w => groupUsers.Contains(w.Value)).Select(s => s.Key).Distinct().ToList();
//                     foreach (var role in actualRoleUser) {
//                         Context.UserRoles.Add(new UserRole() {
//                             UserId = user.Id,
//                             RoleId = role
//                         });
//                     }
//                 }
//                 Context.SaveChanges();
//                 return new BaseResponse(new BaseResponseModel());
//             } catch (Exception exc) {
//                 error = exc;
//                 return new BaseResponse(exc);
//             }
//         }
//         public BaseResponse ChangeNotificationSettings(UserProfileNotificationSettings notificationSettings, Guid? userId) {
//             if (!userId.HasValue) {
//                 return new BaseResponse(new Exception());
//             }

//             var user = GetEntityById<User>(userId.Value, DeletedSearchType.Actual);
//             if (user == null) {
//                 return new BaseResponse(new Exception());
//             }

//             user.ChangeStatusNotificationEmail = notificationSettings.ChangeStatus.Email;
//             user.ChangeStatusNotificationSystem = notificationSettings.ChangeStatus.System;
//             user.StartStageNotificationEmail = notificationSettings.StartStage.Email;
//             user.StartStageNotificationSystem = notificationSettings.StartStage.System;
//             user.StopStageNotificationEmail = notificationSettings.StopStage.Email;
//             user.StopStageNotificationSystem = notificationSettings.StopStage.System;
//             user.StartTechProcessNotificationEmail = notificationSettings.StartTechProcess.Email;
//             user.StartTechProcessNotificationSystem = notificationSettings.StartTechProcess.System;
//             user.EndTechProcessNotificationEmail = notificationSettings.EndTechProcess.Email;
//             user.EndTechProcessNotificationSystem = notificationSettings.EndTechProcess.System;
//             user.NotActivityNotificationEmail = notificationSettings.NotActivity.Email;
//             user.NotActivityNotificationSystem = notificationSettings.NotActivity.System;
//             Context.SaveChanges();
//             return new BaseResponse(new BaseResponseModel());
//         }
//         public UserNamePatronymic GetNameAndPatronymic(string GivenName) {
//             var obj = GivenName.Split(' ');
//             return new UserNamePatronymic(obj[0], obj.Length > 1 ? obj[1] : null);
//         }
//         /// <summary>
//         /// Найти пользователей
//         /// </summary>
//         /// <param name="search">текст для поиска</param>
//         /// <returns></returns>
//         /// <summary>
//         /// Найти пользователей
//         /// </summary>
//         /// <param name="search">текст для поиска</param>
//         /// <returns></returns>
//         public BaseResponseEnumerable FindUsers(Guid[] Keys, Guid[] exceptIds, string search, Guid? filialId = null) {
//             try {
//                 if (Keys == null) {
//                     Keys = new Guid[0];
//                 }

//                 IEnumerable<ListItemResponseModel<Guid>> listItemResponseModels = new ListItemResponseModel<Guid>[0];
//                 var query = GetAll<User>(DeletedSearchType.Actual, true).Where(x => x.Id != UserGuid.SystemPGOSUserId);
//                 if (!string.IsNullOrEmpty(search)) {
//                     var searchs = search.Trim().Split(' ').ToList();
//                     if (searchs.Count == 1) {
//                         var text = searchs[0];
//                         bool isEngl = Regex.IsMatch(text, "^[a-zA-Z0-9]*$");
//                         if (isEngl) {
//                             query = text.Contains('@')
//                                 ? query.Where(x => x.Email.Contains(text))
//                                 : query.Where(x => x.Email.Contains(text) || x.Login.Contains(text));
//                         } else {
//                             query = query.Where(x =>
//                                 x.Name.Contains(text) || x.Surname.Contains(text) || x.Patronymic.Contains(text) ||
//                                 x.Login.Contains(text));
//                         }
//                     } else {
//                         search = "";
//                         foreach (var s in searchs)
//                             search += s.ToLower();
//                         query = query.Where(x =>
//                             (x.Surname + x.Name + x.Patronymic + x.Login).ToLower().Trim().StartsWith(search));
//                     }
//                 }

//                 if (Keys.Any()) {
//                     query = query.Union(GetAll<User>(DeletedSearchType.Actual, true).Where(x => Keys.Contains(x.Id)));
//                 }

//                 if (filialId.HasValue) {
//                     var filialIds = filialRepository.GetParentFilials(filialId.Value).Select(x => x.Id).ToArray();
//                     query = query.Where(x => filialIds.Contains(x.FilialId));
//                 }

//                 if (exceptIds != null && exceptIds.Length > 0)
//                     query = query.Where(q => !exceptIds.Contains(q.Id));

//                 listItemResponseModels = query
//                     .OrderByDescending(x => !Keys.Any() || Keys.Contains(x.Id))
//                     .ThenBy(x => x.Surname)
//                     .Take((Keys.Any() ? Keys.Length : 10) + 10)
//                     .Select(x => new ListItemResponseModel<Guid> {
//                         Key = x.Id,
//                         Text = x.Surname + " " + x.Name + " " + x.Patronymic + " (" + x.Login + ")"
//                     }).ToArray();

//                 return new BaseResponseEnumerable(listItemResponseModels);
//             } catch (Exception ex) {
//                 return new BaseResponseEnumerable(ex);
//             }
//         }

//         /// <summary>
//         /// Обновить пользователя
//         /// </summary>
//         /// <param name="userAd">Пользователь в AD</param>
//         /// <param name="userDb">Пользователь из БД</param>
//         /// <returns></returns>
//         public BaseResponse<UserViewModel> UpdateUserFormAD(ADUser userAd, UserViewModel userDb) {
//             try {
//                 if (userDb == null) {
//                     var nm = GetNameAndPatronymic(userAd.GivenName);
//                     userDb = new UserViewModel {
//                         Email = userAd.EmailAddress,
//                         Login = userAd.WinLogin,
//                         Name = string.IsNullOrWhiteSpace(nm.Name) ? "Не указана" : nm.Name,
//                         Patronymic = nm.Patronymic,
//                         Surname = string.IsNullOrWhiteSpace(userAd.Surname) ? "Не указана" : userAd.Surname
//                     };
//                 }

//                 return new BaseResponse<UserViewModel>(userDb);
//             } catch (Exception ex) {
//                 return new BaseResponse<UserViewModel>(ex);
//             }
//         }

//         /// <summary>
//         /// Получить пользователя по логину
//         /// </summary>
//         /// <param name="login">Логин пользователя</param>
//         /// <returns></returns>
//         public BaseResponse<UserViewModel> GetUserByLogin(string login) {
//             try {
//                 //var query = GetAll<User>(true, x => x.Filial);
//                 //var countUserDataBase = query.Count();
//                 //var user = query.GetUserByLogin(login, ModelToViewModel());

//                 /*
// 				int countUserDataBase = 1;
// 				if (string.IsNullOrEmpty(login)) {
// 					return new BaseResponse<UserViewModel>(new UserViewModel(), new Exception("Пользователь не найден"),
// 						countUserDataBase);
// 				}
// 				var query = Context.Users
// 					.Include(i => i.Filial)
// 					.Include(i => i.Department)
// 					.Include(i => i.Post)
// 					.Include(i => i.Roles.Select(s => s.Role))
// 					.Where(x => x.Login.ToUpper() == login.ToUpper());
// 				var user = query.AsNoTracking().ToList().Select(ModelToViewModel()).FirstOrDefault();
// 				if (user == null) {
// 					return new BaseResponse<UserViewModel>(new UserViewModel(), new Exception("Пользователь не найден"),
// 						countUserDataBase);
// 				}
// 				return new BaseResponse<UserViewModel>(user);
// 				*/

//                 /*
// 				int countUserDataBase = 1;
// 				var query = Context.Users
// 					.Include(i => i.Filial)
// 					.Include(i => i.Department)
// 					.Include(i => i.Post)
// 					.Where(x => x.Login.ToUpper() == login.ToUpper());
// 				var user = query.AsNoTracking().FirstOrDefault();
// 				if (user == null) 
// 					return new BaseResponse<UserViewModel>(new UserViewModel(), new Exception("Пользователь не найден"), countUserDataBase);
// 				return new BaseResponse<UserViewModel>(user);
// 				*/


//                 //return GetUserByIdName(new Guid("5DAD096D-3930-42F1-895A-A134BE871BA6"));



//                 if (string.IsNullOrEmpty(login))
//                     return new BaseResponse<UserViewModel>(new UserViewModel(), new Exception("Логин пользователя не определен"), 0);
//                 login = login.ToUpper();
//                 var id = Context.Users.FirstOrDefault(x => x.Login.ToUpper() == login && !x.IsDeleted)?.Id;
//                 if (!id.HasValue)
//                     return new BaseResponse<UserViewModel>(new UserViewModel(), new Exception("Пользователь не найден"), 0);
//                 return GetUserByIdName(id);
//             } catch (Exception ex) {
//                 return new BaseResponse<UserViewModel>(ex);
//             }
//         }

//         /// <summary>
//         /// Метод получения логина и имени пользователся
//         /// </summary>
//         /// <param name="Id"></param>
//         /// <returns></returns>
//         public BaseResponse<UserViewModel> GetUserByIdName(Guid? Id) {
//             try {

//                 //return new BaseResponse<UserViewModel>(new UserViewModel
//                 //{
//                 //	Id = new Guid("5DAD096D-3930-42F1-895A-A134BE871BA6"),
//                 //	FilialId = new Guid("5E7ECA5D-6FC9-4B92-B249-221CA4B4AEE6"),
//                 //	FilialDateChange = new DateTime(2019, 8, 22, 7, 57, 56, 411),
//                 //	FilialDateChangeView = ConvertDateToStringHaveMilissecond(new DateTime(2019, 8, 22, 7, 57, 56, 411)),
//                 //	Name = "Тест",
//                 //	Surname = "Пользователь",
//                 //	Patronymic = "",
//                 //	Email = "sinetsky@tmc-center.ru",
//                 //	Login = "cdu\\sinetskii-rm",
//                 //	DepartmentName = "",
//                 //	PostName = "",
//                 //	FilialName = "Исполнительный аппарат",
//                 //	RoleNames = "Администратор ИА",
//                 //	Roles = new List<Guid> { new Guid("046B0508-F051-4023-862F-F44E2C2F9DFE") },
//                 //	PostId = Guid.Empty,
//                 //	DepartmentId = Guid.Empty,
//                 //	NotificationSettings = new UserProfileNotificationSettings()
//                 //	{
//                 //		ChangeStatus = new NotificationSendType { Email = false, System = false },
//                 //		StartStage = new NotificationSendType { Email = false, System = false },
//                 //		StopStage = new NotificationSendType { Email = false, System = false },
//                 //		StartTechProcess = new NotificationSendType { Email = false, System = false },
//                 //		NotActivity = new NotificationSendType { Email = false, System = false }
//                 //	}

//                 //});



//                 UserViewModel user = new UserViewModel();
//                 if (Id.HasValue) {
//                     //Context.Configuration.LazyLoadingEnabled = false;
//                     //var query = GetAll<User>(true, x => x.Filial, x => x.Post, x => x.Department, x => x.Roles.Select(y => y.Role));
//                     //user = query.GetUserById(Id, ModelToViewModel());
//                     //Context.Configuration.LazyLoadingEnabled = true;


//                     var query = Context.Users.Where(x => x.Id == Id.Value);
//                     user = query.AsNoTracking().Select(ModelToViewModel()).FirstOrDefault();
//                     if (user != null) {
//                         var filial = Context.Filials.FirstOrDefault(x => x.Id == user.FilialId && x.DateChange == user.FilialDateChange);
//                         user.FilialName = filial?.Name ?? "";
//                         if (user.PostId.HasValue) {
//                             var post = Context.Posts.FirstOrDefault(x => x.Id == user.PostId);
//                             user.PostName = post?.Name ?? "";
//                         }
//                         if (user.DepartmentId.HasValue) {
//                             var dept = Context.Departments.FirstOrDefault(x => x.Id == user.DepartmentId);
//                             user.DepartmentName = dept?.Name ?? "";
//                         }
//                         user.Roles = Context.UserRoles.Where(x => x.UserId == Id.Value).Select(s => s.RoleId).ToList();
//                         if (user.Roles.Count > 0) {
//                             var roles = Context.Roles.Where(x => user.Roles.Contains(x.Id)).ToList();
//                             user.RoleNames = string.Join(",", roles.Select(s => s.Name));
//                         }
//                     }


//                 } else {
//                     user = null;
//                 }

//                 return new BaseResponse<UserViewModel>(user);
//             } catch (Exception ex) {
//                 //            try {
//                 //	Context.Configuration.LazyLoadingEnabled = true;
//                 //} catch(Exception) { }
//                 return new BaseResponse<UserViewModel>(ex);
//             }
//         }

//         /// <summary>
//         /// Метод получения актуальных пользователей
//         /// </summary>
//         /// <param name="Id"></param>
//         /// <returns></returns>
//         public IQueryable<User> GetAllActualUsers(bool isNoTracking = false,
//             params Expression<Func<User, object>>[] includes) {
//             try {
//                 return GetAll(isNoTracking, includes).Where(x => !x.IsDeleted);
//             } catch (Exception) {
//                 return null;
//             }
//         }
//         public BaseResponse GetIndicatorModel(Guid id, Guid? userId) {
//             try {
//                 var indEnt = Context.Indicators.AsNoTracking().FirstOrDefault(x => x.Id == id && !x.IsDeleted);
//                 if (indEnt == null)
//                     return new BaseResponse("Ошибка! Не удалось определить показатель");
//                 var indUserEnt = Context.IndicatorUsers.Include(i => i.Indicator).AsNoTracking().FirstOrDefault(ind => ind.IndicatorId == indEnt.Id && ind.UserId == userId);

//                 var userFilialId = Context.Users.FirstOrDefault(x => x.Id == userId && !x.IsDeleted).FilialId;
//                 var filialInd = Context.FilialIndicatorUsers.Include(x => x.FilialIndicator).FirstOrDefault(x => x.UserId == userId && (x.FilialIndicator != null ? x.FilialIndicator.IndicatorId == indEnt.Id : false) && (x.FilialIndicator != null ? x.FilialIndicator.FilialId == userFilialId : false))?.FilialIndicator;

//                 if (indUserEnt == null)
//                     return new BaseResponse(new IndicatorViewModel {
//                         Id = indEnt.Id,
//                         UserDeadlineForSubmitting = filialInd?.DeadlineForSubmitting?.ToLocalTime() ?? indEnt.DeadlineForSubmitting?.ToLocalTime() ?? DateTime.Now,
//                         ReportingPeriodType = (int)indEnt.ReportingPeriodType,
//                         NativeDeadlineForSubmitting = filialInd?.DeadlineForSubmitting?.ToLocalTime() ?? indEnt.DeadlineForSubmitting?.ToLocalTime() ?? DateTime.Now,
//                         NativeDeadlineForSubmittingDay = filialInd?.DeadlineForSubmitting?.ToLocalTime().Day ?? indEnt.DeadlineForSubmitting?.ToLocalTime().Day ?? DateTime.UtcNow.Day,
//                         NativeDeadlineForSubmittingMonth = filialInd?.DeadlineForSubmitting?.ToLocalTime().Month ?? indEnt.DeadlineForSubmitting?.ToLocalTime().Month ?? DateTime.UtcNow.Month,
//                         DeadlineForSubmittingType = GetDeadlineType(indEnt.ReportingPeriodType, indEnt.DeadlineForSubmitting, out int day, out int mounth),
//                         Day = day,
//                         Month = mounth,
//                     });
//                 else
//                     return new BaseResponse(new IndicatorViewModel {
//                         Id = indEnt.Id,
//                         UserDeadlineForSubmitting = indUserEnt.UserDeadlineForSubmitting?.ToLocalTime(),
//                         ReportingPeriodType = (int)indUserEnt.Indicator.ReportingPeriodType,
//                         NativeDeadlineForSubmitting = filialInd?.DeadlineForSubmitting?.ToLocalTime() ?? indEnt.DeadlineForSubmitting?.ToLocalTime() ?? DateTime.Now,
//                         NativeDeadlineForSubmittingDay = filialInd?.DeadlineForSubmitting?.ToLocalTime().Day ?? indEnt.DeadlineForSubmitting?.ToLocalTime().Day ?? DateTime.UtcNow.Day,
//                         NativeDeadlineForSubmittingMonth = filialInd?.DeadlineForSubmitting?.ToLocalTime().Month ?? indEnt.DeadlineForSubmitting?.ToLocalTime().Month ?? DateTime.UtcNow.Month,
//                         DeadlineForSubmittingType = GetDeadlineType(indEnt.ReportingPeriodType, indUserEnt?.UserDeadlineForSubmitting ?? indEnt.DeadlineForSubmitting, out int day, out int mounth),
//                         Day = day,
//                         Month = mounth,
//                     });
//             } catch (Exception ex) {
//                 return new BaseResponse(ex);
//             }
//         }
//         /// <summary>
//         /// Получение сущбностей для вывода в таблицу
//         /// </summary>
//         /// <param name="skip"></param>
//         /// <param name="take"></param>
//         /// <param name="sorts"></param>
//         /// <param name="filters"></param>
//         /// <param name="period"></param>
//         /// <returns></returns>
//         public IHelperResult GetIndicatorsTableGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters, UserInfo userInfo) {
//             try {
//                 DataGridResponseModel<IndicatorDataTable> model = new DataGridResponseModel<IndicatorDataTable>();
//                 IEnumerable<IndicatorDataTable> query = null;
//                 query = Context.Indicators.Where(i => !i.IsDeleted).Select(ModelIndicatorToDataTable(userInfo.UserId));

//                 FilterIndicatorsQuery(ref query, filters);
//                 var orderedQuery = query.OrderBy(x => 0);
//                 OrderIndicatorsQuery(ref orderedQuery, sorts);
//                 model.totalCount = orderedQuery.Count();

//                 model.data = orderedQuery.Skip(skip).Take(take).Select((x, i) => { x.Num = i + 1; return x; });

//                 return model;
//             } catch (Exception exp) {
//                 return new BaseResponseEnumerable<IndicatorDataTable>(exp);
//             }
//         }
//         public Func<Indicator, int, IndicatorDataTable> ModelIndicatorToDataTable(Guid? userId) {
//             return (x, index) => new IndicatorDataTable {
//                 Id = x.Id,
//                 Name = x.Name,
//                 Num = index + 1,
//                 OrderNum = x.Num,
//                 UserDeadlineForSubmitting = UserDeadlineDate(x, userId, out bool isUserDeadline),
//                 IsUserDeadline = isUserDeadline,
//             };
//         }
//         private string UserDeadlineDate(Indicator model, Guid? userId, out bool isUserDeadline) {
//             var userDate = Context.IndicatorUsers.AsNoTracking().FirstOrDefault(i => i.UserId == userId && i.IndicatorId == model.Id)?.UserDeadlineForSubmitting;
//             isUserDeadline = userDate.HasValue && userDate?.Date.ToUniversalTime() != model.DeadlineForSubmitting?.Date.ToUniversalTime();
//             if (isUserDeadline)
//                 return indicatorRepository.GetDeadlineForSubmittingString(model.ReportingPeriodType, userDate);
//             else {
//                 var userFilialId = Context.Users.FirstOrDefault(x => x.Id == userId && !x.IsDeleted).FilialId;
//                 var filialInd = Context.FilialIndicatorUsers.Include(x => x.FilialIndicator).FirstOrDefault(x => x.UserId == userId && (x.FilialIndicator != null ? x.FilialIndicator.IndicatorId == model.Id : false) && (x.FilialIndicator != null ? x.FilialIndicator.FilialId == userFilialId : false))?.FilialIndicator;
//                 //var filialInd = Context.FilialIndicators.FirstOrDefault(x => x.UserId == userId && x..IndicatorId == model.Id && x.FilialId == userFilialId);

//                 return indicatorRepository.GetDeadlineForSubmittingString(model.ReportingPeriodType, filialInd?.DeadlineForSubmitting ?? model.DeadlineForSubmitting);
//             }
//         }
//         private static void OrderIndicatorsQuery(ref IOrderedEnumerable<IndicatorDataTable> query, DataGridSort[] sorts) {
//             if (sorts != null && sorts.Any()) {
//                 foreach (var sort in sorts) {
//                     switch (sort.ColumnName) {
//                         case "Name": {
//                                 query = sort.Type == DataGridSortType.Descending ? query.ThenByDescending(x => x.OrderNum) : query.ThenBy(x => x.OrderNum);
//                                 break;
//                             }
//                         case "UserDeadlineForSubmitting": {
//                                 query = sort.Type == DataGridSortType.Descending ? query.ThenByDescending(x => x.UserDeadlineForSubmitting) : query.ThenBy(x => x.UserDeadlineForSubmitting);
//                                 break;
//                             }
//                         default:
//                             query = query.ThenBy(x => x.Name);
//                             break;
//                     }
//                 }
//             } else {
//                 query = query.ThenBy(x => x.Name);
//             }
//         }
//         /// <summary>
//         /// Фильтрация запроса для отображений в таблице
//         /// </summary>
//         /// <param name="query"></param>
//         /// <param name="filters"></param>
//         private static void FilterIndicatorsQuery(ref IEnumerable<IndicatorDataTable> query, DataGridFilter[] filters) {
//             if (filters != null && filters.Any()) {
//                 foreach (var filter in filters) {
//                     filter.Value = filter.Value.Trim().ToLower();
//                     switch (filter.ColumnName) {
//                         case "Name": {
//                                 switch (filter.Type) {
//                                     case DataGridFilterType.Contains: {
//                                             query = query.Where(x => x.Name.ToLower().Contains(filter.Value));
//                                             break;
//                                         }

//                                     case DataGridFilterType.NotContains: {
//                                             query = query.Where(x => !x.Name.ToLower().Contains(filter.Value));
//                                             break;
//                                         }
//                                     case DataGridFilterType.StartsWith: {
//                                             query = query.Where(x => x.Name.ToLower().StartsWith(filter.Value));
//                                             break;
//                                         }
//                                     case DataGridFilterType.EndsWith: {
//                                             query = query.Where(x => x.Name.ToLower().EndsWith(filter.Value));
//                                             break;
//                                         }
//                                     case DataGridFilterType.Equals: {
//                                             query = query.Where(x => x.Name.ToLower() == filter.Value);
//                                             break;
//                                         }
//                                     case DataGridFilterType.NotEquals: {
//                                             query = query.Where(x => x.Name.ToLower() != filter.Value);
//                                             break;
//                                         }
//                                 }
//                                 break;
//                             }
//                         case "UserDeadlineForSubmitting": {
//                                 switch (filter.Type) {
//                                     case DataGridFilterType.Contains: {
//                                             query = query.Where(x => x.UserDeadlineForSubmitting.ToLower().Contains(filter.Value));
//                                             break;
//                                         }

//                                     case DataGridFilterType.NotContains: {
//                                             query = query.Where(x => !x.DeadlineForSubmitting.ToLower().Contains(filter.Value));
//                                             break;
//                                         }
//                                     case DataGridFilterType.StartsWith: {
//                                             query = query.Where(x => x.DeadlineForSubmitting.ToLower().StartsWith(filter.Value));
//                                             break;
//                                         }
//                                     case DataGridFilterType.EndsWith: {
//                                             query = query.Where(x => x.DeadlineForSubmitting.ToLower().EndsWith(filter.Value));
//                                             break;
//                                         }
//                                     case DataGridFilterType.Equals: {
//                                             query = query.Where(x => x.DeadlineForSubmitting.ToLower() == filter.Value);
//                                             break;
//                                         }
//                                     case DataGridFilterType.NotEquals: {
//                                             query = query.Where(x => x.DeadlineForSubmitting.ToLower() != filter.Value);
//                                             break;
//                                         }
//                                 }
//                                 break;
//                             }
//                     }
//                 }
//             }
//         }
//         /// <summary>
//         /// Редактирование/добавление сущности пользователь-показатель с пользовательской датой подачи сведений
//         /// </summary>
//         /// <param name="id">Ключ показателя</param>
//         /// <param name="date">Пользовательская дата</param>
//         /// <param name="userId">Ключ пользователя</param>
//         /// <returns></returns>
//         public BaseResponse SetUserDeadlineForSubmitting(Guid id, DateTime? date, int type, int month, int day, Guid? userId) {
//             try {
//                 var indEnt = Context.Indicators.AsNoTracking().FirstOrDefault(x => x.Id == id && !x.IsDeleted);
//                 if (indEnt.ReportingPeriodType != ReportingPeriodType.Fact)
//                     date = GetDeadlineDate(indEnt.ReportingPeriodType, type, month, day);

//                 if (indEnt == null)
//                     return new BaseResponse("Ошибка! Не удалось определить показатель");
//                 var indUserEnt = Context.IndicatorUsers.FirstOrDefault(ind => ind.IndicatorId == indEnt.Id && ind.UserId == userId);
//                 if (indUserEnt == null) {
//                     indUserEnt = new IndicatorUser {
//                         Id = Guid.NewGuid(),
//                         IndicatorId = indEnt.Id,
//                         UserId = userId.Value,
//                     };
//                     Context.IndicatorUsers.Add(indUserEnt);
//                 }
//                 indUserEnt.DateChange = DateTime.UtcNow;
//                 indUserEnt.UserDeadlineForSubmitting = date?.ToUniversalTime();
//                 Context.SaveChanges();
//                 return new BaseResponse(new BaseResponseModel());
//             } catch (Exception ex) {
//                 return new BaseResponse(ex);
//             }
//         }
//         /// <summary>
//         /// Редактирование/добавление сущности пользователь-показатель с пользовательской датой подачи сведений
//         /// </summary>
//         /// <param name="id">Ключ показателя</param>
//         /// <param name="date">Пользовательская дата</param>
//         /// <param name="userId">Ключ пользователя</param>
//         /// <returns></returns>
//         public BaseResponse RemoveTableStorageSettings(Guid? userId) {
//             try {
//                 if (!userId.HasValue)
//                     return new BaseResponse("Ошибка! Пользователь не найден");

//                 var toRemove = Context.UserStoredDatas.Where(x => x.UserId == userId);

//                 Context.UserStoredDatas.RemoveRange(toRemove);
//                 Context.SaveChanges();
//                 return new BaseResponse(new BaseResponseModel());
//             } catch (Exception ex) {
//                 return new BaseResponse(ex);
//             }
//         }
//     }
// }