﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechNest.Constants;
using TechNest.Models;
using TechNest.Repositories;
using TechNest.ViewModels;

namespace TechNest.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {


            if(User.IsInRole(Roles.Employer))
            {
                var alljobPostings = await _repository.GetAllAsync();

                var userId = _userManager.GetUserId(User);

                var filteredJobPostings = alljobPostings.Where(jp => jp.UserId == userId);

                return View(filteredJobPostings);
            }

            var jobPostings = await _repository.GetAllAsync();
            return View(jobPostings);
        }

        [Authorize(Roles = "Admin, Employer")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        [Authorize(Roles = "Admin, Employer")]
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


        [HttpDelete]
        [Authorize(Roles = "Admin, Employer")]
        public async Task<IActionResult> Delete(int id)
        {
            var jobPosting = await _repository.GetByIdAsync(id);

            if(jobPosting == null )
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if(User.IsInRole(Roles.Admin) == false && jobPosting.UserId != userId)
            {
                return Forbid();
            }

            await _repository.DeleteAsync(id);
            return Ok();
        }

    }
}