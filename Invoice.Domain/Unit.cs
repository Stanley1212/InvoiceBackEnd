using System.Collections.Generic;

namespace Invoice.Domain
{
    public class Unit:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Item> Items{ get; set; }
    }
}
