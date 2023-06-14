using lazarData.Interfaces;

namespace lazarData.Models.Administration
{
    internal class User : IDateChange, IUser
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateChange { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}