namespace ELearning_Platforms.Application.DTOs.Courses
{
    public class AddCourseDTO
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? ThumbnailUrl { get; set; }
        public int Price { get; set; }

    }
}
