using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMK.Utils.Interfaces.Base {
    public interface IGroupTemporaryReference {

         Guid EquipmentId { get; set; }
         Guid PowerFacilityId { get; set; }

    }
}
