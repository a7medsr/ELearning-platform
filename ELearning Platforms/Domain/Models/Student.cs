namespace ELearning_Platforms.Models
{
    public class Student : BaseUser
    {
        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
        public ICollection<CourseFeedback> CourseFeedbacks { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<StudentTakenTest> StudentTakenTests { get; set; }
    }
}
