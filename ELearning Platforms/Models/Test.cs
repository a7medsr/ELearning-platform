
namespace ELearning_Platforms.Models
{
    public class Test : BaseEntity
    {
        public string Title { get; set; }
        public int PassingScore { get; set; } = 50;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    }
}
