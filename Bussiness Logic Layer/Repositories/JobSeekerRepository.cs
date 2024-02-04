using Bussiness_Logic_Layer.DTOs;
using Bussiness_Logic_Layer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Bussiness_Logic_Layer.Repositories
{
    public interface IJobSeekerRepository
    {
        Task<JobSeeker> GetJobSeeker(string id);
    }
    public class JobSeekerRepository : IJobSeekerRepository
    {
        private readonly AppDbContext _context;
        private readonly IIdentityRepository _identity;
        public JobSeekerRepository(AppDbContext context, IIdentityRepository identity)
        {
            _context = context;
            _identity = identity;
        }
        public async Task<JobSeeker> GetJobSeeker(string id)
        {
            if (id == null)
            {
                throw new Exception("Id is empty");
            }

            var user = await _identity.GetUserById(id);
            var jobSeeker = await _context.JobSeekers.FirstOrDefaultAsync(c => c.UserId == user.Id);

            jobSeeker.User = user;

            return jobSeeker;
        }

    }
}
