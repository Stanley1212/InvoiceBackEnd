using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core.Dtos
{
   public class BillDetailDto
    {
        public int BillID { get; set; }
        public int ItemID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}
