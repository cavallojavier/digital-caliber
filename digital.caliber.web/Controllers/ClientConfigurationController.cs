using System.Threading.Tasks;
using digital.caliber.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace digital.caliber.Controllers
{
    [Route("api/[controller]")]
    public class ClientConfigurationController : Controller
    {
        ClientConfiguration clientConfig;

        public ClientConfigurationController(IOptions<ClientConfiguration> clientConfigOptions, IConfiguration config)
        {
            clientConfig = clientConfigOptions?.Value;
            clientConfig.Environment = config["ASPNETCORE_ENVIRONMENT"];
            clientConfig.Admins = config["Admins"];
        }

        [HttpGet(Name = "Get"), ActionName("Get")]
        public IActionResult Get()
        {
            return new JsonResult(clientConfig);
        }
    }
}