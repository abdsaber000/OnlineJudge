using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineJudge.Models
{
    public class Contest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("ApplicationUser")]
        public string AuthorId { get; set; }
        public bool CanSubmit()
        {
            return DateTime.Now >= StartDate;
        }

        public bool IsSubmitInContestTime()
        {
            return CanSubmit() && DateTime.Now < EndDate;
        }
    }
}
