using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice
{
    public class PaginationInfo
    {
        public int pageSize { get; set; } = 100;
        public int currentPage { get; set; } = 1;
    }
}
