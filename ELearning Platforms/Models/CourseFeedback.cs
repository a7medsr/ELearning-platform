namespace ELearning_Platforms.Models
{
    public class CourseFeedback
    {
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public int Rating { get; set; } = 0;
        public string Comment { get; set; } = "";

    }
}
