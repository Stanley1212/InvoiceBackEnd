using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain
{
    public class Bill : BaseEntity
    {
        public int SupplierID { get; set; }
        public decimal Total { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<BillDetail> BillDetails { get; set; }
    }
}
