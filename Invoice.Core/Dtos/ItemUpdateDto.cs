using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core.Dtos
{
    public class ItemUpdateDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Stock { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Type { get; set; }
        public int UnitID { get; set; }
    }
}
