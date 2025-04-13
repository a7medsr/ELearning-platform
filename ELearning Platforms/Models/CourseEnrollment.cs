using System.ComponentModel.DataAnnotations;

namespace ELearning_Platforms.Models
{
    public class CourseEnrollment
    {
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        [DataType("percentage")]
        public float Progress { get; set; } = 0;
    }
}
