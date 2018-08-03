using System.Collections.Generic;
using System.Threading.Tasks;
using digital.caliber.model.Models;
using digital.caliber.model.ViewModels;
using digital.caliber.services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace digital.caliber.spa.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class MeasuresApiController : ControllerBase
    {
        private readonly IMeasureService _measureService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MeasuresApiController(IMeasureService measureService, UserManager<ApplicationUser> userManager)
        {
            _measureService = measureService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<List<MeasureViewModel>> GetMeassures()
        {
            var loggedUser = await _userManager.GetUserAsync(User);

            var result = await _measureService.GetMeasures(loggedUser.Id);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<MeasureViewModel> GetMeassure([FromRoute] int id)
        {
            var loggedUser = await _userManager.GetUserAsync(User);

            var result = await _measureService.GetMeasure(loggedUser.Id, id);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            await _measureService.DeleteMeasure(loggedUser.Id, id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] MeasureViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var loggedUser = await _userManager.GetUserAsync(User);
            await _measureService.SaveMeasure(loggedUser.Id, viewModel);
            return Ok();
        }
    }
}