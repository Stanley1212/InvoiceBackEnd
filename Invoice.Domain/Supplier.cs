using System;

namespace Invoice.Domain
{
    public class Supplier:BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool Activo { get; set; }
    }
}
