using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ELearning_Platforms.Models.DbContext
{
    public class ELearningDbContext : IdentityDbContext<BaseUser>
    {
        public ELearningDbContext(DbContextOptions<ELearningDbContext> options)
  : base(options)
        {

        }

        public ELearningDbContext() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentTakenTest>().HasKey(e => new { e.StudentID, e.TestID });
            modelBuilder.Entity<Payment>().HasKey(e => new { e.StudentID, e.CourseID });
            modelBuilder.Entity<CourseEnrollment>().HasKey(e => new { e.StudentID, e.CourseID });
            modelBuilder.Entity<CourseFeedback>().HasKey(e => new { e.StudentID, e.CourseID });

        }
        #region  
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Course> Courses { get; set; } 
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionOptions> QuestionOptions { get; set; }
        public virtual DbSet<CourseEnrollment> CourseEnrollment { get; set; }
        public virtual DbSet<CourseFeedback> CourseFeedback { get; set; }
        public virtual DbSet<StudentTakenTest> StudentTakenTest { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        #endregion
    }
}
