namespace Invoice.Domain
{
    public class Customer:BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool Activo { get; set; }
    }
}
