using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineJudge.Models;

namespace OnlineJudge.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Problem> Problem { get; set; } = default!;
        public DbSet<OnlineJudge.Models.Submission> Submission { get; set; } = default!;
        public DbSet<OnlineJudge.Models.Contest> Contest { get; set; } = default!;
        public DbSet<OnlineJudge.Models.ContestRegister> ContestRegister { get; set; } = default!;
    }
}
