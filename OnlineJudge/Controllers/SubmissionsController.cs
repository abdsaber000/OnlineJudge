using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.DataContracts;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineJudge.Data;
using OnlineJudge.Models;
using OnlineJudge.Services;
using OnlineJudge.ViewModels;

namespace OnlineJudge.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public static IEnumerable<SubmissionViewModel> GetAllSubmissions(
            ApplicationDbContext context , int ContestId  = 0)
        {
            var submissions = context.Submission.ToList();
            var problems = context.Problem.ToList();
            var users = context.Users.ToList();
            var result = from submission in submissions
                         join problem in problems on submission.ProblemId equals problem.Id
                         join user in users on submission.UserId equals user.Id
                         orderby submission.Id descending
                         select new SubmissionViewModel
                         {
                             Id = submission.Id,
                             ProblemId = submission.ProblemId,
                             ProblemTitle = problem.Title,
                             Vredict = submission.Vredict,
                             Handle = user.Handle,
                             ContestId = submission.ContestId
                         };
            if(ContestId != 0)
            {
                result = result
                    .Where(submission => submission.ContestId == ContestId);
            }
            return result;
        }

        // GET: Submissions
        public async Task<IActionResult> Index()
        {
            
            return View(GetAllSubmissions(_context));
        }

        // GET: Submissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submission == null)
            {
                return NotFound();
            }
            if(submission.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }
            return View(submission);
        }

        // GET: Submissions/Create
        public IActionResult Create()
        {
            return View();
        }

        private  void RunSubmission(ref Submission submission)
        {
            Problem problem = _context.Problem.Find(submission.ProblemId);
            if(problem == null)
            {

                return ;
            }

            string input = problem.InputTest;
            string output = problem.ExpectedOutput;
            string code = submission.Code;
            TestRunner testRunner = new TestRunner(code, input, output);
            testRunner.MakeDir();
            testRunner.MakeFiles();
            testRunner.WriteData();
            testRunner.RunCode();
            submission.Vredict = testRunner.Vredict;

        }


        // POST: Submissions/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Submission submission)
        {
            if (ModelState.IsValid)
            {
                submission.Vredict = "In queue";
                submission.ContestId = _context.Problem
                    .FirstOrDefault(problem => 
                        problem.Id == submission.ProblemId).ContestId;

                RunSubmission(ref submission);
                _context.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(submission);
        }

        public async Task<IActionResult> CreateCode(ProblemViewModel problem)
        {

            Submission submission = new Submission()
            {
                ProblemId = problem.Id,
                Code = problem.code,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            var _problem = _context.Problem
                .FirstOrDefault(p => p.Id == problem.Id);

            if(_problem != null)
            {
                var contest = _context.Contest
                    .FirstOrDefault(contest => contest.Id == _problem.ContestId);
                if(contest != null)
                    submission.IsInContestTime = contest.IsSubmitInContestTime();
            }

            submission.Vredict = "In queue";
            submission.ContestId = _context.Problem
                    .FirstOrDefault(problem =>
                        problem.Id == submission.ProblemId).ContestId;
            RunSubmission(ref submission);
            _context.Add(submission);
            await _context.SaveChangesAsync();
            /*
            return RedirectToAction("Details" 
                , "Problems" , new {id = problem.Id});*/
            return RedirectToAction("Index");
            
        }

        // GET: Submissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submission.FindAsync(id);
            if (submission == null)
            {
                return NotFound();
            }
            return View(submission);
        }

        // POST: Submissions/Edit/5
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Submission submission)
        {
            if (id != submission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmissionExists(submission.Id))
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
            return View(submission);
        }

        // GET: Submissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        // POST: Submissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submission = await _context.Submission.FindAsync(id);
            if (submission != null)
            {
                _context.Submission.Remove(submission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmissionExists(int id)
        {
            return _context.Submission.Any(e => e.Id == id);
        }
    }
}
