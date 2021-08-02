using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain
{
    public class ProductionDetail
    {
        public int ID { get; set; }
        public int ProductionID { get; set; }
        public int ItemID { get; set; }
        public decimal Quantity { get; set; }

        public Item Item { get; set; }
    }
}
