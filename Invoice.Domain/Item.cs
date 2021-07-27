namespace Invoice.Domain
{
    public class Item:BaseEntity
    {
        public string Name { get; set; }
        public decimal Stock { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public int UnitID { get; set; }
        public Unit Unit { get; set; }
    }
}
