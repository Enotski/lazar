﻿using Lazar.Services.Contracts.Request.DataTable.Base;

namespace Lazar.Services.Contracts.Request.DataTable {
    public class RoleDataTableRequestDto : DataTableRequestDto
    {
        public Guid? SelectedUserId { get; set; }
    }
}