
namespace ELearning_Platforms.Models
{
    public class Test : BaseEntity
    {
        public string Title { get; set; }
        public int PassingScore { get; set; } = 50;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<StudentTakenTest> StudentTakenTests { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
