using lazarData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Users
{
    internal class Role: IKeyEntity, IName
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateChange { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
