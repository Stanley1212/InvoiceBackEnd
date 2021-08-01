using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain
{
    public class Production: BaseEntity
    {
        public int ItemID { get; set; }
        public decimal Quantity { get; set; }
        public Item Item { get; set; }

        public ICollection<ProductionDetail> ProductionDetails { get; set; }
    }
}
