using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.Models
{
    public class Problem
    {
        [Key]
        public int Id { get; set; }

        public int ContestId { get; set; }
        public string Title { get; set; }
        public string Statement { get; set; }
        public string SolutionCode { get; set; }
        public string InputTest {  get; set; }
        public string ExpectedOutput { get; set; }

    }
}
