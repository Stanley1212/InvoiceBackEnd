using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice
{
    public class ErrorResponseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
