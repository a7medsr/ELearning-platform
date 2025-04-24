namespace ELearning_Platforms.Models
{
    public class Teacher : BaseUser
    {
        public string Bio {  get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
