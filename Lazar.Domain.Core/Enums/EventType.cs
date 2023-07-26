﻿using System.ComponentModel;

namespace Lazar.Domain.Core.Enums {
    public enum EventType
    {
        [Description("Read")]
        Read,
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
    }
}
