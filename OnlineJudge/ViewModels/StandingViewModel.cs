using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.ViewModels
{
    public class StandingViewModel
    {
        public int ContestId { get; set; }

        public List<StandingRowViewModel> Rows { get; set; }

    }
}
