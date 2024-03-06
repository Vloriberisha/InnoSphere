using Bussiness_Logic_Layer.DTOs;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static DataAccessLayer.Constants.Enumerations;

namespace Bussiness_Logic_Layer.Services
{
    public interface IRegistrationService
    {
        Task Register(RegisterJobSeekerDTO request);
        Task Register(RegisterCompanyDTO request);
    }
    public class RegistrationService : IRegistrationService
    {

        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public RegistrationService(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task Register(RegisterCompanyDTO request)
        {
            if (request == null)
            {
                throw new Exception("User is not provided");
            }
            var users = await _userManager.FindByEmailAsync(request.Email);
            if (users != null)
            {
                throw new Exception("This email already exists");
            }
            var _user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.CompanyName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Location = request.Location
            };
            var result = await _userManager.CreateAsync(_user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
            await _userManager.AddToRoleAsync(_user, "Employer");

            byte[] photoBytes;
            using (var memoryStream = new MemoryStream())
            {
                await request.Photo.CopyToAsync(memoryStream);
                photoBytes = memoryStream.ToArray();
            }

            var employer = new Employer
            {
                UserId = _user.Id,
                Founded = request.Founded,
                Founder = request.Founder,
                Photo = Convert.ToBase64String(photoBytes),
                Description = request.Description,
                NoEmployees = request.NoEmployees,
                Website = request.Website
            };
            await _context.Employers.AddAsync(employer);
        }

        public async Task Register(RegisterJobSeekerDTO request)
        {
            if (request == null)
            {
                throw new Exception("User is not provided");
            }
            var users = await _userManager.FindByEmailAsync(request.Email);
            if (users != null)
            {
                throw new Exception("This email already exists");
            }
            var _user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.FirstName + request.LastName + Guid.NewGuid().ToString()[..5],
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Location = request.Location,
            };

            var result = await _userManager.CreateAsync(_user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
            await _userManager.AddToRoleAsync(_user, "JobSeeker");

            byte[] photoBytes;
            using (var memoryStream = new MemoryStream())
            {
                await request.Photo.CopyToAsync(memoryStream);
                photoBytes = memoryStream.ToArray();
            }

            var jobseeker = new JobSeeker
            {
                UserId = _user.Id,
                Photo = Convert.ToBase64String(photoBytes),
                Gender = request.Gender,
                Position = request.Position,
                DateOfBirth = request.DateOfBirth
            };

            await _context.JobSeekers.AddAsync(jobseeker);


        }
    }
}
