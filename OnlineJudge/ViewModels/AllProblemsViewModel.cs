using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.ViewModels
{
    public class AllProblemsViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public bool CanEditOrDelete { get; set; }
        public int NumberOfAcceptedSubmissions { get; set; }
        public int NumberOfTotalSubmissions { get; set; }
    }
}
