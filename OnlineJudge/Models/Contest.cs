using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.Models
{
    public class Contest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsRegisterValid()
        {
            return DateTime.Now < EndDate;
        }
    }
}
