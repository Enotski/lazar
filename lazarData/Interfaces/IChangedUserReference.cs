using System;

namespace lazarData.Interfaces
{
    public interface IChangedUserReference
    {
        Guid? ChangedUserId { get; set; }
    }
}
