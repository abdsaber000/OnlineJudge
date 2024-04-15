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
        [ForeignKey("AuthorId")]
        [MaxLength(450)]
        public string AuthorId { get; set; }
       // public ApplicationUser ApplicationUser { get; set; }  
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
