using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain
{
    public class Invoice
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTimeOffset Fecha { get; set; }
        public decimal Total { get; set; }
    }
}
