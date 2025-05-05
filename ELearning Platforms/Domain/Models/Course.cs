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
        public string? thumbnailUrl { get; set; }
        [DataType("Money")]
        public int Price { get; set; }

        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
        public ICollection<CourseFeedback> CourseFeedbacks { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Test> Tests { get; set; } 
        public ICollection<Lesson> Lessons { get; set; }

    }
}
