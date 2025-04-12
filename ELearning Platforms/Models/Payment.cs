using System.ComponentModel.DataAnnotations;
using ELearning_Platforms.Models.enums;
namespace ELearning_Platforms.Models
{
    public class Payment: BaseEntity
    {
        [DataType("Money")]
        public int Amount { get; set; }
        public String Status { get; set; } = PayStatus.Pending.ToString();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;




    }
}
