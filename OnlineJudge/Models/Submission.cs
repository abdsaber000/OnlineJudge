using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineJudge.Models
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }

        public int ProblemId { get; set; }
        public int ContestId {get; set;}
        public bool IsInContestTime { get; set; }
        public string Code { get; set; }
        public string Vredict { get; set; } = "In queue";

        [ForeignKey("ApplicatoinUser")]
        public string UserId { get; set; }
    }
}
