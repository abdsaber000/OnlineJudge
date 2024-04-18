using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineJudge.Data;
using OnlineJudge.Models;
using OnlineJudge.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineJudge.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProblemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public static async Task<List<AllProblemsViewModel>> GetProblems(ApplicationDbContext _context, string UserId , int _contestId = 0)
        {
            List<AllProblemsViewModel> model = new List<AllProblemsViewModel>();
            foreach (var problem in _context.Problem)
            {
                var contest = await _context.Contest.FindAsync(problem.ContestId);
                bool canShowProblem = false;
                if (_contestId == 0 && contest == null)
                {
                    canShowProblem = true;
                }
                else if (_contestId == 0 && contest.EndDate < DateTime.Now )
                {
                    canShowProblem = true;
                }else if (_contestId != 0)
                {
                    canShowProblem = problem.ContestId == _contestId;
                }

                if (canShowProblem)
                {
                    int NumberOfAcceptedSubmissions = 0,
                        NumberOfTotalSubmissions = 0;
                    foreach (var submission in _context.Submission)
                    {
                        if (submission.ProblemId == problem.Id)
                        {
                            NumberOfTotalSubmissions++;
                            if (submission.Vredict == "Accepted")
                                NumberOfAcceptedSubmissions++;
                        }
                    }
                    bool canEditOrDelete = UserId == problem.AuthorId;
                    model.Add(new AllProblemsViewModel
                    {
                        Id = problem.Id,
                        Title = problem.Title,
                        CanEditOrDelete = canEditOrDelete,
                        NumberOfAcceptedSubmissions = NumberOfAcceptedSubmissions,
                        NumberOfTotalSubmissions = NumberOfTotalSubmissions
                    });
                }
            }
            return model;
        }
        // GET: Problems
        public async Task<IActionResult> Index()
        {
            
            return View(await GetProblems(_context, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        // GET: Problems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var problem = await _context.Problem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }
            
            if(problem.AuthorId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                var contest = await _context.Contest
                    .FirstOrDefaultAsync(m => m.Id == problem.ContestId);

                if (contest != null && !contest.CanSubmit())
                {
                    return NotFound();
                }
            }
            

            ProblemViewModel result = new ProblemViewModel()
            {
                Id = problem.Id,
                Title = problem.Title,
                Statement = problem.Statement
            };
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var submissionsByUser = _context.Submission
                .Where(submission => submission.UserId == UserId 
                    && submission.ProblemId == problem.Id).ToList();
            result.submissions = submissionsByUser;
            return View(result);
        }

        // GET: Problems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Problems/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Problem problem)
        {
            problem.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ModelState.Remove("AuthorId");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(problem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                } catch (Exception)
                {
                    ModelState.AddModelError("", "Invalid Contest Id");
                }
            }
            return View(problem);
        }

        // GET: Problems/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problem.FindAsync(id);
            if (problem == null)
            {
                return NotFound();
            }
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UserId != problem.AuthorId)
            {
                return NotFound();
            }
            return View(problem);
        }

        // POST: Problems/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id,  Problem problem)
        {
            if (id != problem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemExists(problem.Id))
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
            return View(problem);
        }

        // GET: Problems/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problem == null)
            {
                return NotFound();
            }

            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UserId != problem.AuthorId)
            {
                return NotFound();
            }

            return View(problem);
        }

        // POST: Problems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problem = await _context.Problem.FindAsync(id);
            if (problem != null)
            {
                _context.Problem.Remove(problem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemExists(int id)
        {
            return _context.Problem.Any(e => e.Id == id);
        }
    }
}
