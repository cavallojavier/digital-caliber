using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using digital.caliber.model.Models;
using digital.caliber.services.CustomLogger;
using digital.caliber.services.Services;
using digital.caliber.spa.Auth;
using digital.caliber.spa.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace digital.caliber.spa.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ICustomLogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;

        public AccountController(IAccountManager accountManager, 
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions, 
            ICustomLogger logger)
        {
            _accountManager = accountManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _logger = logger;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [AllowAnonymous]
        [HttpPost("Register"), ActionName("Register")]
        public async Task<ActionResult> Register(AccountRegisterViewModel registerVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var identityResult = await _accountManager.Register(registerVm.FirstName, registerVm.LastName, registerVm.Email, registerVm.Password);
                if (!identityResult.Succeeded)
                {
                    var error = identityResult.Errors.Any() ? identityResult.Errors.FirstOrDefault().Description : "Error while registering user!";
                    return BadRequest(error);
                }

                var user = await _accountManager.Authenticate(registerVm.Email, registerVm.Password, false);

                if (user == null)
                    return Unauthorized();

                var identity = await GetClaimsIdentity(user);

                var response = new
                {
                    uId = identity.Claims.Single(c => c.Type == "id").Value,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    formattedName = user.FirstName + " " + user.LastName,
                    auth_token = await _jwtFactory.GenerateEncodedToken(user.Email, identity),
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
                };

                var userData = JsonConvert.SerializeObject(response);

                return new OkObjectResult(userData);
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, "Account", ex, "Register");
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login"), ActionName("Login")]
        public async Task<ActionResult> Login([FromBody] AccountLoginViewModel loginVm)
        {
            try
            {
                var user = await _accountManager.Authenticate(loginVm.Email, loginVm.Password, loginVm.RememberMe);

                if (user == null)
                    return Unauthorized();

                var identity = await GetClaimsIdentity(user);

                var response = new
                {
                    uId = identity.Claims.Single(c => c.Type == "id").Value,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    formattedName = user.FirstName + " " + user.LastName,
                    auth_token = await _jwtFactory.GenerateEncodedToken(user.Email, identity),
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
                };

                var userData = JsonConvert.SerializeObject(response);

                return new OkObjectResult(userData);
            }
            catch (Exception ex)
            {
                await _logger.Log(LogLevel.Error, "Account", ex, "Login");
                throw ex;
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("Logout"), ActionName("Logout")]
        public async Task<ActionResult> Logout()
        {
            await this._accountManager.Logout();

            return Ok();
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(ApplicationUser user)
        {
            // get the user to verifty
            if (user != null)
            {
                return await _jwtFactory.GenerateClaimsIdentity(user.Email, user.Id);
            }

            // User doesn't exist ... yet!
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}