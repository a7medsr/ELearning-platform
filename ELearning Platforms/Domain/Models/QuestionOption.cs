namespace ELearning_Platforms.Models
{
    public class QuestionOption:BaseEntity
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public string QuestionId { get; set; }
        public Question Questionn { get; set; }

    }
}
