using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineJudge.Models
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Problem")]
        public int ProblemId { get; set; }

        [ForeignKey("Contest")]
        public int ContestId {get; set;}
        public bool IsInContestTime { get; set; }
        public string Code { get; set; }
        public string Vredict { get; set; } = "In queue";
        public DateTime SubmitTime { get; set; } = DateTime.Now;

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
    }
}
