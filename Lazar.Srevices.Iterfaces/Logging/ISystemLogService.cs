using Lazar.Services.Contracts.Logging;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Base;

namespace Lazar.Srevices.Iterfaces.Logging {
    public interface ILoggingervice : ILogService {
        /// <summary>
        /// Возвращает список записей журнала событий
        /// </summary>
        /// <param name="options">Параметры фильтрации и поиска</param>
        /// <returns></returns>
        Task<DataTableDto<SystemLogDto>> GetRecordsAsync(DataTableRequestDto options);
    }
}
