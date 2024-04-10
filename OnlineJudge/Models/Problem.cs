using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineJudge.Models
{
    public class Problem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Contest")]
        public int ContestId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Statement { get; set; }
        public string SolutionCode { get; set; }
        public string InputTest {  get; set; }
        public string ExpectedOutput { get; set; }

    }
}
