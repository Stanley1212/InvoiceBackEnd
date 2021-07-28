using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain
{
    public class InvoiceHeader : BaseEntity
    {
        public int CustomerID { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
