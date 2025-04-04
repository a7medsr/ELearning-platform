using System.ComponentModel.DataAnnotations;

namespace ELearning_Platforms.Models
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ThumbnailUrl { get; set; }
        public int Price { get; set; }
    }
}
