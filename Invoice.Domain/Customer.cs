﻿namespace Invoice.Domain
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
