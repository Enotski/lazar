using Lazar.Services.Contracts.Logging;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Base;

namespace Lazar.Srevices.Iterfaces.Logging {
    /// <summary>
    /// Service of system events logging
    /// </summary>
    public interface ISystemLogService : ILogService {
        /// <summary>
        /// System event records for presentation layer
        /// </summary>
        /// <param name="options">Search options</param>
        /// <returns></returns>
        Task<DataTableDto<SystemLogTableDto>> GetRecordsAsync(DataTableRequestDto options);
    }
}
