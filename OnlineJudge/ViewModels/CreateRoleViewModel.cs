using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
