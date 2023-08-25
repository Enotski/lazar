using System.ComponentModel;

namespace Lazar.Domain.Core.Enums {
    /// <summary>
    /// Type of subsystem in system
    /// </summary>
    public enum SubSystemType {
        [Description("Users")]
        Users,
        [Description("Roles")]
        Roles,
        [Description("Logging")]
        Logging,
        [Description("Dsp")]
        Dsp
    }
}
