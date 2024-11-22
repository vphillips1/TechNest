using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechNest.Models;

namespace TechNest.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<JobPosting> JobPostings { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


    }
}
