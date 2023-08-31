namespace Lazar.Services.Contracts.Administration {
    public class UserTableDto : UserDto {
        public int Num { get; set; }
        public UserTableDto() : base() { }
        public UserTableDto(Guid id, int num, string name, string login, string password, string email, string roles, IEnumerable<Guid> roleIds, string changedBy, DateTime dateChange)
            : base(id, name, login, password, email, roles, roleIds, changedBy, dateChange) {
            Num = num;
        }
    }
}
