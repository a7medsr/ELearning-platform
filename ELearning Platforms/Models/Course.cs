using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning_Platforms.Models
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ThumbnailUrl { get; set; }
        [DataType("Money")]
        public int Price { get; set; }
        public Guid TeacherID { get; set; }
        public Teacher Teacher { get; set; }
    }
}
