using System.ComponentModel.DataAnnotations;

namespace ELearning_Platforms.Models
{
    public class CourseEnrollment
    {
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        [DataType("percentage")]
        public float Progress { get; set; } = 0;
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
