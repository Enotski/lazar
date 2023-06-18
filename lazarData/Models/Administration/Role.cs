using lazarData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Administration
{
    public class Role: IKeyEntity, IName, IDateChange
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateChange { get; set; }
        public List<User> Users { get; set; }
    }
}
