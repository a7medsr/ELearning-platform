using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Models
{
    public class StudentTakenTest
    {
        public int Score { get; set; }
        public DateTime TakenAt { get; set; } = DateTime.UtcNow;
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string TestId { get; set; }
        public Test Test { get; set; }

    }
}
