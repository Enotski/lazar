using Lazar.Services.Contracts.EventLogs;
using Lazar.Services.Contracts.Request.DataGrid.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Base;

namespace Lazar.Srevices.Iterfaces.EventLog {
    public interface IEventLogService : ILogService {
        /// <summary>
        /// Возвращает список записей журнала событий
        /// </summary>
        /// <param name="options">Параметры фильтрации и поиска</param>
        /// <returns></returns>
        Task<DataTableDto<SystemLogDto>> GetRecordsAsync(DataGridRequestDto options);
    }
}
