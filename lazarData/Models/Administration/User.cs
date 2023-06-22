using lazarData.Interfaces;
using lazarData.Models.EventLogs;

namespace lazarData.Models.Administration
{
    public class User : EntityBase, IUser
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
        public List<EventLog> ChangedEventLogs { get; set; }
    }
}