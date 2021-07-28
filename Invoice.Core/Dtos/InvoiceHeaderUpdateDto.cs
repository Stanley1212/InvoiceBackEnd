﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core.Dtos
{
    public class InvoiceHeaderUpdateDto
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<InvoiceDetailCreateDto> InvoiceDetails { get; set; }
    }
}
