namespace ELearning_Platforms.Models
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public int Points {  get; set; }
        public Guid TestID { get; set; }
        public Test Test { get; set; }
    }
}
