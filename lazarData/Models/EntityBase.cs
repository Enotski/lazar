using lazarData.Interfaces;

namespace lazarData.Models
{
    public class EntityBase: IKeyEntity, IDateChange
    {
        public Guid Id { get; set; }
        public DateTime DateChange { get; set; }
    }
}
