using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core.Dtos
{
    public class CustomerUpdateDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
