namespace Lazar.Services.Contracts.Administration {
    public class RoleTableDto : RoleDto {
        public int Num { get; set; }
        public RoleTableDto() : base() { }
        public RoleTableDto(Guid id, int num, string name, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
            Num = num;
        }
    }
}
