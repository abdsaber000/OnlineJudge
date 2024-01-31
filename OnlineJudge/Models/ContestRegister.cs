using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.Models
{
    public class ContestRegister
    {
        [Key]
        public int Id { get; set; } 
        public string UserId { get; set; } 
        public int ContestId { get; set; }
    }
}
