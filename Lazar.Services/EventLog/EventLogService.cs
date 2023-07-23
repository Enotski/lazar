using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Srevices.Iterfaces.EventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazar.Services.EventLog
{
    public class EventLogService : IEventLogService
    {
        private readonly IRepositoryManager _repositoryManager;

        public EventLogService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
    }
}
