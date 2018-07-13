using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using digital.caliber.model.Models;
using Microsoft.AspNetCore.Identity;

namespace digital.caliber.services.Services
{
    public interface IAccountManager : IDisposable
    {
        Task<ApplicationUser> GetById(string id);

        Task<ApplicationUser> GetByEmail(string email);

        Task<ApplicationUser> GetByName(string name);

        Task<IdentityResult> Register(string firstName, string lastName, string email, string password);

        Task<ApplicationUser> Authenticate(string email, string password, bool rememberMe);

        Task<bool> Update(ApplicationUser user);

        Task<bool> Delete(int id);

        Task<string> ForgotPassword(string email);
    }
}
