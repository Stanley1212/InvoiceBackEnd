using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public string UserCreated { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string UserUpdated { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public bool Active { get; set; }
    }
}
