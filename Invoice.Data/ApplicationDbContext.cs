using Invoice.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Invoice.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions op) : base(op)
        {

        }

        public DbSet<InvoiceHeader> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<ProductionDetail> ProductionDetails { get; set; }
    }
}
