using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core.Dtos
{
    public class CustomerCreateDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
