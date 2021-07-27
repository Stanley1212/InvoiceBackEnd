using System;
using System.Collections.Generic;

namespace Invoice.Domain
{
    public class Supplier:BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public ICollection<Bill> Bills { get; set; }
    }
}
