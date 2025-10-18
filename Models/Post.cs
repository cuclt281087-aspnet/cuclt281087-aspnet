using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Models
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Excerpt { get; set; }

        [StringLength(500)]
        public string? FeaturedImage { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = "General"; // General, News, Guide, Promotion

        public string Tags { get; set; } = "[]"; // JSON array

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Draft"; // Draft, Published, Archived

        public Guid AuthorId { get; set; }

        public int ViewCount { get; set; } = 0;

        public bool IsFeatured { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? PublishedAt { get; set; }

        public virtual Admin Author { get; set; } = null!;
    }
}