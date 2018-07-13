using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using digital.caliber.Auth;
using digital.caliber.model.Models;
using digital.caliber.services.CustomLogger;
using digital.caliber.services.Services;
using digital.caliber.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace digital.caliber.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ICustomLogger _logger;

        public AccountController(IAccountManager accountManager, 
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions, 
            ICustomLogger logger)
        {
            _accountManager = accountManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("OK!");
        }

        [AllowAnonymous]
        //[HttpPost("Register"), ActionName("Register")]
        public async Task<ActionResult> Register([FromBody] AccountRegisterViewModel registerVm)
        {
            return Ok("yeah");
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //try
            //{
            //    var identityResult = await _accountManager.Register(registerVm.FirstName, registerVm.LastName, registerVm.Email, registerVm.Password);
            //    if (!identityResult.Succeeded)
            //        throw new Exception("An error ocurred while creating account!");

            //    var user = await _accountManager.Authenticate(registerVm.Email, registerVm.Password);

            //    if (user == null)
            //        return Unauthorized();

            //    var identity = await GetClaimsIdentity(user);

            //    var response = new
            //    {
            //        uId = identity.Claims.Single(c => c.Type == "id").Value,
            //        firstName = user.FirstName,
            //        lastName = user.LastName,
            //        email = user.Email,
            //        formattedName = user.FirstName + " " + user.LastName,
            //        auth_token = await _jwtFactory.GenerateEncodedToken(user.Email, identity),
            //        expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            //    };

            //    var userData = JsonConvert.SerializeObject(response);

            //    return new OkObjectResult(userData);
            //}
            //catch (Exception ex)
            //{
            //    await _logger.Log(LogLevel.Error, "Account", ex, "Register");
            //    throw ex;
            //}
        }

        [AllowAnonymous]
        [HttpPost("Login"), ActionName("Login")]
        public async Task<ActionResult> Login([FromBody] AccountLoginViewModel loginVm)
        {
            try
            {
                var user = await _accountManager.Authenticate(loginVm.Email, loginVm.Password);

                if (user == null)
                    return Unauthorized();

                var identity = await GetClaimsIdentity(user);

                var response = new
                {
                    uId = identity.Claims.Single(c => c.Type == "id").Value,
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