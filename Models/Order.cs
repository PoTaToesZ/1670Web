using System.ComponentModel.DataAnnotations;

namespace FPTBookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}
