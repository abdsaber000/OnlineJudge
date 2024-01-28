using OnlineJudge.Models;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.ViewModels
{
    public class ProblemViewModel
    {
        public int Id { get; set; }
        public string Statement { get; set; }
        public string Title { get; set; }
        public string code { get; set; }

        public List<Submission> submissions { get; set; }
    }
}
