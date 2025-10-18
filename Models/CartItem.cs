using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Models
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string SessionId { get; set; } = string.Empty;

        [Required]
        public Guid ProductId { get; set; }

        public int Size { get; set; }

        [Required]
        [StringLength(50)]
        public string Color { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Product Product { get; set; } = null!;
    }
}