using OnlineJudge.Models;

namespace OnlineJudge.ViewModels
{
    public class ContestsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsRegistered { get; set; }
    }
}
