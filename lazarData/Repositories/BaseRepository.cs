using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazarData.Repositories {
    public abstract class BaseRepository<TViewModel, TModel>
            where TViewModel : BaseResponseModel
            where TModel : class {
        private RmContext _context;
        /// <summary>
        /// Контекст
        /// </summary>
        public RmContext Context => _context ?? (_context = new RmContext(Helpers.RepositoryHelper.GetDbConnectionStringName()));
        /// <summary>
        /// Преобразовывает сущность из базы данных в модель представления
        /// </summary>
        /// <returns></returns>
        public abstract Func<TModel, TViewModel> ModelToViewModel();

        /// <summary>
        /// Получить все записи в таблице
        /// </summary>
        /// <param name="isNoTracking">Не отслеживать изменения</param>
        /// <returns></returns>
        public IQueryable<FModel> GetAll<FModel>(bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel {
            try {
                if (Context == null) {
                    throw new ArgumentNullException("Контекст не инициализирован");
                }
                var query = Context.Set<FModel>().AsQueryable();
                if (includes != null && includes.Length > 0) {
                    foreach (var include in includes) {
                        query = query.Include(include);
                    }
                }
                if (isNoTracking) {
                    return query.AsNoTracking();
                }
                return query;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public IQueryable<FModel> GetAll<FModel>(DeletedSearchType type, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IDeleted {
            try {
                return GetAll(isNoTracking, includes).GetByStatus(type);
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Получить записи справочников по дате актуальности
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="actualDate">Дата актуальности</param>
        /// <param name="isNoTracking">Без отслеживания изменений</param>
        /// <param name="includes">Сущности, которые необходимо подтянуть</param>
        /// <returns></returns>
        public IQueryable<FModel> GetAllByActualDate<FModel>(DateTime actualDate, DeletedSearchType deletedSearch = DeletedSearchType.Actual, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, IActualDate, IKeyEntity, IDeleted, TModel {
            try {
                if (Context == null) {
                    throw new ArgumentNullException("Контекст не инициализирован");
                }
                var query = Context.Set<FModel>().AsQueryable();
                query = query.Join(query.Where(x => x.ActualDate <= actualDate)
                                        .Select(m => new { m.Id, m.ActualDate })
                                        .GroupBy(m => m.Id)
                                        .Select(m => new { m.Key, LastActual = m.Max(k => k.ActualDate) }),
                                                SourceData => new { Key = SourceData.Id, LastActual = SourceData.ActualDate },
                                                KeyData => new { KeyData.Key, KeyData.LastActual },
                                                (SourceData, KeyData) => SourceData);
                if (deletedSearch != DeletedSearchType.All) {
                    query = query.GetByStatus(deletedSearch);
                }
                if (includes != null && includes.Length > 0) {
                    foreach (var include in includes) {
                        query = query.Include(include);
                    }
                }
                if (isNoTracking) {
                    return query.AsNoTracking();
                }
                return query;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Получить все актуальные записи в темпоральной таблице
        /// </summary>
        /// <param name="isNoTracking">Не отслеживать изменения</param>
        /// <returns></returns>
        public IQueryable<FModel> GetAllTemporary<FModel>(bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IDateChange, IKeyEntity {
            try {
                if (Context == null) {
                    throw new ArgumentNullException("Контекст не инициализирован");
                }
                var query = Context.Set<FModel>().AsQueryable().GetAllTemporary();
                if (includes != null && includes.Length > 0) {
                    foreach (var include in includes) {
                        query = query.Include(include);
                    }
                }

                if (isNoTracking) {
                    return query.AsNoTracking();
                }
                return query;
            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Получить все записи в темпоральной таблице
        /// </summary>
        /// <param name="isNoTracking">Не отслеживать изменения</param>
        /// <returns></returns>
        public IQueryable<FModel> GetAllTemporary<FModel>(DeletedSearchType type, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IDateChange, IKeyEntity, IDeleted {
            try {
                //if (type == DeletedSearchType.All) {
                //	return GetAll<FModel>(isNoTracking, includes).OrderByDescending(o => o.DateChange);
                //}
                return GetAllTemporary(isNoTracking, includes).GetByStatus(type);
            } catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// Получить запись по ИД
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="Id">ИД записи</param>
        /// <param name="isNoTracking">Отслеживать изменения</param>
        /// <returns></returns>
        public virtual TViewModel GetViewById<FModel>(Guid Id, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IKeyEntity {
            try {
                var query = GetAll<FModel>(isNoTracking, includes);
                var order = query.Where(x => x.Id == Id).Select(ModelToViewModel()).FirstOrDefault();
                return order;
            } catch (Exception exp) {
                return null;
            }
        }

        /// <summary>
        /// Получить запись по ИД
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="Id">ИД записи</param>
        /// <param name="isNoTracking">Отслеживать изменения</param>
        /// <returns></returns>
        public virtual TViewModel GetViewById<FModel>(Guid Id, DeletedSearchType deleted, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IKeyEntity, IDeleted {
            try {
                var query = GetAll<FModel>(deleted, isNoTracking, includes);
                var order = query.Where(x => x.Id == Id).Select(ModelToViewModel()).FirstOrDefault();
                return order;
            } catch (Exception exp) {
                return null;
            }
        }

        /// <summary>
        /// Получить запись по ИД
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="Id">ИД записи</param>
        /// <param name="isNoTracking">Отслеживать изменения</param>
        /// <returns></returns>
        public virtual TViewModel GetViewByName<FModel>(string name, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IName {
            try {
                if (string.IsNullOrEmpty(name)) {
                    return null;
                }

                var query = GetAll<FModel>(isNoTracking, includes);
                var order = query.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.Trim().ToUpper() == name.Trim().ToUpper()).Select(ModelToViewModel()).FirstOrDefault();
                return order;
            } catch (Exception exp) {
                return null;
            }
        }
        /// <summary>
        /// Получить запись по составному ключу Ид и дате изменения
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="Id">Ид Записи</param>
        /// <param name="DateChange">Дата изменения записи</param>
        /// <param name="isNoTracking">Остлуживать изменения</param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual TViewModel GetViewByTemporaryKey<FModel>(Guid Id, DateTime? DateChange, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IKeyEntity, IDateChange {
            try {
                IQueryable<FModel> query = null;
                query = DateChange.HasValue
                    ? GetAll<FModel>(isNoTracking, includes)
                        .Where(x => x.Id == Id && x.DateChange == DateChange)
                    : GetAllTemporary<FModel>(isNoTracking, includes)
                        .Where(x => x.Id == Id);

                var order = query.Select(ModelToViewModel()).FirstOrDefault();

                return order;
            } catch (Exception exp) {
                return null;
            }
        }
        /// <summary>
        /// Получить запись по ИД
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="Id">ИД записи</param>
        /// <param name="isNoTracking">Отслеживать изменения</param>
        /// <returns></returns>
        public virtual FModel GetEntityById<FModel>(Guid Id, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IKeyEntity {
            try {
                var query = GetAll<FModel>(isNoTracking, includes);
                var order = query.FirstOrDefault(x => x.Id == Id);
                return order;
            } catch (Exception exp) {
                return null;
            }
        }
        /// <summary>
        /// Получить запись по ИД
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="Id">ИД записи</param>
        /// <param name="isNoTracking">Отслеживать изменения</param>
        /// <returns></returns>
        public virtual FModel GetEntityById<FModel>(Guid Id, DeletedSearchType deleted, bool isNoTracking = false, params Expression<Func<FModel, object>>[] includes)
            where FModel : class, TModel, IKeyEntity, IDeleted {
            try {
                var query = GetAll<FModel>(deleted, isNoTracking, includes);
                var order = query.FirstOrDefault(x => x.Id == Id);
                return order;
            } catch (Exception exp) {
                return null;
            }
        }

        /// <summary>
        /// Удаление по ид
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IHelperResult RemoveByIds<FModel>(Guid id)
            where FModel : class, TModel, IKeyEntity, new() {
            return RemoveByIds<FModel>(new Guid[] { id });
        }
        /// <summary>
        /// Удаление по ид
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IHelperResult RemoveByIds<FModel>(Guid[] ids)
            where FModel : class, TModel, IKeyEntity, new() {
            try {
                foreach (var id in ids) {
                    var entry = Context.Entry<FModel>(new FModel() {
                        Id = id
                    });
                    entry.State = EntityState.Deleted;
                }

                Context.SaveChanges();

                return new BaseResponse(new BaseResponseModel());
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }
        /// <summary>
        /// Удаление по ид
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IHelperResult RemoveByIdsWithStatus<FModel>(Guid id, Expression<Func<FModel, bool>> condition = null)
            where FModel : class, TModel, IKeyEntity, IDeleted, new() {
            return RemoveByIdsWithStatus<FModel>(new Guid[] { id }, condition);
        }
        /// <summary>
        /// Удаление по ид
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public BaseResponseEnumerable RemoveByIdsWithStatus<FModel>(Guid[] ids, Expression<Func<FModel, bool>> condition = null)
            where FModel : class, TModel, IKeyEntity, IDeleted, new() {
            try {
                var query = Context.Set<FModel>().AsQueryable();
                if (condition != null) {
                    query = query.Where(condition);
                }

                var list = query.ContainsByStep(x => x.Id, ids);

                foreach (var row in list) {
                    row.IsDeleted = true;
                }

                Context.SaveChanges();

                return new BaseResponseEnumerable(list.Select(ModelToViewModel()));
            } catch (Exception ex) {
                return new BaseResponseEnumerable(ex);
            }
        }
        /// <summary>
        /// Удаление по внешнему ид
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IHelperResult RemoveByRelationIds<FModel>(Guid[] ids)
            where FModel : class, TModel, IKeyEntity, IRelationId, new() {
            try {
                foreach (var id in ids) {
                    var entry = Context.Entry<FModel>(new FModel() {
                        RelationId = id
                    });
                    entry.State = EntityState.Deleted;
                }

                Context.SaveChanges();

                return new BaseResponse(new BaseResponseModel());
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }
        /// <summary>
        /// Уникальное наименование
        /// </summary>
        /// <typeparam name="FModel">Модель</typeparam>
        /// <param name="name">Наименование</param>
        /// <param name="id">Ид</param>
        /// <returns></returns>
        public IHelperResult IsUniqueName<FModel>(string name, Guid? id = null)
            where FModel : class, TModel, IKeyEntity, IName {
            try {
                if (id.HasValue && id == Guid.Empty) {
                    id = new Guid?();
                }
                var isUn = true; // GetAll<FModel>(true).IsUniqueName(name, id);
                return new BaseResponse<ValueResponseModel<bool>>(new ValueResponseModel<bool>(isUn));
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }

        public IHelperResult IsUniqueIcpName<FModel>(string icpName, Guid? id = null)
            where FModel : class, TModel, IKeyEntity, IIcpNames, IDeleted, IDateChange {
            try {
                if (id.HasValue && id == Guid.Empty) {
                    id = new Guid?();
                }
                var isUn = GetAllTemporary<FModel>(DeletedSearchType.Actual, true).IsUniqueIcpName(icpName, id);
                return new BaseResponse<ValueResponseModel<bool>>(new ValueResponseModel<bool>(isUn));
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }

        public IHelperResult IsUniqueIcpCode<FModel>(string icpCode, Guid? id = null)
            where FModel : class, TModel, IKeyEntity, IIcpNames, IDeleted, IDateChange {
            try {
                if (id.HasValue && id == Guid.Empty) {
                    id = new Guid?();
                }
                var isUn = GetAllTemporary<FModel>(DeletedSearchType.Actual, true).IsUniqueIcpCode(icpCode, id);
                return new BaseResponse<ValueResponseModel<bool>>(new ValueResponseModel<bool>(isUn));
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }
        /// <summary>
        /// Уникальное имя и по ключу 
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="name"></param>
        /// <param name="DbLiftId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHelperResult IsUniqueNameByDbLiftId<FModel>(string name, int? DbLiftId, Guid? id = null)
            where FModel : class, TModel, IKeyEntity, IName, IDbLift {
            try {
                if (id.HasValue && id == Guid.Empty) {
                    id = new Guid?();
                }
                var isUn = true; //  GetAll<FModel>(true).IsUniqueNameByDbLiftId(name, DbLiftId, id);
                return new BaseResponse<ValueResponseModel<bool>>(new ValueResponseModel<bool>(isUn));
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }
        /// <summary>
        /// Унникальность аббревиатуры
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHelperResult IsUniqueIdent<FModel>(string name, Guid? id = null)
            where FModel : class, TModel, IKeyEntity, IName, IDbLift {
            try {
                if (id.HasValue && id == Guid.Empty) {
                    id = new Guid?();
                }
                var isUn = true; // GetAll<FModel>(true).IsUniqueIdent(name, id);
                return new BaseResponse<ValueResponseModel<bool>>(new ValueResponseModel<bool>(isUn));
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }
        /// <summary>
        /// Уникальность шифра 
        /// </summary>
        /// <typeparam name="FModel"></typeparam>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHelperResult IsUniqueShifr<FModel>(string name, Guid? id = null)
            where FModel : class, TModel, IKeyEntity, IName, IDbLift, IShifr {
            try {
                if (id.HasValue && id == Guid.Empty) {
                    id = new Guid?();
                }

                // В базе куча задвоенных шифров по разным филиалам, временно отключим проверку
                var isUn = true; // GetAll<FModel>(true).IsUniqueShifr(name, id);

                return new BaseResponse<ValueResponseModel<bool>>(new ValueResponseModel<bool>(isUn));
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }


        /// <summary>
        /// Уникальный ключ ПО Подъем
        /// </summary>
        /// <typeparam name="FModel">Модель</typeparam>
        /// <param name="name">Наименование</param>
        /// <param name="id">Ид</param>
        /// <returns></returns>
        public IHelperResult IsUniuqeTemporaryLiftKey<FModel>(string name, Guid? id = null)
            where FModel : class, TModel, IKeyEntity, IDbLift, IDateChange, IDeleted {
            try {
                if (id.HasValue && id == Guid.Empty) {
                    id = new Guid?();
                }
                var isUn = true; // GetAllTemporary<FModel>(DeletedSearchType.Actual, true).IsUniqueDBLiftKey(name, id);
                return new BaseResponse<ValueResponseModel<bool>>(new ValueResponseModel<bool>(isUn));
            } catch (Exception ex) {
                return new BaseResponse(ex);
            }
        }

        //public void AddAlls<FModel>(IEnumerable<FModel> entityObjects,RmContext context)
        //	where FModel : class {
        //	try {

        //		context.Configuration.AutoDetectChangesEnabled = false;
        //		context.Configuration.ValidateOnSaveEnabled = false;
        //		context.Database.CommandTimeout = 360;
        //		context.BulkInsert(entityObjects);			
        //		GC.Collect();
        //		GC.WaitForPendingFinalizers();
        //	} catch (Exception ex) {
        //		throw ex;
        //	}
        //}
        //public void AddAlls<FModel>(IEnumerable<FModel> entityObjects)
        //	where FModel : class {
        //	try {

        //		Context.Configuration.AutoDetectChangesEnabled = false;
        //		Context.Configuration.ValidateOnSaveEnabled = false;
        //		Context.Database.CommandTimeout = 360;

        //		Context.BulkInsert(entityObjects);
        //		GC.Collect();
        //		GC.WaitForPendingFinalizers();
        //	} catch (Exception ex) {
        //		throw ex;
        //	}
        //}
        public void AddAll<FModel>(IEnumerable<FModel> entityObjects, int BulkCopyTimeout = 600)
          where FModel : class {
            try {
                using (var sqlBulk = new SqlBulkCopy(Context.Database.Connection.ConnectionString)) {

                    sqlBulk.BulkCopyTimeout = BulkCopyTimeout;
                    sqlBulk.DestinationTableName = typeof(FModel).Name;
                    sqlBulk.WriteToServer(entityObjects.AsDataReader());

                    sqlBulk.BulkCopyTimeout = BulkCopyTimeout * 1000;
                    sqlBulk.BatchSize = 1000;

                    sqlBulk.Close();
                    ((IDisposable)sqlBulk).Dispose();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            } catch (Exception ex) {
                throw ex;
            }
        }
        public void AddAll<FModel>(IEnumerable<FModel> entityObjects, string TableName, int BulkCopyTimeout = 600)
            where FModel : class {
            try {
                using (var sqlBulk = new SqlBulkCopy(Context.Database.Connection.ConnectionString)) {
                    sqlBulk.BulkCopyTimeout = BulkCopyTimeout;
                    sqlBulk.BatchSize = 1000;
                    sqlBulk.DestinationTableName = TableName;
                    sqlBulk.WriteToServer(entityObjects.AsDataReader());
                    sqlBulk.Close();
                    ((IDisposable)sqlBulk).Dispose();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void AddRangeBulk<TModel>(IEnumerable<TModel> Entities, int Timeout = 300, int BatchSize = 1000) where TModel : class {
            try {
                if (Entities.Count() == 0) {
                    return;
                }
                var TableName = typeof(TModel).Name;
                List<string> Columns = new List<string>();
                using (var ctx = new RmContext(Context.Database.Connection.ConnectionString)) {
                    using (var command = ctx.Database.Connection.CreateCommand()) {
                        command.CommandText = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{TableName}' ORDER BY ORDINAL_POSITION";
                        ctx.Database.Connection.Open();
                        using (var result = command.ExecuteReader()) {
                            while (result.Read()) {
                                Columns.Add(result.GetString(0));
                            }
                        }
                    }
                }
                using (var sqlCopy = new SqlBulkCopy(Context.Database.Connection.ConnectionString)) {
                    sqlCopy.BulkCopyTimeout = Timeout * 1000;
                    sqlCopy.DestinationTableName = TableName;
                    sqlCopy.BatchSize = BatchSize;
                    sqlCopy.WriteToServer(Entities.AsDataReader(Columns.ToArray()));
                    sqlCopy.Close();
                    ((IDisposable)sqlCopy).Dispose();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            } catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// Получить тип контрольнонго срока по дате
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetDeadlineType(ReportingPeriodType type, DateTime? date, out int day, out int month) {
            if (!date.HasValue) {
                day = 1;
                month = 1;
                return 1;
            }
            var deadLine = date.Value.ToLocalTime();
            day = deadLine.Day;
            month = type == ReportingPeriodType.Year ? deadLine.Month : 1;

            switch (type) {
                case ReportingPeriodType.Month: {
                        return deadLine.Month == 12 ? 0 : deadLine.Month == 1 ? 1 : 2;
                    }
                case ReportingPeriodType.Quarter: {
                        return deadLine.Month >= 10 && deadLine.Month <= 12 ? 0 : deadLine.Month >= 1 && deadLine.Month <= 3 ? 1 : 2;
                    }
                case ReportingPeriodType.Year: {
                        return deadLine.Year == 1999 ? 0 : deadLine.Year == 2000 ? 1 : 2;
                    }
            }
            return 1;
        }
        /// <summary>
        /// Получить дату по типу контрольного срока
        /// </summary>
        /// <param name="type"></param>
        /// <param name="deadlineType"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public DateTime GetDeadlineDate(ReportingPeriodType type, int deadlineType, int month, int day) {
            switch (type) {
                case ReportingPeriodType.Month: {
                        return new DateTime(DateTime.UtcNow.Year, deadlineType == 0 ? 12 : deadlineType, day);
                    }
                case ReportingPeriodType.Quarter: {
                        return new DateTime(DateTime.UtcNow.Year, deadlineType == 0 ? 10 : deadlineType == 1 ? 1 : 4, day);
                    }
                case ReportingPeriodType.Year: {
                        return new DateTime(deadlineType == 0 ? 1999 : deadlineType == 1 ? 2000 : 2001, month, day);
                    }
            }
            return new DateTime(2000, 1, 1);
        }
        /// <summary>
        /// Преобразование даты в строку с миллисекундами
        /// </summary>       
        /// <param name="date"></param>
        /// <returns></returns>
        public string ConvertDateToStringHaveMilissecond(DateTime date) {
            try {
                if (date == null)
                    return "";
                var result = date.ToString("dd.MM.yyyy HH:mm:ss:fffffff");
                return result;
            } catch (Exception exc) {
                throw exc;
            }
        }
        /// <summary>
        /// Метод преобразования строки в формат dateTime с миллисекундами для темпаралльных данных
        /// </summary>
        /// <param name="DateChange"></param>
        /// <returns></returns>
        protected DateTime ConvertStringFormatDataChangehaveMlisecond(string DateChange) {
            try {
                DateTime date;
                DateTime.TryParseExact(DateChange, "dd.MM.yyyy HH:mm:ss:fffffff", CultureInfo.InstalledUICulture, DateTimeStyles.None, out date);
                return date;
            } catch (Exception exc) {
                return new DateTime();
            }
        }

        /// <summary>
        /// Получить список актуальных темпоральных объектов по ID и имени
        /// </summary>
        /// <returns></returns>
        public BaseResponseEnumerable GetListTemporaryElements<T>(string searchText, string key)
            where T : class, TModel, IKeyEntity, IDateChange, IName {
            try {
                var query = GetAllTemporary<T>(true);
                if (!string.IsNullOrEmpty(searchText)) {
                    query = query.Where(x => x.Name.Contains(searchText));
                }

                Guid id = Guid.Empty;
                if (!Guid.TryParse(key, out id)) {
                }

                var pQuery = query
                    .OrderBy(x => x.Name)
                    .Take(2)
                    .Select(x => new ListItemResponseModel<Guid>() {
                        Key = x.Id,
                        Text = x.Name,
                        DateChange = x.DateChange
                    });
                if (id != Guid.Empty) {
                    pQuery = pQuery.Union(GetAllTemporary<T>(true)
                        .Where(x => x.Id == id)
                        .Select(x => new ListItemResponseModel<Guid>() {
                            Key = x.Id,
                            Text = x.Name,
                            DateChange = x.DateChange,
                        }));
                }
                var listItemResponseModels = pQuery.ToArray();

                return new BaseResponseEnumerable(listItemResponseModels);
            } catch (Exception ex) {
                return new BaseResponseEnumerable(ex);
            }
        }
        /// <summary>
        /// Получить список актуальных темпоральных объектов по ID, DateChange  и имени
        /// </summary>
        /// <returns></returns>
        public BaseResponseEnumerable GetListTemporaryElements<T>(string searchText, string key, DateTime DateChange)
            where T : class, TModel, IKeyEntity, IDateChange, IName {
            try {
                var query = GetAllTemporary<T>(true);
                if (!string.IsNullOrEmpty(searchText)) {
                    query = query.Where(x => x.Name.Contains(searchText));
                }
                Guid id = Guid.Empty;
                if (!Guid.TryParse(key, out id)) {
                }

                var pQuery = query
                    .OrderBy(x => x.Name)
                    .Take(2)
                    .Select(x => new ListItemResponseModel<Guid>() {
                        Key = x.Id,
                        Text = x.Name,
                        DateChange = x.DateChange
                    });
                if (id != Guid.Empty) {
                    pQuery = pQuery.Union(GetAllTemporary<T>(true)
                        .Where(x => x.Id == id && x.DateChange == DateChange)
                        .Select(x => new ListItemResponseModel<Guid>() {
                            Key = x.Id,
                            Text = x.Name,
                            DateChange = x.DateChange,
                        }));
                }
                var listItemResponseModels = pQuery.ToArray();

                return new BaseResponseEnumerable(listItemResponseModels);
            } catch (Exception ex) {
                return new BaseResponseEnumerable(ex);
            }
        }
        public BaseResponseEnumerable GetListTemporaryStatusElementsByFilial<T>(string searchText, string key, Guid filialId, DeletedSearchType type = DeletedSearchType.Actual)
            where T : class, TModel, IKeyEntity, IDateChange, IName, IDeleted, IFilialRelation {
            try {
                var query = GetAllTemporary<T>(true).GetByStatus(type).Where(x => x.FilialId == filialId);
                if (!string.IsNullOrEmpty(searchText)) {
                    query = query.Where(x => x.Name.Contains(searchText));
                }

                Guid id = Guid.Empty;
                if (!Guid.TryParse(key, out id)) {
                }

                var pQuery = query
                    .OrderBy(x => x.Name)
                    .Take(20)
                    .Select(x => new ListItemResponseModel<Guid>() {
                        Key = x.Id,
                        Text = x.Name,
                        DateChange = x.DateChange
                    });
                if (id != Guid.Empty) {
                    pQuery = pQuery.Union(GetAllTemporary<T>(true)
                        .GetByStatus(type)
                        .Where(x => x.Id == id)
                        .Select(x => new ListItemResponseModel<Guid>() {
                            Key = x.Id,
                            Text = x.Name,
                            DateChange = x.DateChange,
                        }));
                }
                var listItemResponseModels = pQuery.ToArray();

                return new BaseResponseEnumerable(listItemResponseModels);
            } catch (Exception ex) {
                return new BaseResponseEnumerable(ex);
            }
        }

        /// <summary>
        /// Получить список актуальных темпоральных объектов со статусом по ID и имени
        /// </summary>
        /// <returns></returns>
        public BaseResponseEnumerable GetListTemporaryStatusElements<T>(string searchText, Guid? Key, DeletedSearchType type = DeletedSearchType.Actual)
            where T : class, TModel, IKeyEntity, IDateChange, IName, IDeleted {
            try {
                var query = GetAllTemporary<T>(type, true);

                if (!string.IsNullOrEmpty(searchText)) {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.Trim().ToLower()));
                }

                var listItemResponseModels = query
                    .OrderByDescending(x => x.Id == Key)
                    .ThenBy(x => x.Name)
                    .Take(20)
                    .Select(x => new ListItemResponseModel<Guid>() {
                        Key = x.Id,
                        Text = x.Name,
                        DateChange = x.DateChange
                    });

                return new BaseResponseEnumerable(listItemResponseModels);
            } catch (Exception ex) {
                return new BaseResponseEnumerable(ex);
            }
        }

        /// <summary>
        /// Получить список объектов по ID и имени
        /// </summary>
        /// <returns></returns>
        public BaseResponseEnumerable GetListElements<T>(string searchText, string key)
            where T : class, TModel, IKeyEntity, IName {
            try {
                var query = GetAll<T>(true);
                if (!string.IsNullOrEmpty(searchText)) {
                    query = query.Where(x => x.Name.Contains(searchText));
                }

                Guid id = Guid.Empty;
                if (!Guid.TryParse(key, out id)) {
                }

                var pQuery = query
                    .OrderBy(x => x.Name)
                    .Take(20)
                    .Select(x => new ListItemResponseModel<Guid>() {
                        Key = x.Id,
                        Text = x.Name,
                    });
                if (id != Guid.Empty) {
                    pQuery = pQuery.Union(GetAll<T>(true)
                        .Where(x => x.Id == id)
                        .Select(x => new ListItemResponseModel<Guid>() {
                            Key = x.Id,
                            Text = x.Name,
                        }));
                }
                var listItemResponseModels = pQuery.ToArray();

                return new BaseResponseEnumerable(listItemResponseModels);
            } catch (Exception ex) {
                return new BaseResponseEnumerable(ex);
            }
        }
        /// <summary>
        /// Получить список объектов со статусом по ID и имени
        /// </summary>
        /// <returns></returns>
        public BaseResponseEnumerable GetListStatusElements<T>(string searchText, string key, DeletedSearchType type = DeletedSearchType.Actual)
            where T : class, TModel, IKeyEntity, IName, IDeleted {
            try {
                var query = GetAll<T>(true).GetByStatus(type);
                if (!string.IsNullOrEmpty(searchText)) {
                    query = query.Where(x => x.Name.Contains(searchText));
                }

                Guid id = Guid.Empty;
                if (!Guid.TryParse(key, out id)) {
                }

                var pQuery = query
                    .OrderBy(x => x.Name)
                    .Take(20)
                    .Select(x => new ListItemResponseModel<Guid>() {
                        Key = x.Id,
                        Text = x.Name,
                    });
                if (id != Guid.Empty) {
                    pQuery = pQuery.Union(GetAll<T>(true)
                        .GetByStatus(type)
                        .Where(x => x.Id == id)
                        .Select(x => new ListItemResponseModel<Guid>() {
                            Key = x.Id,
                            Text = x.Name,
                        }));
                }
                var listItemResponseModels = pQuery.ToArray();

                return new BaseResponseEnumerable(listItemResponseModels);
            } catch (Exception ex) {
                return new BaseResponseEnumerable(ex);
            }
        }
        /// <summary>
        /// Получение месячного периода(от начала текущего месяца до начала следующего месяца)
        /// </summary>
        /// <returns></returns>
        public DatePeriod GetDefaultMonthPeriod(DateTime? atTime = null) {
            DateTime now = atTime ?? DateTime.Now;
            var result = new DatePeriod { startDate = new DateTime(now.Year, now.Month, 1), endDate = new DateTime(now.Year, now.Month, 1).AddMonths(1) };
            return result;
        }
        /// <summary>
        /// Получение годового диспетчерского периода
        /// </summary>
        /// <param name="atTime">Получить период на дату (если не указана, то текущая)</param>
        /// <param name="shifted">Если true (по умолчанию), то сдвинуть начало годового периода в соответствии с настройками</param>
        /// <returns>Период дат (начало, конец) с учетом настроек годового периода ПГкОС</returns>
        public DatePeriod GetDefaultYearPeriod(DateTime? atTime = null, bool shifted = true) {
            DateTime setSt = new DateTime(2000, 1, 1);
            DateTime setEd = new DateTime(2001, 1, 1);
            if (shifted) {
                SystemSettingsRepository settingRepo = new SystemSettingsRepository();
                Exception Error;
                setSt = settingRepo.GetParamValueDate(out Error, Guids.SysSettingParam.GlobalYearPeriodStart);
                if (Error != null) throw new Exception("Ошибка получения системных настроек[setSt]: " + Error.Message);
                setEd = settingRepo.GetParamValueDate(out Error, Guids.SysSettingParam.GlobalYearPeriodEnd);
                if (Error != null) throw new Exception("Ошибка получения системных настроек[setEd]: " + Error.Message);
            }
            DateTime now = atTime ?? DateTime.UtcNow.AddHours(3); // Считаем, что сервер всегда работает по UTC+3
            if (now.Year < 1980 || now.Year > 2100) throw new Exception(String.Format("Неверно задано время[GetDefaultYearPeriod]: {0}", now));
            DateTime yearSt = setSt;
            DateTime yearEd = setEd;
            int yearStep = 1;
            if (setSt > now) yearStep = -1;
            while (yearSt > now || yearEd <= now) {
                yearSt = yearSt.AddYears(yearStep);
                yearEd = yearEd.AddYears(yearStep);
            }
            var result = new DatePeriod { startDate = yearSt, endDate = yearEd };
            return result;
        }

        public BaseResponse UploadToIcp(string collectionFormCode, Guid readinessInformationId, UserInfo user, bool noUserCheck = false, bool inTransaction = false) {
            string logResult = string.Empty;
            string logMsg = $"Загрузка сведений в СКТС, Id='{readinessInformationId}'";
            Guid? filId = null;
            DateTime? filDC = null;
            EventLogRepository _eventLogRepository = new EventLogRepository();
            try {
                var reIn = Context.ReadinessInformations
                    .Include(i => i.Indicator).Include(i => i.Filial).Include(i => i.TechProcessStage).Include(i => i.TechProcessStage.TechProcessesStageUsers)
                    .FirstOrDefault(x => x.Id == readinessInformationId);
                if (reIn == null)
                    throw new Exception($"Не найдены сведения Id={readinessInformationId}");
                if (reIn.Indicator != null && reIn.Filial != null)
                    logMsg += $", показатель: '{reIn.Indicator.Name}', филиал: '{reIn.Filial.Name}', период: '{reIn.ReportingPeriodDate:dd.MM.yyyy}'";
                filId = reIn.FilialId;
                filDC = reIn.FilialDateChange;
                // Проверить, что у пользователя есть права на экспорт сведений
                if (!noUserCheck) {
                    if (!reIn.TechProcessStage.ExportData)
                        throw new Exception("Сведения не должны выгружаться на данном этапе");
                    if (user?.UserId != UserGuid.SystemPGOSUserId && !reIn.TechProcessStage.TechProcessesStageUsers.Any(x => x.UserId == (user?.UserId ?? Guid.Empty) && x.ExportData))
                        throw new Exception("У пользователя отсутствуют права на выгрузку сведений");
                }
                var stream = CallOnExportXML(out Exception Error, readinessInformationId);
                if (Error != null)
                    throw Error;
#if (DEBUG)
                Thread.Sleep(1000);
#endif
#if (!DEBUG)
                string xmlBody = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                var repository = new ExternalSystemsRepository();
                var sets = repository.LoadModelSettingIcp();
                if (sets == null)
                    throw new Exception("Не удалось загрузить настройки доступа к СКТС");
                string url = sets.Result.Obj.ConnectionStringIcp;
                string login = sets.Result.Obj.UserNameIcp;
                string password = sets.Result.Obj.UserPasswordIcp;
                int timeout = sets.Result.Obj.WaitTimeoutIcp;
                var icpRepository = new IcpInteraction.IcpRepository(url, login, password, timeout);
                var result = icpRepository.UploadIcpData(collectionFormCode, login, xmlBody);
                if (result.Exception != null)
                    throw result.Exception;
#endif
                ReadinessInformationRepository readinessRepo;
                if (this is ReadinessInformationRepository)
                    readinessRepo = this as ReadinessInformationRepository;
                else
                    readinessRepo = new ReadinessInformationRepository();
                var stateResult = readinessRepo.ChangeState(readinessInformationId, TechProcessState.Exported, "", null, user, noUserCheck, inTransaction);
                if (stateResult.Error != null)
                    throw stateResult.Error;
                bool res = _eventLogRepository.LogEvent(SubSystemType.Indicators, SystemEventType.IcpExport, logMsg, "Успешно", user, out logResult, filId, filDC);
                if (!res)
                    throw new Exception(logResult);
                return new BaseResponse(new BaseResponseModel());
            } catch (Exception ex) {
                _eventLogRepository.LogEvent(SubSystemType.Indicators, SystemEventType.IcpExport, logMsg, ex.Message, user, out logResult, filId, filDC);
                return new BaseResponse(ex);
            }
        }

        public BaseResponse UploadArchiveToIcp(string collectionFormCode, Guid readinessInformationId, UserInfo user, Guid[] selIds = null, bool noUserCheck = false, bool inTransaction = false) {
            string logResult = string.Empty;
            string logMsg = $"Загрузка сведений в СКТС, Id='{readinessInformationId}'";
            Guid? filId = null;
            DateTime? filDC = null;
            EventLogRepository _eventLogRepository = new EventLogRepository();
            try {
                var reIn = Context.ReadinessInformations
                    .Include(i => i.Indicator).Include(i => i.Filial).Include(i => i.TechProcessStage).Include(i => i.TechProcessStage.TechProcessesStageUsers)
                    .FirstOrDefault(x => x.Id == readinessInformationId);
                if (reIn == null)
                    throw new Exception($"Не найдены сведения Id={readinessInformationId}");
                if (reIn.Indicator != null && reIn.Filial != null)
                    logMsg += $", показатель: '{reIn.Indicator.Name}', филиал: '{reIn.Filial.Name}', период: '{reIn.ReportingPeriodDate:dd.MM.yyyy}'";
                filId = reIn.FilialId;
                filDC = reIn.FilialDateChange;
                // Проверить, что у пользователя есть права на экспорт сведений
                if (!noUserCheck) {
                    if (!reIn.TechProcessStage.ExportData)
                        throw new Exception("Сведения не должны выгружаться на данном этапе");
                    if (user?.UserId != UserGuid.SystemPGOSUserId && !reIn.TechProcessStage.TechProcessesStageUsers.Any(x => x.UserId == (user?.UserId ?? Guid.Empty) && x.ExportData))
                        throw new Exception("У пользователя отсутствуют права на выгрузку сведений");
                }
                var archiveStream = CallOnGetArchive(out Exception error, readinessInformationId, "indicator", selIds);
                if (error != null)
                    throw error;
                List<string> xmlBodies = new List<string>();
                using (var zip = new ZipArchive(archiveStream, ZipArchiveMode.Read)) {
                    foreach (var file in zip.Entries) {
                        using (var ms = new MemoryStream()) {
                            file.Open().CopyTo(ms);
                            xmlBodies.Add(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
                        }
                    }
                }
#if (DEBUG)
                Thread.Sleep(2000);
#endif
#if (!DEBUG)
                var repository = new ExternalSystemsRepository();
                var sets = repository.LoadModelSettingIcp();
                if (sets == null)
                    throw new Exception("Не удалось загрузить настройки доступа к СКТС");
                string url = sets.Result.Obj.ConnectionStringIcp;
                string login = sets.Result.Obj.UserNameIcp;
                string password = sets.Result.Obj.UserPasswordIcp;
                int timeout = sets.Result.Obj.WaitTimeoutIcp;
                var icpRepository = new IcpInteraction.IcpRepository(url, login, password, timeout);
                foreach (var xmlBody in xmlBodies) {
                    var result = icpRepository.UploadIcpData(collectionFormCode, login, xmlBody);
                    if (result.Exception != null) {
                        throw result.Exception;
                    }
                }
#endif
                ReadinessInformationRepository readinessRepo;
                if (this is ReadinessInformationRepository)
                    readinessRepo = this as ReadinessInformationRepository;
                else
                    readinessRepo = new ReadinessInformationRepository();
                var stateResult = readinessRepo.ChangeState(readinessInformationId, TechProcessState.Exported, "", selIds, user, noUserCheck, inTransaction);
                if (stateResult.Error != null)
                    throw stateResult.Error;
                bool res = _eventLogRepository.LogEvent(SubSystemType.Indicators, SystemEventType.IcpExport, logMsg, "Успешно", user, out logResult, filId, filDC);
                if (!res)
                    throw new Exception(logResult);
                return new BaseResponse(new BaseResponseModel());
            } catch (Exception ex) {
                _eventLogRepository.LogEvent(SubSystemType.Indicators, SystemEventType.IcpExport, logMsg, ex.Message, user, out logResult, filId, filDC);
                return new BaseResponse(ex);
            }
        }

        public MemoryStream CallOnExportXML(out Exception error, Guid readinessInformationId) {
            error = null;
            var indicatorId = Context.ReadinessInformations.FirstOrDefault(x => x.Id == readinessInformationId)?.IndicatorId;

            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial1)
                return (new Indicators.Filial.FilialIndicator1.FilialInd1DetailRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial2)
                return (new Indicators.Filial.FilialIndicator2.FilialInd2DetailRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial3)
                return (new Indicators.Filial.FilialIndicator3.FilialInd3DetailRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial4)
                return (new Indicators.Filial.FilialIndicator4.FilialInd4DetailRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial5)
                return (new Indicators.Filial.FilialIndicator5.FilialIndicator5Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial6)
                return (new Indicators.Filial.FilialIndicator6.FilialIndicator6Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial7)
                return (new Indicators.Filial.FilialIndicator7.FilialIndicator7Repository()).OnExportIndicator(out error, readinessInformationId);

            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial13)
                return (new Indicators.Filial.FilialIndicator13.FilialIndicator13Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial14)
                return (new Indicators.Filial.FilialIndicator14.FilialIndicator14Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial15)
                return (new Indicators.Filial.FilialIndicator15.FilialIndicator15Repository()).OnExportIndicator(out error, readinessInformationId);

            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial17)
                return (new Indicators.Filial.FilialIndicator17.FilialIndicator17Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial18)
                return (new Indicators.Filial.FilialIndicator18.FilialIndicator18Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial19)
                return (new Indicators.Filial.FilialIndicator19.FilialIndicator19Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial20)
                return (new Indicators.Filial.FilialIndicator20.FilialIndicator20Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial21)
                return (new Indicators.Filial.FilialIndicator21.FilialIndicator21Repository()).OnExportIndicator(out error, readinessInformationId);

            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilialSpecial1)
                return (new Indicators.Filial.FilialSpecIndicator1.FilialSpecIndicator1Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilialSpecial2)
                return (new Indicators.Filial.FilialSpecIndicator2.FilialSpecInd2ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilialSpecial3)
                return (new Indicators.Filial.FilialSpecIndicator3.FilialSpecIndicator3Repository()).OnExportIndicator(out error, readinessInformationId);

            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject1)
                return (new Indicators.SE.SEIndicator1.SEInd1ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject2)
                return (new Indicators.SE.SEIndicator2.SEInd2ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject3)
                return (new Indicators.SE.SEIndicator3.SEInd3ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject4)
                return (new Indicators.SE.SEIndicator4.SEInd4ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject5)
                return (new Indicators.SE.SEIndicator5.SEInd5ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject6)
                return (new Indicators.SE.SEIndicator6.SEInd6ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject7)
                return (new Indicators.SE.SEIndicator7.SEInd7MinRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject7_1)
                return (new Indicators.SE.SEIndicator7_1.SEInd7_1MinRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject8)
                return (new Indicators.SE.SEIndicator8.SEInd8ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject9)
                return (new Indicators.SE.SEIndicator9.SEInd9ShortRepository()).OnExportIndicator(out error, readinessInformationId);

            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject11)
                return (new Indicators.SE.SEIndicator11.SEIndicator11Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject12_1)
                return (new Indicators.SE.SEIndicator12_1.SEInd12_1ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject13)
                return (new Indicators.SE.SEIndicator13.SEInd13ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject14)
                return (new Indicators.SE.SEIndicator14.SEInd14ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject15)
                return (new Indicators.SE.SEIndicator15.SEInd15ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject16)
                return (new Indicators.SE.SEIndicator16.SEInd16ShortRepository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject17)
                return (new Indicators.SE.SEIndicator17.SEIndicator17Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubject18)
                return (new Indicators.SE.SEIndicator18.SEInd18ShortRepository()).OnExportIndicator(out error, readinessInformationId);

            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubjectSpecial1)
                return (new Indicators.SE.SESpecIndicator1.SESpecIndicator1Repository()).OnExportIndicator(out error, readinessInformationId);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubjectSpecial2)
                return (new Indicators.SE.SESpecIndicator2.SEIndSpec2ShortRepository()).OnExportIndicator(out error, readinessInformationId);

            return null;
        }
        public MemoryStream CallOnGetArchive(out Exception error, Guid readinessInformationId, string fileName, Guid[] selIds = null) {
            error = null;
            var indicatorId = Context.ReadinessInformations.FirstOrDefault(x => x.Id == readinessInformationId)?.IndicatorId;

            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial8)
                return (new Indicators.Filial.FilialIndicator8.FilialIndicator8Repository()).GetArchive(out error, readinessInformationId, fileName, selIds);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial9)
                return (new Indicators.Filial.FilialIndicator9.FilialIndicator9Repository()).GetArchive(out error, readinessInformationId, fileName, selIds);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial10)
                return (new Indicators.Filial.FilialIndicator10.FilialIndicator10Repository()).GetArchive(out error, readinessInformationId, fileName, selIds);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial11)
                return (new Indicators.Filial.FilialIndicator11.FilialIndicator11Repository()).GetArchive(out error, readinessInformationId, fileName, selIds);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial12)
                return (new Indicators.Filial.FilialIndicator12.FilialIndicator12Repository()).GetArchive(out error, readinessInformationId, fileName, selIds);

            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial16_1)
                return (new Indicators.Filial.FilialIndicator16_1.FilialIndicator16_1Repository()).GetArchive(out error, readinessInformationId, fileName, selIds);
            if (indicatorId == Guids.IndicatorType.Filial.IndicatorFilial16_2)
                return (new Indicators.Filial.FilialIndicator16_2.FilialIndicator16_2Repository()).GetArchive(out error, readinessInformationId, fileName, selIds);

            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubjectSpecialAccidents)
                return (new Indicators.SE.SEIndicatorSpecialAccidents.SEIndicatorSpecialAccidentsRepository()).GetArchive(out error, readinessInformationId, fileName);
            if (indicatorId == Guids.IndicatorType.Subject.IndicatorSubjectSpecial5)
                return (new Indicators.SE.SESpecIndicator5.SEIndSpec5ShortRepository()).GetArchive(out error, readinessInformationId, fileName);

            return null;
        }

        public virtual MemoryStream OnExportIndicator(out Exception error, Guid readinessInformationId) {
            throw new Exception("BaseRepository OnExportIndicator call");
        }
        public virtual MemoryStream GetArchive(out Exception error, Guid readinessInformationId, string fileName, Guid[] selIds = null) {
            throw new Exception("BaseRepository GetArchive call");
        }
        //public virtual MemoryStream OnExportIndicator(Guid readinessInformationId, PersonalCategory category) {
        //	throw new Exception("BaseRepository OnExportIndicator call[2]");
        //}
        public string GetFilialWorkSheetBottomTitle(Guid readinessInformationId) {
            if (readinessInformationId == null || readinessInformationId == Guid.Empty)
                return "Не удалось определить сведения";
            string filialName = "";
            string reportingPeriod = "";
            string state = "";
            try {
                var readinessInformation = Context.ReadinessInformations.
                                                    Include(r => r.Filial).
                                                    Include(r => r.Indicator).
                                                    FirstOrDefault(x => x.Id == readinessInformationId);
                if (readinessInformation != null) {
                    filialName = readinessInformation.Filial.Name;
                    reportingPeriod = readinessInformation.ReportingPeriodDate.GetReportingPeriod(readinessInformation.Indicator.ReportingPeriodType);
                    state = readinessInformation.TechProcessState.GetDescription();
                }

                return $"{(filialName.ToLower() == "исполнительный аппарат" ? "АО \"СО ЕЭС\", Главный диспетчерский центр" : "Филиал " + filialName)}. Отчетный период: {reportingPeriod}. Статус: {state}";
            } catch (Exception ex) {
                return ex.Message;
            }
        }
        /// <summary>
        /// метод конвертации сущности справочника НСИ в модель для записи в Excel
        /// </summary>
        /// <typeparam name="IcpModel">Класс объектов справочника</typeparam>
        /// <param name="model">объект</param>
        /// <returns></returns>
        public virtual Entities.Views.ViewModels.DataTable.Icp.IcpDataGridRowResponseModel IcpModelToExcelTable<IcpModel>(IcpModel model) where IcpModel : class, TModel, Entities.Interfaces.IIcpEntity {
            return new Entities.Views.ViewModels.DataTable.Icp.IcpDataGridRowResponseModel {
                Id = model.Id,
                ChangedUser = model.ChangedUser.GetFullNameWithLogin(),
                DateChange = model.DateChange?.ToLocalTime().ToString(),
                IcpCode = model.IcpCode,
                Name = model.Name,
                ActualDate = model.ActualDate.ToShortDateString(),
                Status = model.IsDeleted ? "Удалено" : "Актуально"
            };
        }
        /// <summary>
        /// Экспорт объектов справочника НСИ в Excel документ
        /// </summary>
        /// <typeparam name="IcpModel">Класс объектов справочника</typeparam>
        /// <param name="documentName">наименование докумета</param>
        /// <param name="all">все (и удаленные)</param>
        /// <param name="actualDate">дата актуальности</param>
        /// <param name="ModelToExcelTable">метод конвертации сущности в модель для записи в Excel</param>
        /// <param name="error">исключение</param>
        /// <param name="props">список экспортируемых свойств</param>
        /// <returns></returns>
        public byte[] IcpExcelExport<IcpModel>(string documentName, bool all, DateTime? actualDate, Func<IcpModel, Entities.Views.ViewModels.DataTable.Icp.IcpDataGridRowResponseModel> ModelToExcelTable, out Exception error, List<ExcelPropertyColumn> props = null, params Expression<Func<IcpModel, object>>[] includes) where IcpModel : class, TModel, Entities.Interfaces.IIcpEntity {
            error = null;
            try {
                var query = GetIcpData(all, actualDate, out error, includes);
                if (error != null)
                    throw error;
                GetIcpProps(all, ref props, out error);
                if (error != null)
                    throw error;

                var icps = query.OrderBy(x => x.Name).Select(ModelToExcelTable).Select((x, i) => { x.Num = ++i; return x; });
                var excelPackage = ExcelPackageHelper.IcpImportToExel(icps, props, documentName, out error);

                return excelPackage;
            } catch (Exception exc) {
                error = exc;
                return null;
            }
        }
        /// <summary>
        /// Экспорт объектов справочника НСИ в Excel документ
        /// </summary>
        /// <typeparam name="IcpModel">Класс объектов справочника</typeparam>
        /// <param name="documentName">наименование докумета</param>
        /// <param name="all">все (и удаленные)</param>
        /// <param name="actualDate">дата актуальности</param>
        /// <param name="ModelToExcelTable">метод конвертации сущности в модель для записи в Excel</param>
        /// <param name="error">исключение</param>
        /// <param name="props">список экспортируемых свойств</param>
        /// <param name="includes">список включений</param>
        /// <returns></returns>
        public byte[] IcpExcelExport<IcpModel, IcpTableModel>(string documentName, bool all, DateTime? actualDate, Func<IcpModel, IcpTableModel> ModelToExcelTable, out Exception error, List<ExcelPropertyColumn> props = null, params Expression<Func<IcpModel, object>>[] includes) where IcpModel : class, TModel, Entities.Interfaces.IIcpEntity
                                                                    where IcpTableModel : class, INum {
            error = null;
            try {
                var query = GetIcpData(all, actualDate, out error, includes);
                if (error != null)
                    throw error;
                GetIcpProps(all, ref props, out error);
                if (error != null)
                    throw error;

                var icps = query.OrderBy(x => x.Name).Select(ModelToExcelTable).Select((x, i) => { x.Num = ++i; return x; });
                var excelPackage = ExcelPackageHelper.IcpImportToExel(icps, props, documentName, out error);

                return excelPackage;
            } catch (Exception exc) {
                error = exc;
                return null;
            }
        }
        /// <summary>
        /// Получение сущьностей НСИ справочника
        /// </summary>
        /// <typeparam name="IcpModel"></typeparam>
        /// <param name="all">все (и удаленные)</param>
        /// <param name="actualDate">дата актуальности</param>
        /// <param name="exc">исключение</param>
        /// <param name="includes">список включений</param>
        private IQueryable<IcpModel> GetIcpData<IcpModel>(bool all, DateTime? actualDate, out Exception exc, params Expression<Func<IcpModel, object>>[] includes) where IcpModel : class, TModel, Entities.Interfaces.IIcpEntity {
            try {
                exc = null;
                if (includes == null || includes.Length == 0)
                    includes = new Expression<Func<IcpModel, object>>[] { x => x.ChangedUser };
                else
                    includes = new Expression<Func<IcpModel, object>>[] { x => x.ChangedUser }.Concat(includes).ToArray();

                var query = GetAll(all ? DeletedSearchType.All : DeletedSearchType.Actual, true, includes);
                if (all == false)
                    query = query.Where(x => x.ActualDate <= actualDate);

                return query;
            } catch (Exception ex) {
                exc = ex;
                return null;
            }
        }
        /// <summary>
        /// Получение свойств для экспорта объектов НСИ справочника в Excel
        /// </summary>
        /// <param name="all"></param>
        /// <param name="props"></param>
        /// <param name="exc"></param>
        private void GetIcpProps(bool all, ref List<ExcelPropertyColumn> props, out Exception exc) {
            try {
                exc = null;

                if (props == null) {
                    props = new List<ExcelPropertyColumn> {
                        new ExcelPropertyColumn("Наименование", "Name"),
                        new ExcelPropertyColumn("Актуальность", "ActualDate"),
                        new ExcelPropertyColumn("Код", "IcpCode"),
                        new ExcelPropertyColumn("Дата изменения", "DateChange"),
                        new ExcelPropertyColumn("Пользователь", "ChangedUser"),
                    };
                }
                if (all)
                    props.Add(new ExcelPropertyColumn("Статус", "Status"));
            } catch (Exception ex) {
                exc = ex;
            }
        }
        public BaseRepository() { }

        public BaseRepository(RmContext context) {
            _context = context;
        }
    }
}
