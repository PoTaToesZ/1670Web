namespace FPTBookStore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
