using Lazar.Domain.Interfaces.Repositories.Common;
using Lazar.Srevices.Iterfaces.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazar.Services.Administration
{
    public class UsersService : IUsersService
    {
        private readonly IRepositoryManager _repositoryManager;

        public UsersService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
    }
}
