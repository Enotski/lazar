using System.ComponentModel;

namespace lazarData.Enums
{
    public enum EventType
    {
        [Description("Create")]
        Create,
        [Description("Update")]
        Update,
        [Description("Delete")]
        Delete,
        [Description("Error")]
        Error,
        [Description("LogIn")]
        LogIn,
        [Description("LogOut")]
        LogOut,
        [Description("Registration")]
        Registration,
        [Description("DeleteUser")]
        DeleteUser
    }
}
