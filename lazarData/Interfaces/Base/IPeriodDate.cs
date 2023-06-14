using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMK.Utils.Interfaces.Base
{
    public interface IPeriodDate
    {
        DateTime to { get; set; }

        DateTime from { get; set; }
    }
}
