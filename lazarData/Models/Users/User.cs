using lazarData.Interfaces;

namespace lazarData.Models.Users
{
    internal class User : IKeyEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateChange { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}