using System.ComponentModel.DataAnnotations;
using ELearning_Platforms.Models.enums;
namespace ELearning_Platforms.Models
{
    public class Payment
    {
        [DataType("Money")]
        public int Amount { get; set; }
        public String Status { get; set; } = PayStatus.Pending.ToString();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid StudentID { get; set; }
        public Student Student { get; set; }
        public Guid CourseID { get; set; }
        public Course Course { get; set; }

    }
}
