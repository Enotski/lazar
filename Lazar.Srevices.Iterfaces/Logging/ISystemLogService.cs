﻿using Lazar.Services.Contracts.Logging;
using Lazar.Services.Contracts.Request.DataTable.Base;
using Lazar.Services.Contracts.Response.Models;
using Lazar.Srevices.Iterfaces.Base;

namespace Lazar.Srevices.Iterfaces.Logging {
    /// <summary>
    /// Service of system events logging
    /// </summary>
    public interface ILoggingervice : ILogService {
        /// <summary>
        /// System event records for presentation layer
        /// </summary>
        /// <param name="options">Параметры фильтрации и поиска</param>
        /// <returns></returns>
        Task<DataTableDto<SystemLogDto>> GetRecordsAsync(DataTableRequestDto options);
    }
}
