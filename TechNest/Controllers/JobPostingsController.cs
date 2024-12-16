using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechNest.Models;
using TechNest.Repositories;
using TechNest.ViewModels;

namespace TechNest.Controllers
{
    public class JobPostingsController : Controller
    {
        private readonly IRepository<JobPosting> _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public JobPostingsController(
            IRepository<JobPosting> repository,
            UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var jobPostings = await _repository.GetAllAsync();
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobPostingViewModel jobPostingVm)
        {

            if (ModelState.IsValid)
            {

                var jobPosting = new JobPosting
                {
                    Title = jobPostingVm.Title,
                    Description = jobPostingVm.Description,
                    Company = jobPostingVm.Company,
                    Location = jobPostingVm.Location,
                    UserId = _userManager.GetUserId(User)

                }; 
                await _repository.AddAsync(jobPosting);

                return RedirectToAction(nameof(Index));
            }

            return View(jobPostingVm);
        }
    }
}