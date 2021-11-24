using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core.Dtos
{
    public class InvoiceHeaderCreateDto
    {
        public int CustomerID { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<InvoiceDetailCreateDto> InvoiceDetails { get; set; }
    }
}
