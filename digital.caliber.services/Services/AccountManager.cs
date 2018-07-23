using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using digital.caliber.model.Models;
using digital.caliber.services.CustomLogger;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace digital.caliber.services.Services
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICustomLogger _logger;

        public AccountManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICustomLogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<ApplicationUser> Authenticate(string email, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);

            if (!result.Succeeded)
                throw new Exception("Authentication failed");

            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetByName(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("Email not found");

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> Register(string firstName, string lastName, string email, string password)
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

        public Task<bool> Update(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
