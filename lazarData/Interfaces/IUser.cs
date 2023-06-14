namespace lazarData.Interfaces
{
    public interface IUser : IKeyEntity
    {
        string Email { get; set; }
        string Login { get; set; }
    }
}
