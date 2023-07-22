using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Utils
{
    public static class Guids
    {
        public static class Roles
        {
            public static readonly Guid Administrator = new("{DECCF253-3463-46D0-8EA3-C3C5FF051A15}");
            public static readonly Guid Moderator = new("{838809EB-D41F-42E1-84E3-A393BAF006AC}");
            public static readonly Guid User = new("{33B4FE6F-63CD-43EE-926D-6B6D27324B0D}");
        }
    }
}
