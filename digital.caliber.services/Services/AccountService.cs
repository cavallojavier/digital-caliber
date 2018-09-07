using System;
using System.Security.Claims;
using System.Threading.Tasks;
using digital.caliber.model.Models;
using digital.caliber.services.CustomLogger;
using Microsoft.AspNetCore.Identity;

namespace digital.caliber.services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICustomLogger _logger;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICustomLogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<SignInResult> AuthenticateAsync(string email, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetByNameAsync(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser> GetByUserAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("Email not found");

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            //Check if existing already
            var exists = await _userManager.FindByEmailAsync(email);

            if (exists != null)
                throw new Exception("Email already existing");

            var user = new ApplicationUser()
            {
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                RegisteredOn = DateTime.UtcNow
            };

            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, string firstName, string lastName, string email)
        {
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;

            return await _userManager.UpdateAsync(user);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdatePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result;
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
