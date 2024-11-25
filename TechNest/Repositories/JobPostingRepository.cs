
using TechNest.Models;
namespace TechNest.Repositories
{
    public class JobPostingRepository : IRepository<JobPosting>
    {
        public Task AddAsync(JobPosting entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobPosting>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<JobPosting> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(JobPosting entity)
        {
            throw new NotImplementedException();
        }
    }
}
