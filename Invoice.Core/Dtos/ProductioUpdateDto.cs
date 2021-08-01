using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core.Dtos
{
   public class ProductioUpdateDto
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public decimal Quantity { get; set; }
        public ICollection<ProductionDetailDto> ProductionDetails { get; set; }
    }
}
