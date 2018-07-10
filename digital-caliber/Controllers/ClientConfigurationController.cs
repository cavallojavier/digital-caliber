using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using digital.caliber.ViewModels;

namespace digital.caliber.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ClientConfigurationController : ControllerBase
    {
        ClientConfiguration clientConfig;

        public ClientConfigurationController(IOptions<ClientConfiguration> clientConfigOptions, IConfiguration config)
        {
            clientConfig = clientConfigOptions?.Value;
            clientConfig.Environment = config["ASPNETCORE_ENVIRONMENT"];
            clientConfig.Admins = config["Admins"];
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(clientConfig);
        }
    }
}