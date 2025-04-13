using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Models
{
    public class StudentTakenTest
    {
        public int Score { get; set; }
        public DateTime TakenAt { get; set; } = DateTime.UtcNow;
        public Guid StudentID { get; set; }
        public Student Student { get; set; }
        public  Guid TestID { get; set; }
        public Test Test { get; set; }

    }
}
