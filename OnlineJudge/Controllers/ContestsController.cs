using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineJudge.Data;
using OnlineJudge.Data.Migrations;
using OnlineJudge.Models;
using OnlineJudge.ViewModels;

namespace OnlineJudge.Controllers
{
    public class ContestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contests
        public async Task<IActionResult> Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var contests = await _context.Contest.ToListAsync();
            List<ContestsViewModel> result = new List<ContestsViewModel>();

            foreach(Contest contest in contests)
            {
                result.Add(new ContestsViewModel
                {
                    Id=contest.Id,
                    Name=contest.Name,
                    StartDate=contest.StartDate,
                    EndDate=contest.EndDate,
                    IsRegistered=DidUserRegister(contest.Id)
                });
            }
            return View(result);
        }

        // GET: Contests/Details/5
        [Route("Contest/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contest = await _context.Contest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contest == null)
            {
                return NotFound();
            }

            return View(contest);
        }

        [Route("Contest/{ContestId}/Problems" , Name ="ProblemsInContest")]
        public async Task<IActionResult> ProblemsInContest(int ContestId)
        {
            var problems = await _context.Problem
                .Where(problem => problem.ContestId == ContestId).ToListAsync();
            return View(problems);
        }

        [Route("Contest/{ContestId}/Standing", Name = "Standing")]
        public async Task<IActionResult> Standing(int ContestId)
        {
            var submissions = await _context.Submission
                .Where(submission => submission.ContestId == ContestId)
                .ToListAsync();

            //var participants = await _context.ContestRegister
            //    .Where(register => register.ContestId == ContestId)
            //    .Select(register => register.UserId)
            //    .ToListAsync();

            var participants = await _context.Users
                .Select(participant => participant.Id).ToListAsync();

            var problems = await _context.Problem
                .Where(problem => problem.ContestId == ContestId)
                .ToListAsync();

            List<StandingRowViewModel> Standing =
                new List<StandingRowViewModel>();

            
            
            foreach(var participant in participants)
            {
                StandingRowViewModel ViewModelInContest = new StandingRowViewModel()
                {
                    Handle = _context.Users
                    .FirstOrDefault(user => user.Id == participant)
                    .Handle,
                    IsInContestTime = true
                    , UserSubmitCount = new List<StandingCellViewModel> ()
                    
                };

                StandingRowViewModel ViewModelInPractice = new StandingRowViewModel()
                {
                    Handle = _context.Users
                    .FirstOrDefault(user => user.Id == participant)
                    .Handle,
                    IsInContestTime = false
                    ,
                    UserSubmitCount = new List<StandingCellViewModel>()
                };
                bool didUserMadeSubmitInContest = false;
                bool didUserMadeSubmissionAfterContest = false;
                foreach (var problem in problems)
                {
                    bool isAcceptedInContest = false;
                    bool isAcceptedInPractice = false;
                    int counterOfSubmissionsInContest = 0;
                    int counterOfSubmisionsAfterContest = 0;
                    foreach (var submission in submissions)
                    {
                        if (submission.UserId == participant
                            && problem.Id == submission.ProblemId
                            )
                        {
                            if (submission.IsInContestTime)
                            {
                                counterOfSubmissionsInContest++;

                                if (submission.Vredict == "Accepted")
                                {
                                    isAcceptedInContest = true;
                                }
                            }
                            else
                            {
                                counterOfSubmisionsAfterContest++;
                                if (submission.Vredict == "Accepted")
                                {
                                    isAcceptedInPractice = true;
                                }
                            }
                            
                        }
                    }
                    if (counterOfSubmissionsInContest > 0)
                    {
                        didUserMadeSubmitInContest = true;
                    }
                    if(counterOfSubmisionsAfterContest > 0)
                    {
                        didUserMadeSubmissionAfterContest = true;
                    }

                    ViewModelInContest
                        .UserSubmitCount
                        .Add(new StandingCellViewModel { 
                            NumberOfSubmissions = counterOfSubmissionsInContest,
                            IsAccepted = isAcceptedInContest});
                    ViewModelInPractice
                        .UserSubmitCount
                        .Add(new StandingCellViewModel
                        {
                            NumberOfSubmissions = counterOfSubmisionsAfterContest,
                            IsAccepted = isAcceptedInPractice
                        });
                }

                if (didUserMadeSubmitInContest)
                {
                    Standing.Add(ViewModelInContest);
                }

                if (didUserMadeSubmissionAfterContest)
                {
                    Standing.Add(ViewModelInPractice);
                }
            }

            return View(Standing);
        }

        // GET: Contests/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Contest contest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contest);
        }

        // GET: Contests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contest = await _context.Contest.FindAsync(id);
            if (contest == null)
            {
                return NotFound();
            }
            return View(contest);
        }

        // POST: Contests/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Contest contest)
        {
            if (id != contest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContestExists(contest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contest);
        }

        // GET: Contests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contest = await _context.Contest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contest == null)
            {
                return NotFound();
            }

            return View(contest);
        }

        // POST: Contests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contest = await _context.Contest.FindAsync(id);
            if (contest != null)
            {
                _context.Contest.Remove(contest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Register(ContestsViewModel contest)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!DidUserRegister(contest.Id))
            {
                ContestRegister register = new ContestRegister
                {
                    ContestId = contest.Id,
                    UserId = UserId
                };
                _context.ContestRegister.Add(register);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index" , "Contests" );
        }

        [HttpPost]
        public async Task<IActionResult> UnRegister(ContestsViewModel contest)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (DidUserRegister(contest.Id))
            {
                var register = await _context.ContestRegister
                    .FirstOrDefaultAsync(register => 
                    register.UserId == UserId 
                    && register.ContestId == contest.Id);
                if(register != null)
                    _context.ContestRegister.Remove(register);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Contests");
        }

        [Route("Contest/{ContestId}/Submissions", Name = "Submissions")]

        // GET: Contest/id/Submissions
        public  IActionResult Submissions(int ContestId)
        {
            
            return View(SubmissionsController
                    .GetAllSubmissions(_context , ContestId));
        }


        private bool ContestExists(int id)
        {
            return _context.Contest.Any(e => e.Id == id);
        }

        private bool DidUserRegister(int contestId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _context.ContestRegister.Any(e => e.UserId == UserId
             && e.ContestId == contestId);
           
        }
    }
}
