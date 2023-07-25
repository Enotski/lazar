using Lazar.Domain.Core.EntityModels.Base;

namespace Lazar.Domain.Core.Models.Administration {
    public class Role : NameEntity {
        public ICollection<User> Users { get; set; } = new List<User>();
        public Role() : base() { }
        public Role(Guid id, string name, string changedBy, DateTime dateChange)
            : base(id, name, changedBy, dateChange) {
        }
    }
}
