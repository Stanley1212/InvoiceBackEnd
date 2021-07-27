using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core
{
    public class PagedData<T>
        where T : class
    {
        public int currentPage { get; private set; }
        public int totalPages { get; private set; }
        public int pageSize { get; private set; }
        public int totalRecords { get; private set; }
        public bool hasPrevious => (currentPage > 1);
        public bool hasNext => (currentPage < totalPages);
        public IEnumerable<T> data { get; set; }

        public PagedData(IEnumerable<T> data, int maxRecords, int currentpage, int size)
        {
            this.data = data;
            this.totalRecords = maxRecords;
            this.pageSize = size;
            this.currentPage = currentpage;
            this.totalPages = (int)Math.Ceiling(maxRecords / (double)size);
        }
    }
}
