using System;
using System.Security.Claims;
using System.Threading.Tasks;
using digital.caliber.model.Models;
using Microsoft.AspNetCore.Identity;

namespace digital.caliber.services.Services
{
    public interface IAccountService : IDisposable
    {
        Task<ApplicationUser> GetByIdAsync(string id);

        Task<ApplicationUser> GetByEmailAsync(string email);

        Task<ApplicationUser> GetByNameAsync(string name);

        Task<ApplicationUser> GetByUserAsync(ClaimsPrincipal user);

        Task<IdentityResult> RegisterAsync(string firstName, string lastName, string email, string password);

        Task<SignInResult> AuthenticateAsync(string email, string password, bool rememberMe);

        Task LogoutAsync();

        Task<IdentityResult> UpdateAsync(ApplicationUser user, string firstName, string lastName, string email);

        Task<IdentityResult> UpdatePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);

        Task<bool> DeleteAsync(int id);

        Task<string> ForgotPasswordAsync(string email);
    }
}
