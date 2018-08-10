using System;
using System.IO;
using System.Threading.Tasks;
using digital.caliber.services.PdfHelpers;

namespace digital.caliber.services.Services
{
    public class ExportService : IExportService
    {
        private readonly IMeasureService _measureService;
        //private readonly IConverter _converter;

        public ExportService(IMeasureService measureService
            /*, IConverter converter*/)
        {
            _measureService = measureService;
            //_converter = converter;
        }

        public async Task<(byte[], string)> ExportToPdf(string userId, int id)
        {
            throw new NotImplementedException();
            //var dataResult = await this._measureService.GetResult(userId, id);
            //var fileName = $"Haenggi Results - {dataResult.PatientName}_{dataResult.DateMeasure.ToShortDateString()}.pdf";

            //var globalSettings = new GlobalSettings
            //{
            //    ColorMode = ColorMode.Color,
            //    Orientation = Orientation.Portrait,
            //    PaperSize = PaperKind.A4,
            //    Margins = new MarginSettings { Top = 10 },
            //    DocumentTitle = $"Haenggi Results - {dataResult.PatientName}"
            //};

            //var objectSettings = new ObjectSettings
            //{
            //    PagesCount = true,
            //    HtmlContent = PdfTemplateGenerator.GetHtmlString(dataResult),
            //    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp/src", "styles.scss") },
            //    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
            //    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            //};

            //var pdf = new HtmlToPdfDocument()
            //{
            //    GlobalSettings = globalSettings,
            //    Objects = { objectSettings }
            //};

            //var file = _converter.Convert(pdf);
            ////return File(file, "application/pdf");
            //return (file, fileName);
        }

        public Task<(byte[], string)> ExportToExcel(string userId, int id)
        {
            throw new NotImplementedException();
        }
    }
}
