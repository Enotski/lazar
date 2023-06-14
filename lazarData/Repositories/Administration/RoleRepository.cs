// using SysRM.Data.Context;
// using SysRM.Data.Repositories.Administration;
// using SysRM.Entities.Enums;
// using SysRM.Entities.Models.References.Administration;
// using SysRM.Entities.Views.Response;
// using SysRM.Entities.Views.ViewModels;
// using SysRM.Entities.Views.ViewModels.Base;
// using SysRM.Entities.Views.ViewModels.DataGrid;
// using System;
// using System.Collections.Generic;
// using System.Data.Entity;
// using System.Linq;
// using TMK.Utils.AD;
// using TMK.Utils.Enums;
// using TMK.Utils.Extensions;
// using TMK.Utils.Interfaces.Base;
// using TMK.Utils.Models.Result;

// namespace SysRM.Data.Repositories {
// 	public class RoleRepository : BaseRepository<RoleViewModel, Role> {
// 		/// <summary>
// 		/// Репозиторий логирования
// 		/// </summary>
// 		EventLogRepository logRepo = new EventLogRepository();
// 		/// <summary>
// 		/// Репозиторий построения меню
// 		/// </summary>
// 		private readonly BuildAccessesRepository _buildRepository = new BuildAccessesRepository();
// 		/// <summary>
// 		/// Репозиторий построения меню
// 		/// </summary>
// 		private readonly RightRepository _rightRepository = new RightRepository();
// 		/// <summary>
// 		/// Конструктор
// 		/// </summary>
// 		public RoleRepository() : base() { }
// 		/// <summary>
// 		/// Конструктор
// 		/// </summary>
// 		/// <param name="context">Контекст</param>
// 		public RoleRepository(RmContext context) : base(context) { }

// 		public static void FilterData(ref IQueryable<Role> source, DataGridFilter[] filters) {
// 			if (filters == null || !filters.Any()) return;
// 			foreach (var filter in filters) {
// 				filter.Value = filter.Value.Trim().ToLower();
// 				switch (filter.ColumnName) {
// 					case "Name": {
// 							switch (filter.Type) {
// 								case DataGridFilterType.Contains: {
// 										source = source.Where(x => x.Name.ToLower().Contains(filter.Value));
// 										break;
// 									}

// 								case DataGridFilterType.NotContains: {
// 										source = source.Where(x => !x.Name.ToLower().Contains(filter.Value));
// 										break;
// 									}

// 								case DataGridFilterType.StartsWith: {
// 										source = source.Where(x => x.Name.ToLower().StartsWith(filter.Value));
// 										break;
// 									}

// 								case DataGridFilterType.EndsWith: {
// 										source = source.Where(x => x.Name.ToLower().EndsWith(filter.Value));
// 										break;
// 									}

// 								case DataGridFilterType.Equals: {
// 										source = source.Where(x => x.Name.ToLower() == filter.Value);
// 										break;
// 									}
// 								case DataGridFilterType.NotEquals: {
// 										source = source.Where(x => x.Name.ToLower() != filter.Value);
// 										break;
// 									}
// 							}
// 							break;
// 						}
// 					case "GroupAD": {
// 							switch (filter.Type) {
// 								case DataGridFilterType.Contains: {
// 										source = source.Where(x => x.GroupAd.ToLower().Contains(filter.Value));
// 										break;
// 									}

// 								case DataGridFilterType.NotContains: {
// 										source = source.Where(x => !x.GroupAd.ToLower().Contains(filter.Value));
// 										break;
// 									}

// 								case DataGridFilterType.StartsWith: {
// 										source = source.Where(x => x.GroupAd.ToLower().StartsWith(filter.Value));
// 										break;
// 									}

// 								case DataGridFilterType.EndsWith: {
// 										source = source.Where(x => x.GroupAd.ToLower().EndsWith(filter.Value));
// 										break;
// 									}

// 								case DataGridFilterType.Equals: {
// 										source = source.Where(x => x.GroupAd.ToLower() == filter.Value);
// 										break;
// 									}
// 								case DataGridFilterType.NotEquals: {
// 										source = source.Where(x => x.GroupAd.ToLower() != filter.Value);
// 										break;
// 									}
// 							}
// 							break;
// 						}
// 					case "DateLastEdit": {
// 							var dates = filter.Value.Split(';').Select(DateTime.Parse).ToArray();
// 							DateTime date1 = dates[0];
// 							switch (filter.Type) {
// 								case DataGridFilterType.Less: {
// 										source = source.AsEnumerable().Where(x => x.DateChange.Date < date1.Date).AsQueryable();
// 										break;
// 									}

// 								case DataGridFilterType.Greater: {
// 										source = source.AsEnumerable().Where(x => x.DateChange.Date > date1.Date).AsQueryable();
// 										break;
// 									}

// 								case DataGridFilterType.LessOrEqual: {
// 										source = source.AsEnumerable().Where(x => x.DateChange.Date <= date1.Date).AsQueryable();
// 										break;
// 									}

// 								case DataGridFilterType.GreaterOrEqual: {
// 										source = source.AsEnumerable().Where(x => x.DateChange.Date >= date1.Date).AsQueryable();
// 										break;
// 									}

// 								case DataGridFilterType.Equals: {
// 										source = source.AsEnumerable().Where(x => x.DateChange.Date == date1.Date).AsQueryable();
// 										break;
// 									}
// 								case DataGridFilterType.NotEquals: {
// 										source = source.AsEnumerable().Where(x => x.DateChange.Date != date1.Date).AsQueryable();
// 										break;
// 									}
// 								case DataGridFilterType.Between: {
// 										DateTime date2 = dates[1];
// 										source = source.AsEnumerable().Where(x => date1.Date <= x.DateChange.Date && x.DateChange.Date <= date2.Date).AsQueryable();
// 										break;
// 									}
// 							}
// 							break;
// 						}
// 					case "ChangedBy": {
// 							switch (filter.Type) {
// 								case DataGridFilterType.Contains: {
// 										source = source.AsEnumerable().Where(x => x.ChangedByUser.GetFullNameWithLogin().ToLower().Contains(filter.Value)).AsQueryable();
// 										break;
// 									}
// 								case DataGridFilterType.NotContains: {
// 										source = source.AsEnumerable().Where(x => !x.ChangedByUser.GetFullNameWithLogin().ToLower().Contains(filter.Value)).AsQueryable();
// 										break;
// 									}
// 								case DataGridFilterType.StartsWith: {
// 										source = source.AsEnumerable().Where(x => x.ChangedByUser.GetFullNameWithLogin().ToLower().StartsWith(filter.Value)).AsQueryable();
// 										break;
// 									}
// 								case DataGridFilterType.EndsWith: {
// 										source = source.AsEnumerable().Where(x => x.ChangedByUser.GetFullNameWithLogin().ToLower().EndsWith(filter.Value)).AsQueryable();
// 										break;
// 									}
// 								case DataGridFilterType.Equals: {
// 										source = source.AsEnumerable().Where(x => x.ChangedByUser.GetFullNameWithLogin().ToLower() == filter.Value).AsQueryable();
// 										break;
// 									}
// 								case DataGridFilterType.NotEquals: {
// 										source = source.AsEnumerable().Where(x => x.ChangedByUser.GetFullNameWithLogin().ToLower() != filter.Value).AsQueryable();
// 										break;
// 									}
// 							}
// 							break;
// 						}
// 				}
// 			}
// 		}

// 		public static void SortData(ref IOrderedQueryable<Role> source, DataGridSort[] sorts) {
// 			if (sorts == null || !sorts.Any()) return;
// 			foreach (var sort in sorts) {
// 				switch (sort.ColumnName) {
// 					case "Name": {
// 							source = sort.Type == DataGridSortType.Descending
// 								? source.ThenByDescending(x => x.Name)
// 								: source.ThenBy(x => x.Name);
// 							break;
// 						}
// 					case "GroupAD": {
// 							source = sort.Type == DataGridSortType.Descending
// 								? source.ThenByDescending(x => x.GroupAd)
// 								: source.ThenBy(x => x.GroupAd);
// 							break;
// 						}
// 					case "DateLastEdit": {
// 							source = sort.Type == DataGridSortType.Descending
// 								? source.ThenByDescending(x => x.DateChange)
// 								: source.ThenBy(x => x.DateChange);
// 							break;
// 						}
// 					case "ChangedBy": {
// 							source = sort.Type == DataGridSortType.Descending
// 								? source.ThenByDescending(x => x.ChangedByUser.Surname)
// 								: source.ThenBy(x => x.ChangedByUser.Surname);
// 							break;
// 						}
// 				}
// 			}
// 		}

// 		public IHelperResult GetRolesDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters) {
// 			try {
// 				DataGridResponseModel<RoleDataGrid> model = new DataGridResponseModel<RoleDataGrid>();
// 				var query = GetAll<Role>(true, x => x.ChangedByUser);
// 				FilterData(ref query, filters);
// 				var orderedQuery = query.OrderBy(x => 0);
// 				SortData(ref orderedQuery, sorts);
// 				model.totalCount = orderedQuery.Count();
// 				model.data = orderedQuery.Skip(skip).Take(take).AsEnumerable()
// 					.Select(ModelToDataGridViewModel())
// 					.ToArray();
// 				return model;
// 			} catch (Exception exp) {
// 				return DataGridResponseModel<RoleDataGrid>.ErrorResponse(exp);
// 			}
// 		}

// 		public Func<Role, int, RoleDataGrid> ModelToDataGridViewModel() {
// 			return (x, index) => new RoleDataGrid {
// 				Id = x.Id,
// 				Name = x.Name,
// 				GroupAD = x.GroupAd ?? "Группа не указана",
// 				IsDefault = x.IsDefault,
// 				ChangedBy = x.ChangedByUser?.GetFullNameWithLogin(),
// 				DateLastEdit = x.DateChange.ToLocalTime().ToDataGridFormat(),
// 				Num = index + 1
// 			};
// 		}
// 		public IHelperResult GetAdGroupDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters, Guid? IdRole) {
// 			try {
// 				var groupAdString = GetAll<Role>(true).FirstOrDefault(f => f.Id == IdRole)?.GroupAd;
// 				var list = !string.IsNullOrEmpty(groupAdString) ? groupAdString.Split(';').ToArray() : new string[0];
// 				DataGridResponseModel<AdGroupDataGrid> model = new DataGridResponseModel<AdGroupDataGrid>();
// 				if (filters != null) {
// 					foreach (var filter in filters) {
// 						switch (filter.ColumnName) {
// 							case "Name": {
// 									switch (filter.Type) {
// 										case DataGridFilterType.Contains: {
// 												list = list.Where(x => x.ToLower().Contains(filter.Value)).ToArray();
// 												break;
// 											}

// 										case DataGridFilterType.NotContains: {
// 												list = list.Where(x => !x.ToLower().Contains(filter.Value)).ToArray();
// 												break;
// 											}

// 										case DataGridFilterType.StartsWith: {
// 												list = list.Where(x => x.ToLower().StartsWith(filter.Value)).ToArray();
// 												break;
// 											}

// 										case DataGridFilterType.EndsWith: {
// 												list = list.Where(x => x.ToLower().EndsWith(filter.Value)).ToArray();
// 												break;
// 											}

// 										case DataGridFilterType.Equals: {
// 												list = list.Where(x => x.ToLower() == filter.Value).ToArray();
// 												break;
// 											}
// 										case DataGridFilterType.NotEquals: {
// 												list = list.Where(x => x.ToLower() != filter.Value).ToArray();
// 												break;
// 											}
// 									}
// 									break;
// 								}
// 						}
// 					}
// 				}

// 				var orderedQuery = list.OrderBy(x => 0);
// 				if (sorts != null) {
// 					foreach (var sort in sorts) {
// 						switch (sort.ColumnName) {
// 							case "Name": {
// 									orderedQuery = sort.Type == DataGridSortType.Descending
// 										? orderedQuery.ThenByDescending(x => x)
// 										: orderedQuery.ThenBy(x => x);
// 									break;
// 								}
// 						}
// 					}
// 				}

// 				model.totalCount = orderedQuery.Count();
// 				model.data = orderedQuery.Skip(skip).Take(take)
// 					.Select(ModelAdGroupToDataGridViewModel())
// 					.ToArray();
// 				return model;
// 			} catch (Exception exp) {
// 				return DataGridResponseModel<AdGroupDataGrid>.ErrorResponse(exp);
// 			}
// 		}
// 		public Func<string, int, AdGroupDataGrid> ModelAdGroupToDataGridViewModel() {
// 			return (x, index) => new AdGroupDataGrid {
// 				Id = Guid.Empty,
// 				Name = x,
// 				Num = index + 1
// 			};
// 		}
// 		public Func<ADUser, int, UsersGroupDataGrid> ModelUsersGroupToDataGridViewModel() {
// 			return (x, index) => new UsersGroupDataGrid {
// 				Id = Guid.NewGuid(),
// 				FIO = x.Surname + " " + x.GivenName,
// 				Login = x.WinLogin,
// 				Num = index + 1
// 			};
// 		}
// 		public IHelperResult GetUsersGroupDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters, List<string> Groups) {
// 			try {
// 				var list = new ADSearcher().FindByGroup(out Exception exc, Groups);
// 				DataGridResponseModel<UsersGroupDataGrid> model = new DataGridResponseModel<UsersGroupDataGrid>();
// 				if (filters != null) {
// 					foreach (var filter in filters) {
// 						switch (filter.ColumnName) {
// 							case "FIO": {
// 									switch (filter.Type) {
// 										case DataGridFilterType.Contains: {
// 												list = list.Where(x => (x.Surname + " " + x.GivenName).ToLower().Contains(filter.Value)).ToList();
// 												break;
// 											}

// 										case DataGridFilterType.NotContains: {
// 												list = list.Where(x => !(x.Surname + " " + x.GivenName).ToLower().Contains(filter.Value)).ToList();
// 												break;
// 											}

// 										case DataGridFilterType.StartsWith: {
// 												list = list.Where(x => (x.Surname + " " + x.GivenName).ToLower().StartsWith(filter.Value)).ToList();
// 												break;
// 											}

// 										case DataGridFilterType.EndsWith: {
// 												list = list.Where(x => (x.Surname + " " + x.GivenName).ToLower().EndsWith(filter.Value)).ToList();
// 												break;
// 											}

// 										case DataGridFilterType.Equals: {
// 												list = list.Where(x => (x.Surname + " " + x.GivenName).ToLower() == filter.Value).ToList();
// 												break;
// 											}
// 										case DataGridFilterType.NotEquals: {
// 												list = list.Where(x => (x.Surname + " " + x.GivenName).ToLower() != filter.Value).ToList();
// 												break;
// 											}
// 									}
// 									break;
// 								}
// 							case "Login": {
// 									switch (filter.Type) {
// 										case DataGridFilterType.Contains: {
// 												list = list.Where(x => x.WinLogin.ToLower().Contains(filter.Value)).ToList();
// 												break;
// 											}

// 										case DataGridFilterType.NotContains: {
// 												list = list.Where(x => !x.WinLogin.ToLower().Contains(filter.Value)).ToList();
// 												break;
// 											}

// 										case DataGridFilterType.StartsWith: {
// 												list = list.Where(x => x.WinLogin.ToLower().StartsWith(filter.Value)).ToList();
// 												break;
// 											}

// 										case DataGridFilterType.EndsWith: {
// 												list = list.Where(x => x.WinLogin.ToLower().EndsWith(filter.Value)).ToList();
// 												break;
// 											}

// 										case DataGridFilterType.Equals: {
// 												list = list.Where(x => x.WinLogin.ToLower() == filter.Value).ToList();
// 												break;
// 											}
// 										case DataGridFilterType.NotEquals: {
// 												list = list.Where(x => x.WinLogin.ToLower() != filter.Value).ToList();
// 												break;
// 											}
// 									}
// 									break;
// 								}
// 						}
// 					}
// 				}
// 				var orderedQuery = list.OrderBy(x => 0);
// 				if (sorts != null) {
// 					foreach (var sort in sorts) {
// 						switch (sort.ColumnName) {
// 							case "FIO": {
// 									orderedQuery = sort.Type == DataGridSortType.Descending
// 										? orderedQuery.ThenByDescending(x => x.Surname)
// 										: orderedQuery.ThenBy(x => x.Surname);
// 									break;
// 								}
// 							case "Login": {
// 									orderedQuery = sort.Type == DataGridSortType.Descending
// 										? orderedQuery.ThenByDescending(x => x.WinLogin)
// 										: orderedQuery.ThenBy(x => x.WinLogin);
// 									break;
// 								}
// 						}
// 					}
// 				}
// 				model.totalCount = orderedQuery.Count();
// 				model.data = orderedQuery.Skip(skip).Take(take)
// 					.Select(ModelUsersGroupToDataGridViewModel())
// 					.ToArray();
// 				return model;
// 			} catch (Exception exp) {
// 				return DataGridResponseModel<AdGroupDataGrid>.ErrorResponse(exp);
// 			}
// 		}
// 		public IHelperResult GetAllGroupsAdDataGrid(int skip, int take, DataGridSort[] sorts, DataGridFilter[] filters) {
// 			try {
// 				var list = new ADSearcher().AllGroupsAD(out Exception error).ToArray();
// 				DataGridResponseModel<AdGroupDataGrid> model = new DataGridResponseModel<AdGroupDataGrid>();
// 				if (filters != null) {
// 					foreach (var filter in filters) {
// 						switch (filter.ColumnName) {
// 							case "Name": {
// 									switch (filter.Type) {
// 										case DataGridFilterType.Contains: {
// 												list = list.Where(x => x.ToLower().Contains(filter.Value)).ToArray();
// 												break;
// 											}

// 										case DataGridFilterType.NotContains: {
// 												list = list.Where(x => !x.ToLower().Contains(filter.Value)).ToArray();
// 												break;
// 											}

// 										case DataGridFilterType.StartsWith: {
// 												list = list.Where(x => x.ToLower().StartsWith(filter.Value)).ToArray();
// 												break;
// 											}

// 										case DataGridFilterType.EndsWith: {
// 												list = list.Where(x => x.ToLower().EndsWith(filter.Value)).ToArray();
// 												break;
// 											}

// 										case DataGridFilterType.Equals: {
// 												list = list.Where(x => x.ToLower() == filter.Value).ToArray();
// 												break;
// 											}
// 										case DataGridFilterType.NotEquals: {
// 												list = list.Where(x => x.ToLower() != filter.Value).ToArray();
// 												break;
// 											}
// 									}
// 									break;
// 								}
// 						}
// 					}
// 				}
// 				var orderedQuery = list.OrderBy(x => 0);
// 				if (sorts != null) {
// 					foreach (var sort in sorts) {
// 						switch (sort.ColumnName) {
// 							case "Name": {
// 									orderedQuery = sort.Type == DataGridSortType.Descending
// 										? orderedQuery.ThenByDescending(x => x)
// 										: orderedQuery.ThenBy(x => x);
// 									break;
// 								}
// 						}
// 					}
// 				}
// 				model.totalCount = orderedQuery.Count();
// 				model.data = orderedQuery.Skip(skip).Take(take).AsEnumerable()
// 					.Select(ModelAdGroupToDataGridViewModel())
// 					.ToArray();
// 				return model;
// 			} catch (Exception exp) {
// 				return DataGridResponseModel<AdGroupDataGrid>.ErrorResponse(exp);
// 			}
// 		}
// 		/// <summary>
// 		/// Получить роль с правами для редактирования
// 		/// </summary>
// 		/// <param name="roleId"></param>
// 		/// <returns></returns>
// 		public BaseResponse<RoleViewModel> GetRoleWithRights(Guid? roleId) {
// 			try {
// 				if (roleId.HasValue) {
// 					var role = GetViewById<Role>(roleId.Value);
// 					var res = _rightRepository.GetRightsForRole(role);
// 					var menu = _buildRepository.BuildArchitecture();
// 					if (menu.State != ResultState.Success) {
// 						return new BaseResponse<RoleViewModel>(menu.Error);
// 					}
// 					if (res.State == ResultState.Success) {
// 						return new BaseResponse<RoleViewModel>(res.Result);
// 					}

// 					return res;
// 				}

// 				return new BaseResponse<RoleViewModel>(new RoleViewModel());
// 			} catch (Exception ex) {
// 				return new BaseResponse<RoleViewModel>(ex);
// 			}
// 		}

// 		/// <summary>
// 		/// Преобразовывает сущности роли в модель представления роли
// 		/// </summary>
// 		/// <returns></returns>
// 		public override Func<Role, RoleViewModel> ModelToViewModel() {
// 			return model => new RoleViewModel {
// 				Id = model.Id,
// 				Name = model.Name,
// 				GroupAD = model.GroupAd ?? "",
// 				IsDefault = model.IsDefault,
// 			};
// 		}
// 		/// <summary>
// 		/// Общий метод редактирования и добавлние новой роли
// 		/// </summary>
// 		/// <param name="IdRole">Ид Роли выбранной</param>
// 		/// <param name="NameRole">Название роли</param>
// 		/// <param name="GroupAD">Группа Ад в которую должна входить данная роль</param>
// 		/// <returns></returns>
// 		public BaseResponse CommonMethodsChangeRoles(Guid? IdRole, string NameRole, string GroupAD, UserInfo user, Guid[] Rights) {
// 			Role oldData = new Role();
// 			Role newData = new Role();
// 			SystemEventType logType;
// 			try {
// 				Exception exc = new Exception();
// 				if (string.IsNullOrWhiteSpace(NameRole) /*|| string.IsNullOrWhiteSpace(GroupAD)*/) {
// 					throw new Exception("Заполните все поля");
// 				}
// 				var CurrentUserId = user?.UserId;
// 				if (IdRole == Guid.Empty || IdRole == null) {
// 					if (Context.Roles.FirstOrDefault(w => w.Name == NameRole || w.GroupAd == GroupAD) != null) {
// 						throw new Exception("Такая роль уже существует");
// 					}

// 					newData = new Role {
// 						Id = Guid.NewGuid(),
// 						Name = NameRole,
// 						GroupAd = string.IsNullOrEmpty(GroupAD) ? null : GroupAD,
// 						ChangedUserId = CurrentUserId,
// 						DateChange = DateTime.UtcNow
// 					};

// 					Context.Roles.Add(newData);
// 					logType = SystemEventType.Create;
// 				} else {
// 					newData = GetAll<Role>(false, x => x.Rights).FirstOrDefault(w => w.Id == IdRole);
// 					oldData = new Role {
// 						Name = newData.Name,
// 						GroupAd = newData.GroupAd, 
// 					};

// 					newData.GroupAd = string.IsNullOrEmpty(GroupAD) ? null : GroupAD;
// 					newData.Name = NameRole;
// 					newData.ChangedUserId = CurrentUserId;
// 					newData.DateChange = DateTime.UtcNow;

// 					logType = SystemEventType.Create;
// 				}

// 				Context.SaveChanges();
// 				_rightRepository.SaveRights(newData, Rights);

// 				if (logType == SystemEventType.Create) {
// 					logRepo.LogEvent(SubSystemType.Users, SystemEventType.Create, $"Добавление роли':\n до изменений - {oldData.LogString};\n после изменений - {newData.LogString}", "Успешно", user, out string logResult, user.FilialId, user.FilialDateChange);
// 				} else {
// 					logRepo.LogEvent(SubSystemType.Users, SystemEventType.Update, $"Редактирование роли:\n до изменений - {oldData.LogString};\n после изменений - {newData.LogString}", "Успешно", user, out string logResult, user.FilialId, user.FilialDateChange);
// 				}

// 				return new BaseResponse(new BaseResponseModel());
// 			} catch (Exception exc) {
// 				logRepo.LogEvent(SubSystemType.Users, SystemEventType.Error, $"Добавление/редактирование роли", "Ошибка добавления/редактирования", user, out string logResult, user.FilialId, user.FilialDateChange);
// 				return new BaseResponse(exc);
// 			}
// 		}

// 		public BaseResponse SaveADGroup(Guid IdRole, string GroupAD, UserInfo user) {
// 			try {
// 				Exception exc = new Exception();
// 				Role model = null;
// 				var CurrentUserId = user?.UserId;
// 				model = GetAll<Role>(false).FirstOrDefault(w => w.Id == IdRole);
// 				model.GroupAd = string.IsNullOrEmpty(GroupAD) ? null : GroupAD;
// 				model.ChangedUserId = CurrentUserId;
// 				model.DateChange = DateTime.UtcNow;
// 				Context.SaveChanges();

// 				logRepo.LogEvent(SubSystemType.Users, SystemEventType.Error, $"Добавление группы AD - '{GroupAD}'", $"Успешно", user, out string logResult, user.FilialId, user.FilialDateChange);
// 				return new BaseResponse(new BaseResponseModel());
// 			} catch (Exception exc) {
// 				logRepo.LogEvent(SubSystemType.Users, SystemEventType.Error, $"Добавление группы AD", "Ошибка добавления", user, out string logResult, user.FilialId, user.FilialDateChange);
// 				return new BaseResponse(exc);
// 			}
// 		}

// 		/// <summary>
// 		/// Удаление выбранных ролей
// 		/// </summary>
// 		/// <param name="ListIdRoles">Список выбранных Ид ролей для удаления</param>
// 		/// <returns></returns>
// 		public BaseResponse DeleteRoles(List<Guid> ListIdRoles, UserInfo user) {
// 			try {
// 				Role role = new Role();
// 				List<Role> ListRols = new List<Role>();
// 				foreach (var Id in ListIdRoles) {
// 					var roleModel = Context.Roles.FirstOrDefault(w => w.Id == Id);
// 					ListRols.Add(roleModel);
// 				}
// 				string rolesLog = string.Join("; ", ListRols.Select(x => x.LogString));

// 				Context.Roles.RemoveRange(ListRols);
// 				Context.SaveChanges();

// 				logRepo.LogEvent(SubSystemType.Users, SystemEventType.Error, $"Удаление ролей:\n ({rolesLog})", "Успешно", user, out string logResult, user.FilialId, user.FilialDateChange);
// 				return new BaseResponse(new RoleViewModel());
// 			} catch (Exception exc) {
// 				logRepo.LogEvent(SubSystemType.Users, SystemEventType.Error, $"Удаление ролей", "Ошибка удаления", user, out string logResult, user.FilialId, user.FilialDateChange);
// 				return new BaseResponse(exc);
// 			}
// 		}

// 		/// <summary>
// 		/// Удаление выбранных групп ад роли
// 		/// </summary>
// 		/// <param name="ListIdRoles">Список выбранных Ид ролей для удаления</param>
// 		/// <returns></returns>
// 		public BaseResponse DeleteRoles(Guid IdRole, List<string> Groups, UserInfo user) {
// 			var role = Context.Roles.FirstOrDefault(f => f.Id == IdRole);
// 			try {
// 				var oldGroups = role.GroupAd.Split(new char[] { ';' }).ToList();
// 				string newGroups = "";
// 				foreach (var group in oldGroups) {
// 					if (!Groups.Contains(group) && !string.IsNullOrWhiteSpace(group)) {
// 						newGroups += group + ";";
// 					}
// 				}
// 				newGroups = newGroups.TrimEnd(';');
// 				role.GroupAd = newGroups;
// 				Context.SaveChanges();

// 				logRepo.LogEvent(SubSystemType.Users, SystemEventType.Error, $"Удаление групп AD роли '{role.Name}':\nдо изменений - ({string.Join(";", oldGroups)})\nпосле изменений - ({newGroups})", "Ошибка удаления", user, out string logResult, user.FilialId, user.FilialDateChange);
// 				return new BaseResponse(new RoleViewModel());
// 			} catch (Exception exc) {
// 				logRepo.LogEvent(SubSystemType.Users, SystemEventType.Error, $"Удаление групп AD роли '{role.Name}'", "Ошибка удаления", user, out string logResult, user.FilialId, user.FilialDateChange);
// 				return new BaseResponse(exc);
// 			}
// 		}
// 		/// <summary>
// 		/// Получение списка всех групп АД
// 		/// </summary>
// 		/// <returns></returns>
// 		public List<string> GetAllADGroups() {
// 			try {
// 				var allADGroups = new List<string>();
// 				var roleGroups = Context.Roles.Where(x => !string.IsNullOrEmpty(x.GroupAd)).Select(x => x.GroupAd).ToList();
// 				foreach (var groups in roleGroups) {
// 					allADGroups.AddRange(groups.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
// 				}
// 				return allADGroups.Distinct().ToList();
// 			} catch {
// 				return new List<string>();
// 			}
// 		}
// 		/// <summary>
// 		/// Получение всех прав для настройки ролей
// 		/// </summary>
// 		/// <returns></returns>
// 		public BaseResponseEnumerable GetTreeAccessRigths(Guid? roleId) {
// 			try {
// 				var resp = _buildRepository.GetTreeRights(roleId);
// 				if (resp.State != ResultState.Success) {
// 					return new BaseResponseEnumerable(resp.Error);
// 				}
// 				return new BaseResponseEnumerable(resp.Result);
// 			} catch (Exception exc) {
// 				return new BaseResponseEnumerable(exc);
// 			}
// 		}
// 		/// <summary>
// 		/// Получить роли для пользователя по группам AD
// 		/// </summary>
// 		/// <param name="groups">группы AD</param>
// 		/// <returns></returns>
// 		public BaseResponseEnumerable<RoleViewModel> GetRolesForUserByGroup(IEnumerable<string> groups) {
// 			try {

// 				//return new BaseResponseEnumerable<RoleViewModel>(new List<RoleViewModel>
// 				//{
// 				//	new RoleViewModel
//     //                {
// 				//		Id = new Guid("046B0508-F051-4023-862F-F44E2C2F9DFE"),
// 				//		Name = "Администратор ИА",
// 				//		GroupAD = "cdu\\ia_pgos_admin",
// 				//		IsDefault = true
//     //                }
// 				//});


// 				if (groups == null || groups.Count(x => !string.IsNullOrEmpty(x)) == 0) {
// 					return new BaseResponseEnumerable<RoleViewModel>(new List<RoleViewModel>());
// 				}

// 				groups = groups.Where(x => !string.IsNullOrEmpty(x));
// 				var dbRoles = GetAll<Role>(true).Where(x => !string.IsNullOrEmpty(x.GroupAd)).ToArray().Select(ModelToViewModel());

// 				IList<RoleViewModel> roles = new List<RoleViewModel>();

// 				foreach (var rol in dbRoles) {
// 					var rolGrs = rol.GroupAD.Split(';');
// 					foreach (var rGr in rolGrs) {
// 						if (groups.Any(x => x.Trim().ToUpper() == rGr.ToUpper())) {
// 							roles.Add(rol);
// 						}
// 					}
// 				}

// 				return new BaseResponseEnumerable<RoleViewModel>(roles);
// 			} catch (Exception ex) {
// 				return new BaseResponseEnumerable<RoleViewModel>(ex);
// 			}
// 		}
// 		/// <summary>
// 		/// Обновить роли пользователя
// 		/// </summary>
// 		/// <param name="roleIds">Ид ролей</param>
// 		/// <returns></returns>
// 		public BaseResponseEnumerable<ValueResponseModel<Guid>> UpdateUserRoles(Guid userId, IEnumerable<Guid> roleIds) {
// 			try {
// 				if (roleIds == null) 
// 					roleIds = new Guid[0];
// 				var retRoles = new List<ValueResponseModel<Guid>>();

// 				var dbRoles = Context.UserRoles.Where(x => x.UserId == userId).ToList();
// 				foreach (var role in dbRoles) {
// 					if (roleIds.Contains(role.RoleId)) {
// 						roleIds = roleIds.Where(x => x != role.RoleId);
// 						retRoles.Add(new ValueResponseModel<Guid>(role.RoleId));
// 						continue;
// 					}
// 					Context.UserRoles.Remove(role);
// 				}
				
// 				foreach (var roleId in roleIds) {
// 					retRoles.Add(new ValueResponseModel<Guid>(roleId));
// 					Context.UserRoles.Add(new UserRole(userId, roleId));
// 				}

// 				Context.SaveChanges();
// 				return new BaseResponseEnumerable<ValueResponseModel<Guid>>(retRoles);
// 			} catch (Exception ex) {
// 				return new BaseResponseEnumerable<ValueResponseModel<Guid>>(ex);
// 			}
// 		}
// 		/// <summary>
// 		/// Получит роли пользователя
// 		/// </summary>
// 		/// <param name="userId">ИД пользователя</param>
// 		/// <returns></returns>
// 		public BaseResponseEnumerable<RoleViewModel> GetRolesByUser(Guid userId) {
// 			try {
// 				var roles = Context.UserRoles.Include(x => x.Role).Where(x => x.UserId == userId).Select(x => x.Role).Select(ModelToViewModel()).ToArray();
// 				return new BaseResponseEnumerable<RoleViewModel>(roles);
// 			} catch (Exception ex) {
// 				return new BaseResponseEnumerable<RoleViewModel>(ex);
// 			}
// 		}
// 	}
// }
