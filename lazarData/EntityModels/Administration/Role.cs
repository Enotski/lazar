using lazarData.EntityModels;
using lazarData.Interfaces;

namespace lazarData.Models.Administration
{
    public class Role: EntityBase, IName
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
