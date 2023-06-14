// using DbLiftInteraction.Enums;
// using SysRM.Binders;
// using SysRM.Data;
// using SysRM.Data.Helpers;
// using SysRM.Data.Repositories;
// using SysRM.Data.Repositories.Administration;
// using SysRM.Entities.Enums;
// using SysRM.Entities.Models.Build;
// using SysRM.Entities.Models.Settings;
// using SysRM.Entities.Views.Response;
// using SysRM.Entities.Views.ViewModels;
// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using System.Linq;
// using System.Text;
// using System.Web.Caching;
// using System.Web.Mvc;
// using TMK.Utils.Enums;
// using TMK.Utils.Helpers;
// using TMK.Utils.Models.Result;

// namespace SysRM.Controllers {
//     /// <summary>
//     /// Базовый контроллер
//     /// </summary>
//     public class BaseController : Controller {
//         /// <summary>
//         /// Репозиторий ролей
//         /// </summary>
//         private readonly RoleRepository _roleRepos = new RoleRepository();
//         /// <summary>
//         /// Репозиторий прав
//         /// </summary>
//         private readonly RightRepository _rightRepository = new RightRepository();
//         /// <summary>
//         /// Репозиторий прав в меню
//         /// </summary>
//         private readonly BuildAccessesRepository _buildAccessesRepository = new BuildAccessesRepository();
//         /// <summary>
//         /// Репозиторий системных настроек
//         /// </summary>
//         private readonly SystemSettingsRepository _systemRepo = new SystemSettingsRepository();
//         /// <summary>
//         /// Хелпер для работы с репозиториями
//         /// </summary>
//         private readonly RepositoryHelper _repoHelper = new RepositoryHelper();
//         /// <summary>
//         /// Репозиторий сведений
//         /// </summary>
//         private readonly ReadinessInformationRepository _readinessRepo = new ReadinessInformationRepository();
//         private readonly UserStoredDataRepository _userStorDataRepo = new UserStoredDataRepository();

//         /// <summary>
//         /// Путь к вьюхе
//         /// </summary>
//         protected virtual string _pathToView {
//             get {
//                 return null;
//             }
//         }
//         /// <summary>
//         /// Путь к вьюхе с серверной ошибкой
//         /// </summary>
//         protected virtual string _pathToViewServerError {
//             get {
//                 return "~/Views/Errors/ServerError.cshtml";
//             }
//         }
//         /// <summary>
//         /// Метод преобразования строки в формат dateTume с миллисекундами для темпаралльных данных
//         /// </summary>
//         /// <param name="DateChange"></param>
//         /// <returns></returns>
//         protected DateTime ConvertStringFormatDataChangeHaveMilisecond(string DateChange) {
//             try {
//                 DateTime date;
//                 DateTime.TryParseExact(DateChange, "dd.MM.yyyy HH:mm:ss:fffffff", CultureInfo.InstalledUICulture, DateTimeStyles.None, out date);
//                 return date;

//             } catch (Exception) {
//                 return new DateTime();
//             }
//         }
//         /// <summary>
//         /// Метод преобразования строки поиска в тип DeleteSearchType для правильного отображения и выборки теимаральных данных
//         /// </summary>
//         /// <param name="searchText">Текст поиска</param>
//         /// <param name="column">Тип поиска должен быть равен строковому занчению DeletedType</param>
//         /// <returns></returns>
//         protected DeletedSearchType ConvertStringInDeletedSearchType(string searchText, string column) {
//             DeletedSearchType type = DeletedSearchType.Actual;
//             if (column == "DeletedType" && !string.IsNullOrEmpty(searchText)) {
//                 int temp = 0;
//                 if (int.TryParse(searchText, out temp)) {
//                     type = (DeletedSearchType)temp;
//                 }
//             }
//             return type;
//         }
//         /// <summary>
//         /// Текущий пользователь
//         /// </summary>
//         protected UserBaseModel CurrentUser {
//             get {
//                 var resp = RepositoryHelper.GetUserFromCache(HttpContext.User.Identity.Name);
//                 if (resp.State != ResultState.Success) {
//                     return null;
//                 }
//                 return resp.Result;
//             }
//         }
//         public JsonResult GetUserStoredData(Guid tableId) {
//             var res = _userStorDataRepo.GetUserStoredData(tableId, CurrentUser.UserInfo);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult SetUserStoredData(Guid? tableId, string tableData) {
//             var res = _userStorDataRepo.UpdateUserStoredData(tableId, tableData, CurrentUser.UserInfo);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetExecutionResultTypes() {
//             var exts = EnumHelper.GetListParam<ExecutionResultType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetDeviceTypes() {
//             var exts = EnumHelper.GetListParam<DevicesType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetYesNoEnum() {
//             var exts = EnumHelper.GetListParam<YesNoEnum>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetBackgroundLoadingTaskTypeEnum() {
//             var exts = EnumHelper.GetListParam<BackgroundLoadingTaskType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetBackgroundLoadingPeriodTypeEnum() {
//             var exts = EnumHelper.GetListParam<BackgroundLoadingPeriodType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetBackLoadPeriodMonQuatYearTypeEnum() {
//             var exts = EnumHelper.GetListParam<BackLoadPeriodMonQuatYearType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetYesNoUnapplicableEnum() {
//             var exts = EnumHelper.GetListParam<YesNoUnapplicableEnum>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetCommandEnum() {
//             var exts = EnumHelper.GetListParam<Command>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetStateEnum() {
//             var exts = EnumHelper.GetListParam<State>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetDbLiftObjectTypeEnum() {
//             var exts = EnumHelper.GetListParam<DbLiftObjectType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetSystemSettingTypeEnum() {
//             var exts = EnumHelper.GetListParam<SystemSettingType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(exts);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить список значений перчисления
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetPersonal() {
//             var list = EnumHelper.GetListParam<PersonalCategory>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить список значений перчисления
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetGender() {
//             var list = EnumHelper.GetListParam<GenderType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить список состояний оборудования
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetDevStateType() {
//             var list = EnumHelper.GetListParam<DevStateType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetTypeLoadRequestEnum() {
//             var list = EnumHelper.GetListParam<TypeLoadRequest>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить список состояний ремонта
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetRepairStateType() {
//             var list = EnumHelper.GetListParam<RepairStateType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить типы периода
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetReportingPeriodType() {
//             var list = EnumHelper.GetListParam<ReportingPeriodType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить типы показателей для экспорта сведений (все)
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetIndicatorsExtendedType() {
//             var list = EnumHelper.GetListParam<IndicatorsExtendedType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить типы показателей для экспорта сведений
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetIndicatorsType() {
//             var list = EnumHelper.GetListParam<IndicatorType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить границы для типов периода
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetReportingPeriodBorders() {
//             var list = EnumHelper.GetListParam<ReportingPeriodBorders>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить типа графика планового ремонта
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetScheduleType() {
//             var list = EnumHelper.GetListParam<ScheduleType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить тип от начала какого периода отсчитывать день для автоматического формирования сведений
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetTechProcessPeriodModeType() {
//             var list = EnumHelper.GetListParam<TechProcessPeriodMode>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить периоды графика ремонта
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetSchedulePeriod() {
//             var list = EnumHelper.GetListParam<SchedulePeriodType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить состояния графика ремонта
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetScheduleState() {
//             var list = EnumHelper.GetListParam<ScheduleStateType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить рабочие состояния графика ремонта
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetScheduleWorkStateType() {
//             var list = EnumHelper.GetListParam<ScheduleWorkStateType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить назначения графика ремонта
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetScheduleTransferType() {
//             var list = EnumHelper.GetListParam<ScheduleTransferType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить список значений перчисления
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetFilialPowerObjectType() {
//             var list = EnumHelper.GetListParam<FilialPowerObjectType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить список значений перчисления
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetFileFormatSimpleEnum() {
//             var list = EnumHelper.GetListParam<FileFormatSimpleEnum>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить список значений перчисления
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetFamilyStatusType() {
//             var list = EnumHelper.GetListParam<FamilyStatusType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить типы для поиска по отчетному периоду
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetDeletedSearchType() {
//             var list = EnumHelper.GetListParam<DeletedSearchType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить типы для поиска по событиям системы
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetSystemEventType() {
//             var list = EnumHelper.GetListParam<SystemEventType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить состояние диспетчерской заявки
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetClaimRequestStatus() {
//             var list = EnumHelper.GetListParam<ClaimRequestStatus>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить типы продления заявки
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetZvkType() {
//             var list = EnumHelper.GetListParam<ZvkType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetDeviceManagementObjectKindEnum() {
//             var list = EnumHelper.GetListParam<DeviceManagementObjectKind>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetDeviceManagementManageType() {
//             var list = EnumHelper.GetListParam<DeviceManagementManageType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetLoadsTableType() {
//             var list = EnumHelper.GetListParam<LoadsTableType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить типы для поиска по отчетному периоду
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetTechProcessStates() {
//             var list = EnumHelper.GetListParam<TechProcessState>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить результаты выполненого анализа
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetResultAnalysisType() {
//             var list = EnumHelper.GetListParam<ResultAnalysisType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить состояния мероприятия
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetMapEventStatusEnum() {
//             var list = EnumHelper.GetListParam<MapEventStatusEnum>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получить месяцы
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetMonths() {
//             var list = EnumHelper.GetListParam<Months>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Типы объектов ПО БД Подъем
//         /// </summary>
//         public JsonResult GetTypeObjectPodyem() {
//             var list = EnumHelper.GetListParam<DbLiftObjectType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetFilterOperationType() {
//             var list = EnumHelper.GetListParam<FilterOperationType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetServiceDeskServicesTypesEnum() {
//             var list = EnumHelper.GetListParam<ServiceTypesEnum>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetServiceDeskAppealTypesEnum() {
//             var list = EnumHelper.GetListParam<AppealTypesEnum>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetServiceDeskPriorityTypesEnum() {
//             var list = EnumHelper.GetListParam<PriorityTypesEnum>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetServiceDeskUrgencyTypesEnum() {
//             var list = EnumHelper.GetListParam<UrgencyTypesEnum>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetServiceDeskFgpsTypesEnum() {
//             var list = EnumHelper.GetListParam<FgpsTypesEnum>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetTechProcessAlertTypeEnum() {
//             var list = EnumHelper.GetListParam<TechProcessAlertType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetUserStateTypeEnum() {
//             var list = EnumHelper.GetListParam<UserStateType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public JsonResult GetOriginTypeEnum() {
//             var list = EnumHelper.GetListParam<OriginType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Метод получения сведений о заявках
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetApplicationDetailsType() {
//             var list = EnumHelper.GetListParam<ApplicationDetailsType>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value));
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Метод получения сведений о заявках
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetApplicationDetailsTypeShort() {
//             var no = new ListItemResponseModel<int>(1, "Нет");
//             var yes = new ListItemResponseModel<int>(0, "Да");
//             var data = new List<ListItemResponseModel<int>>();
//             data.Add(no);
//             data.Add(yes);
//             return Json(new BaseResponseEnumerable(data), JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Метод получения списка выбора состоящего из 2 записей да и нет
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetTypeChoise() {
//             var yes = new ListItemResponseModel<int>(1, "Да");
//             var no = new ListItemResponseModel<int>(0, "Нет");
//             var data = new List<ListItemResponseModel<int>>();
//             data.Add(no);
//             data.Add(yes);
//             return Json(new BaseResponseEnumerable(data), JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Получение базовой модели пользователя
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetPageInfo(string namePage) {
//             var user = RepositoryHelper.GetUserFromCache(HttpContext.User.Identity.Name);

//             var maintenanceMode = _systemRepo.GetAll<SystemSetting>(true).Where(x => x.Param == Guids.SysSettingParam.GlobalMaintenanceMode).First()?.Value == "1";

//             if (user.State == ResultState.Success && (!maintenanceMode || user.Result.Roles.Contains(Guids.Role.AdministratorIA))) {
//                 var roles = _roleRepos.GetRolesByUser(user.Result.UserInfo.UserId ?? Guid.Empty);
//                 if (roles.State == ResultState.Success) {
//                     var rights = _rightRepository.LoadRightByRole(roles.Result.Select(x => x.Id).ToArray());
//                     var menu = _buildAccessesRepository.BuildArchitecture();
//                     user.Result.Roles = roles.Result.Select(s => s.Id).Distinct().ToList();

//                     if (rights.State == ResultState.Success) {
//                         var mMenu = _buildAccessesRepository.MergeMenuRights(menu.Result, rights.Result);
//                         if (mMenu.State == ResultState.Success) {
//                             var res = new List<BuildAccesses>();
//                             mMenu = _buildAccessesRepository.GetAllElems(mMenu.Result, res);
//                         }
//                         if (!string.IsNullOrWhiteSpace(namePage) && namePage != "/") {
//                             var IdPage = mMenu.Result.FirstOrDefault(w => w.Link == namePage)?.Id;
//                             if (IdPage == null) {
//                                 user.Result.Rights = mMenu.Result.ToList();
//                             } else {
//                                 var listPageElements = mMenu.Result.Where(w => w.TypeElement == TypeElement.Page).ToArray();
//                                 user.Result.Rights = listPageElements.Where(w => w.ParentId == IdPage).ToList();
//                             }
//                         } else {
//                             user.Result.Rights = mMenu.Result.ToList();
//                         }
//                     }
//                 }
//             }
//             return Json(user, JsonRequestBehavior.AllowGet);
//         }
//         /// <summary>
//         /// Отображение страницы
//         /// </summary>
//         /// <returns></returns>
//         public ActionResult Index() {
//             if (!string.IsNullOrEmpty(_pathToView) && (_pathToView.ToUpper().EndsWith("/HOME/INDEX.CSHTML") || 
//                 _pathToView.ToUpper().EndsWith("/READINESSINFORMATIONS.CSHTML") || _pathToView.ToUpper().EndsWith("/READINESSINFONAVIGATION.CSHTML"))) {

//                 bool.TryParse(HttpContext.Session["HelloImageShown"]?.ToString(), out bool helloImageShown);
//                 if (!helloImageShown) {
//                     helloImageShown = true;
//                     HttpContext.Session.Add("HelloImageShown", helloImageShown);
//                     return View("~/Views/Home/ImagePage.cshtml");
//                 }
//             }

//             string cookieVersion = _repoHelper.GetCookie("cookieVersion");
//             bool.TryParse(_repoHelper.GetCookie("isOldReadinessDefault"), out bool isOldReadinessDefault);
//             Guid.TryParse(cookieVersion, out Guid cookieVer);

//             var data = new HomeViewModel {
//                 SystemVersion = Guids.Menu.ActualSystemVersion,
//                 IsValidVersion = cookieVer == Guids.Menu.ActualSystemVersion || cookieVer == Guid.Empty,
//             };

//             var modes = _systemRepo.GetAll<SystemSetting>(true).Where(x => x.Param == Guids.SysSettingParam.GlobalMaintenanceMode);
//             if (modes.Count() == 0)
//                 data.IsMaintenanceMode = false;
//             else
//                 data.IsMaintenanceMode = modes.First()?.Value == "1";

//             var currentUser = CurrentUser;

//             if (data.IsValidVersion) {
//                 if (data.IsMaintenanceMode) {
//                     if (!currentUser.Roles.Contains(Guids.Role.AdministratorIA) && !string.IsNullOrEmpty(_pathToView) && !_pathToView.ToUpper().EndsWith("/HOME/INDEX.CSHTML")) {
//                         return RedirectToAction("Index", "Home");
//                     }
//                 } else if (!string.IsNullOrEmpty(_pathToView) && _pathToView.ToUpper().EndsWith("/HOME/INDEX.CSHTML"))
//                     return RedirectToAction("Index", "ReadinessInformations");
//             } else if (!string.IsNullOrEmpty(_pathToView) && !_pathToView.ToUpper().EndsWith("/HOME/INDEX.CSHTML")) {
//                 return RedirectToAction("Index", "Home");
//             }

//             var redirectToReadiness = HttpContext.Request.Cookies["ReadinessInformationsFromNav"]?.Value;
//             if (!isOldReadinessDefault && !string.IsNullOrEmpty(_pathToView) &&
//                 _pathToView.ToUpper().EndsWith("/READINESSINFORMATIONS.CSHTML") &&
//                 (string.IsNullOrWhiteSpace(redirectToReadiness) || redirectToReadiness == "false")) {
//                 return RedirectToAction("Index", "ReadinessInfoNavigation");
//             }


//             //if (!string.IsNullOrEmpty(_pathToView) && _pathToView.ToUpper().EndsWith("/READINESSINFORMATIONS.CSHTML")) {
//             //    if (!isOldReadinessDefault)
//             //        return RedirectToAction("Index", "ReadinessInfoNavigation");
//             //    else {
//             //        return RedirectToAction("Index", "ReadinessInformations");
//             //    }
//             //}
//             //ADSearcher aDSearcher = new ADSearcher();
//             //Exception exp = null;
//             //var aDUser32 = aDSearcher.FindByGroup(out exp , new List<string>() { "tmk\\администратор кав", "tmk\\ods" });
//             //var aDUser11 = aDSearcher.FindAll(out exp, ADSearchType.SamAccountName, "shayda");
//             // Пока уберем, иначе технолог не видит объявлений на главной странице
//             //if (currentUser != null) {
//             //	if ((currentUser?.Roles).Any(w => w == UserGuid.TechnologRoleId)) {
//             //		var s = Session[currentUser?.UserInfo.UserLogin.Trim().ToLower()];
//             //		if (s == null) {
//             //			Session[currentUser.UserInfo.UserLogin.Trim().ToLower()] = true;
//             //			return RedirectToAction("Index", "ReadinessInformations");
//             //		}
//             //		var ses = (bool)s;
//             //		if (!ses) {
//             //			Session[currentUser.UserInfo.UserLogin.Trim().ToLower()] = true;
//             //			return RedirectToAction("Index", "ReadinessInformations");
//             //		}
//             //	}
//             //}
//             // var aDUser = aDSearcher.FindOne(out exp,  "tmk\\shayda");
//             return View(_pathToView);
//         }
//         /// <summary>
//         /// Расширения для преобразования даты по определенному формату
//         /// </summary>
//         /// <param name="data">Данные</param>
//         /// <param name="contentType">Тип контента</param>
//         /// <param name="contentEncoding">Кодировка</param>
//         /// <returns></returns>
//         protected override JsonResult Json(object data, string contentType, Encoding contentEncoding) {
//             return JsonResultExtensions.JsonNet(data, contentType, contentEncoding);
//         }
//         /// <summary>
//         /// Расширения для преобразования даты по определенному формату
//         /// </summary>
//         /// <param name="data">Данные</param>
//         /// <param name="contentType">Тип контента</param>
//         /// <param name="contentEncoding">Кодировка</param>
//         /// <param name="behavior">Разрешение</param>
//         /// <returns></returns>
//         protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) {
//             return JsonResultExtensions.JsonNet(data, contentType, contentEncoding, behavior);
//         }
//         /// <summary>
//         /// Построение меню
//         /// </summary>
//         /// <returns></returns>
//         [HttpPost]
//         public JsonResult BuildMenu(Guid? UserId) {

//             var maintenanceMode = _systemRepo.GetAll<SystemSetting>(true).Where(x => x.Param == Guids.SysSettingParam.GlobalMaintenanceMode).First()?.Value == "1";
//             var user = RepositoryHelper.GetUserFromCache(HttpContext.User.Identity.Name);

//             if (!UserId.HasValue || maintenanceMode && !user.Result.Roles.Contains(Guids.Role.AdministratorIA)) {
//                 return Json(new BaseResponseEnumerable<BuildAccesses>(new List<BuildAccesses>()));
//             }
//             var data = _buildAccessesRepository.GetMenuItems();

//             var roles = _roleRepos.GetRolesByUser(UserId.Value);
//             if (roles.State != ResultState.Success) {
//                 return Json(roles);
//             }

//             var rights = _rightRepository.LoadRightByRole(roles.Result.Select(x => x.Id).ToArray());
//             if (rights.State != ResultState.Success) {
//                 return Json(rights);
//             }
//             var buildAccesses = new BuildAccesses();
//             data = _buildAccessesRepository.MergeMenuRights(data.Result, rights.Result, buildAccesses);

//             return Json(data);
//         }
//         /// <summary>
//         /// Получить список элементов да/нет
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetItemBoolList() {
//             return Json(new BaseResponseEnumerable(new[] {
//                 new ListItemResponseModel<int>(1, "Да"),
//                 new ListItemResponseModel<int>(0, "Нет"),
//             }), JsonRequestBehavior.AllowGet);
//         }

//         public JsonResult GetItemBoolListHaveAll() {
//             return Json(new BaseResponseEnumerable(new[] {
//                 new ListItemResponseModel<int>(1, "Да"),
//                 new ListItemResponseModel<int>(0, "Нет"),
//                 new ListItemResponseModel<int>(2, "Все"),
//             }), JsonRequestBehavior.AllowGet);
//         }

//         /// <summary>
//         /// Получить месяцы
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult GetTrafficAccidentReasonTypes(bool withNone = false) {
//             var list = EnumHelper.GetListParam<TrafficAccidentReasonTypes>().Select(x => new ListItemResponseModel<int>(x.Key, x.Value)).ToList();
//             if (!withNone && list.Count() != 0)
//                 list.RemoveAt(3);
//             BaseResponseEnumerable res = new BaseResponseEnumerable(list);
//             return Json(res, JsonRequestBehavior.AllowGet);
//         }
//         public bool UserIsAllowedToEdit(Guid readinessInformationId, UserBaseModel userBase) {
//             return _readinessRepo.UserIsAllowedToEdit(readinessInformationId, userBase);
//         }
//         /// <summary>
//         /// Является ли пользователь администратором (ИА / технологическим / филиала)
//         /// </summary>
//         /// <returns></returns>
//         public JsonResult IsUserAdmin() {
//             var isAdmin = CurrentUser.Roles.Any(r => r == Guids.Role.AdministratorIA || r == Guids.Role.AdministratorFilial || r == Guids.Role.TechnologyAdministratorFilial);
//             return Json(new BaseResponse(new Entities.Views.ViewModels.Base.BaseResponseModel(), new { UserIsAdmin = isAdmin }), JsonRequestBehavior.AllowGet);
//         }
//     }
// }