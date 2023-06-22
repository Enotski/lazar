using lazarData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Administration
{
    public class Role: EntityBase, IName
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
