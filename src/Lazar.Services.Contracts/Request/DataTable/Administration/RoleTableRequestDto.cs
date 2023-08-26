using Lazar.Services.Contracts.Request.DataTable.Base;

namespace Lazar.Services.Contracts.Administration {
    public class RoleTableRequestDto : DataTableRequestDto {
        public Guid? SelectedUserId { get; set; }
    }
}
