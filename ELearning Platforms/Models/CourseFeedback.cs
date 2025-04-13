namespace ELearning_Platforms.Models
{
    public class CourseFeedback
    {
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public int Rating { get; set; } = 0;
        public string Comment { get; set; } = "";
        public Guid StudentID { get; set; }
        public Student Student { get; set; }
        public Guid CourseID { get; set; }
        public Course Course { get; set; }

    }
}
