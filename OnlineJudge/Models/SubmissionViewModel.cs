using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.Models
{
    public class SubmissionViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public string Vredict { get; set; } = "In queue";
        public string ProblemTitle { get; set; }
    }
}
