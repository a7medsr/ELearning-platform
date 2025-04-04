namespace ELearning_Platforms.Models
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public string Answer { get; set; }
        public int Points {  get; set; }
    }
}
