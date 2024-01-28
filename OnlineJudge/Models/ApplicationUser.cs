using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public string Handle {  get; set; }
    }
}
