using Bussiness_Logic_Layer.DTOs;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using static DataAccessLayer.Constants.Enumerations;

namespace Bussiness_Logic_Layer.Repositories
{
    public interface IJobApplicationRepository
    {
        Task AddJobApplication(JobApplicationDTO request);
        Task<List<JobApplication>> GetJobSeekerApplications(string id);
        Task RemoveApplication(string id);
        Task<bool> HasApplied(string userId, string jobId);
        Task<List<JobApplication>> GetJobApplications(string id);
        Task UpdateJobApplication(string id, string status);
    }
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly AppDbContext _context;
        private readonly IIdentityRepository _identity;
        public JobApplicationRepository(AppDbContext context, IIdentityRepository identity)
        {
            _context = context;
            _identity = identity;
        }
        public async Task<List<JobApplication>> GetJobSeekerApplications(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception("Id was empty");

            var applications = await _context.JobApplications.Where(j => j.JobSeekerId == id).ToListAsync();
            foreach (var application in applications)
            {
                var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == application.JobId);
                var user = await _identity.GetUserById(job.CompanyId);
                var employer = await _context.Employers.FirstOrDefaultAsync(e => e.UserId == user.Id);


                application.Job = job;
                application.Job.CompanyName = user.UserName;
                application.Job.CompanyPhoto = employer.Photo;
            }

            return applications;
        }

        public async Task<bool> HasApplied(string userId, string jobId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(jobId))
                throw new Exception("Data provided was empty");

            var application = await _context.JobApplications.FirstOrDefaultAsync(j => j.JobSeekerId == userId && j.JobId == jobId);

            return application != null;
        }
        public async Task<List<JobApplication>> GetJobApplications(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception("Id was empty");

            var applications = await _context.JobApplications.Where(j => j.JobId == id).ToListAsync();

            foreach (var application in applications)
            {
                var user = await _identity.GetUserById(application.JobSeekerId);
                var JobSeeker = await _context.JobSeekers.FirstOrDefaultAsync(e => e.UserId == user.Id);

                application.JobSeeker = JobSeeker;
            }
            return applications;
        }



        public async Task AddJobApplication(JobApplicationDTO request)
        {
            if (request == null)
                throw new Exception("Request was empty");

            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == request.JobId);

            if (job == null)
            {
                throw new Exception("Couldnt find job!");
            }

            var application = new JobApplication
            {
                JobSeekerId = request.JobSeekerId,
                JobId = request.JobId,
                Status = Enum.GetName(JobApplicationStatus.Submitted),
                DateSubmitted = DateTime.UtcNow.ToShortDateString()
            };

            await _context.JobApplications.AddAsync(application);
            await _context.SaveChangesAsync();

           
        }


        public async Task UpdateJobApplication(string id, string status)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(status))
                throw new Exception("Request data was empty");

            var application = await _context.JobApplications.FirstOrDefaultAsync(j => j.Id == id);

            if (application == null)
                throw new Exception("Application does not exist");

            application.Status = status;
            _context.Update(application);
            await _context.SaveChangesAsync();

          
        }


        public async Task RemoveApplication(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception("Id was empty");

            var application = await _context.JobApplications.FirstOrDefaultAsync(j => j.Id == id);

            if (application == null)
            {
                throw new Exception("Couldnt find application");
            }

            _context.JobApplications.Remove(application);
            await _context.SaveChangesAsync();


        }
    }
}
