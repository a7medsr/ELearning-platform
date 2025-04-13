namespace ELearning_Platforms.Models
{
    public class StudentTakenTest
    {
        public int Score { get; set; }
        public DateTime TakenAt { get; set; } = DateTime.UtcNow;
    }
}
