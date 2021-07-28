using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core.Dtos
{
    public class BillUpdateDto
    {
        public int ID { get; set; }
        public int SupplierID { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public ICollection<BillDetailDto> BillDetails { get; set; }
    }
}
