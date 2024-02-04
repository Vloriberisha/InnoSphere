using Bussiness_Logic_Layer.DTOs;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace Bussiness_Logic_Layer.Services
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginDTO request);
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;


        public AuthenticationService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> Login(LoginDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("Email does not exist");
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = _tokenService.GenerateToken(user, roles);

                return token;
            }
            else
            {
                throw new Exception("Invalid password");
            }

        }

    }
}
