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
      
    }
}
