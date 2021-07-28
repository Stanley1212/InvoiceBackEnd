using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain
{
    public class Bill : BaseEntity
    {
        public int SupplierID { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<BillDetail> BillDetails { get; set; }
    }
}
