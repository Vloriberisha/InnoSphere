﻿
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bussiness_Logic_Layer.Repositories
{
    public interface IEmployerRepository
    {
        Task<Employer> GetEmployer(string id);
        Task<List<Employer>> GetEmployers();
    }
    public class EmployerRepository : IEmployerRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IIdentityRepository _identity;
        public EmployerRepository(UserManager<User> userManager, AppDbContext context, IIdentityRepository identity)
        {
            _userManager = userManager;
            _context = context;
            _identity = identity;
        }

        #region GET
        public async Task<Employer> GetEmployer(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Id is null");

            var user = await _identity.GetEmployerById(id);
            var employer = await _context.Employers.FirstOrDefaultAsync(e => e.UserId == user.Id);

            if (employer != null)
            {
                employer.User = user;

                var _jobs = await _context.Jobs.Where(j => j.CompanyId == id).ToListAsync();
                if (_jobs != null)
                {
                    employer.JobsPosted = _jobs;
                }

                return employer;
            }
            else
            {
                return new(); 
            }
        }


        public async Task<List<Employer>> GetEmployers()
        {
            var employers = await _userManager.GetUsersInRoleAsync("Employer");

            List<Employer> employersList = [];

            foreach (var user in employers)
            {
                var employer = await _context.Employers.FirstOrDefaultAsync(e => e.UserId == user.Id);

                if (employer != null)
                {
                    employer.User = user;

                    var _jobs = await _context.Jobs.Where(j => j.CompanyId == user.Id).ToListAsync();
                    if (_jobs != null)
                    {
                        employer.JobsPosted = _jobs;
                    }
                    employersList.Add(employer);
                }
            }

            return employersList;
        }
        #endregion
    }
}
