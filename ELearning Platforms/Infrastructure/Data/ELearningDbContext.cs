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
            modelBuilder.Entity<StudentTakenTest>().HasKey(e => new { e.StudentId , e.TestId });
            modelBuilder.Entity<StudentTakenTest>().HasOne(q => q.Student).WithMany(q => q.StudentTakenTests).HasForeignKey(q => q.StudentId);
            modelBuilder.Entity<StudentTakenTest>().HasOne(q => q.Test).WithMany(q => q.StudentTakenTests).HasForeignKey(q => q.TestId);
            
            modelBuilder.Entity<Payment>().HasKey(e => new { e.StudentId, e.CourseId });
            modelBuilder.Entity<Payment>().HasOne(q => q.Student).WithMany(q => q.Payments).HasForeignKey(q => q.StudentId);
            modelBuilder.Entity<Payment>().HasOne(q => q.Course).WithMany(q => q.Payments).HasForeignKey(q => q.CourseId);
            
            modelBuilder.Entity<CourseEnrollment>().HasKey(e => new { e.StudentId, e.CourseId });
            modelBuilder.Entity<CourseEnrollment>().HasOne(q => q.Student).WithMany(q => q.CourseEnrollments).HasForeignKey(q =>q.StudentId);
            modelBuilder.Entity<CourseEnrollment>().HasOne(q => q.Course).WithMany(q => q.CourseEnrollments).HasForeignKey(q => q.CourseId);
            
            modelBuilder.Entity<CourseFeedback>().HasKey(e => new { e.StudentId, e.CourseId });
            modelBuilder.Entity<CourseFeedback>().HasOne(q => q.Student).WithMany(q => q.CourseFeedbacks).HasForeignKey(q => q.StudentId);
            modelBuilder.Entity<CourseFeedback>().HasOne(q => q.Course).WithMany(q => q.CourseFeedbacks).HasForeignKey(q => q.CourseId);
            
            modelBuilder.Entity<Course>().HasOne(q => q.Teacher).WithMany(q => q.Courses).HasForeignKey(q => q.TeacherId);
            
            modelBuilder.Entity<Test>().HasOne(q => q.Course).WithMany(q => q.Tests).HasForeignKey(q => q.CourseId);
            
            modelBuilder.Entity<Lesson>().HasOne(q => q.Course).WithMany(q => q.Lessons).HasForeignKey(q => q.CourseId);
            
            modelBuilder.Entity<Question>().HasOne(q => q.Test).WithMany(q => q.Questions).HasForeignKey(q => q.TestId);
            
            modelBuilder.Entity<QuestionOption>().HasOne(q => q.Questionn).WithMany(q => q.QuestionOptions).HasForeignKey(q => q.QuestionId);
        }
        #region  
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Course> Courses { get; set; } 
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<CourseEnrollment> CourseEnrollment { get; set; }
        public virtual DbSet<CourseFeedback> CourseFeedback { get; set; }
        public virtual DbSet<StudentTakenTest> StudentTakenTest { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        #endregion
    }
}
