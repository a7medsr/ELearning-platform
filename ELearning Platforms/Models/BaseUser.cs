using Microsoft.AspNetCore.Identity;

namespace ELearning_Platforms.Models
{
    public class BaseUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    }
}
