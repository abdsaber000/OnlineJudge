using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineJudge.Models
{
    public class ContestRegister
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ForeignKey("Contest")]
        public int ContestId { get; set; }
    }
}
