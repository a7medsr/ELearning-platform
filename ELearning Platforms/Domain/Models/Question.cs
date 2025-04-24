namespace ELearning_Platforms.Models
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public int Points {  get; set; }
        public string TestId { get; set; }
        public Test Test { get; set; }

        public ICollection<QuestionOption> QuestionOptions { get; set; }
    }
}
