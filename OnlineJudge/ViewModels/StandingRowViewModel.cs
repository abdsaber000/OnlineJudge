
using System.Text;

namespace OnlineJudge.ViewModels
{
    public class StandingRowViewModel
    {
        public string Handle { get; set; }
        public List<StandingCellViewModel> UserSubmitCount { get; set; }
        public bool IsInContestTime { get; set; }

       public static string ConvertToString(int number)
        {
            number--;
            StringBuilder result = new StringBuilder();
            do
            {
                result.Append((char)('A' + number % 26));
                number /= 26;
            }while(number > 0);
            return result.ToString();
        }
    }
}
