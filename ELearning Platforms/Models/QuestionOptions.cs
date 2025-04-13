namespace ELearning_Platforms.Models
{
    public class QuestionOptions:BaseEntity
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionID { get; set; }
        public Question Question { get; set; }

    }
}
