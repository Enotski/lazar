using lazarData.Interfaces;
using lazarData.Models.EventLogs;

namespace lazarData.Models.Administration
{
    internal class User : IDateChange, IUser
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateChange { get; set; }
        public List<Role> Roles { get; set; }
        public List<Role> ChangedRoles { get; set; }
        public List<EventLog> ChangedEventLogs { get; set; }
    }
}