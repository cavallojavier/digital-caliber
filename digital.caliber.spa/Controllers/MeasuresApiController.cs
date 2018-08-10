using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using digital.caliber.model.Models;
using digital.caliber.model.ViewModels;
using digital.caliber.services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace digital.caliber.spa.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class MeasuresApiController : ControllerBase
    {
        private readonly IMeasureService _measureService;
        private readonly IExportService _exportService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MeasuresApiController(IMeasureService measureService, UserManager<ApplicationUser> userManager, IExportService exportService)
        {
            _measureService = measureService;
            _userManager = userManager;
            _exportService = exportService;
        }

        [HttpGet("getMeasures/{items}")]
        public async Task<List<MeasureResumeViewModel>> GetMeassures([FromRoute] int? items)
        {
            var loggedUser = await _userManager.GetUserAsync(User);

            var result = await _measureService.GetMeasures(loggedUser.Id, items);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<MeasureViewModel> GetMeassure([FromRoute] int id)
        {
            var loggedUser = await _userManager.GetUserAsync(User);

            var result = await _measureService.GetMeasure(loggedUser.Id, id);

            return result;
        }

        [HttpGet("GetResults/{id}"), ActionName("GetResults")]
        public async Task<ResultsMeasures> GetResults([FromRoute] int id)
        {
            var loggedUser = await _userManager.GetUserAsync(User);

            return await _measureService.GetResult(loggedUser.Id, id);
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
            var measureId = await _measureService.SaveMeasure(loggedUser.Id, viewModel);

            return Ok(measureId);
        }

        [HttpGet("exportResultsPdf/{id}"), ActionName("exportResultsPdf")]
        public async Task<FileContentResult> ExportResultToPdf([FromRoute] int id)
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            var (stream, fileName) = await _exportService.ExportToPdf(loggedUser.Id, id);

            //return File(stream, "application/pdf", fileName);
            return new FileContentResult(stream, new
                MediaTypeHeaderValue("application/octet"))
            {
                FileDownloadName = fileName
            };
            
        }
    }
}