namespace ELearning_Platforms.Models
{
    public class Lesson : BaseEntity
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public int Duration { get; set; }
        public string? ContentURL { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        
    }
}
