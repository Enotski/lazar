using System.ComponentModel;

namespace Lazar.Domain.Core.Enums
{
    public enum SubSystemType
    {
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
