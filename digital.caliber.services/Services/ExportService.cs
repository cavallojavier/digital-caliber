using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using digital.caliber.model.ViewModels;
using digital.caliber.services.Extensions;
using digital.caliber.services.Resources.Texts;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace digital.caliber.services.Services
{
    public class ExportService : IExportService
    {
        private readonly IMeasureService _measureService;
        private static readonly BaseColor darkBlue = new BaseColor(44, 93, 152);
        private static readonly BaseColor lightblue = new BaseColor(142, 180, 227);

        private static iTextSharp.text.Font fontTitle = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 20, BaseColor.White);
        private static iTextSharp.text.Font fontTableHeader = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18, BaseColor.White);
        private static iTextSharp.text.Font fontTableRow = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.Black);

        public ExportService(IMeasureService measureService)
        {
            _measureService = measureService;
        }

        public async Task<(byte[], string)> ExportToPdf(string userId, int id)
        {
            InitializeCultures();

            var dataResult = await _measureService.GetResult(userId, id);
            var fileName = $"Haenggi Results - {dataResult.PatientName}_{dataResult.DateMeasure.ToShortDateString()}.pdf";

            var doc = new Document(PageSize.A4);
            using (var pdfStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(doc, pdfStream);
                
                doc.AddCreationDate();
                doc.AddTitle(TextsResource.PdfTitle);
                doc.AddAuthor(TextsResource.PdfAuthor);
                
                doc.Open();
                
                SetTitle(doc);

                SetPatientData(doc, dataResult.PatientName, dataResult.HcNumber, dataResult.DateMeasure);

                SetBolton(doc, dataResult);

                SetPont(doc, dataResult);

                SetTanaka(doc, dataResult);

                SetDentalBoneDiscrepancy(doc, dataResult);

                SetMoyers(doc, dataResult);

                doc.NewPage();

                SetCefalometricDiscrepancy(doc, dataResult);

                SetTotalDiscrepancy(doc, dataResult);

                doc.SetMargins(10, 10, (float)1.5, (float)1.5);

                doc.Close();
                writer.Close();

                return (pdfStream.ToArray(), fileName);
            }
        }
        
        private static void SetPatientData(Document doc, string patientName, string hcNumber, DateTime dateOfMeasure)
        {
            var itemsToDisplay = new Dictionary<string, string>
            {
                {TextsResource.Patient_Name, patientName},
                {TextsResource.Patient_HcNumber, hcNumber},
                {TextsResource.Patient_Date, dateOfMeasure.ToShortDateString()}
            };

            SetTable(doc, TextsResource.Patient_Title, itemsToDisplay);
        }

        private static void SetDentalBoneDiscrepancy(Document doc, ResultsMeasures results)
        {
            var itemsToDisplay = new Dictionary<string, string>
            {
                {TextsResource.Higher, results.DentalBoneDiscrepancy.Superior.ToStringUnit()},
                {TextsResource.Lower, results.DentalBoneDiscrepancy.Inferior.ToStringUnit()},
                {TextsResource.DentalBone_HigherAntero, results.DentalBoneDiscrepancy.SuperiorAntero.ToStringUnit()},
                {TextsResource.DentalBone_LowerAntero, results.DentalBoneDiscrepancy.InferiorAntero.ToStringUnit()},
                {
                    TextsResource.DentalBone_LowerIncisives,
                    results.DentalBoneDiscrepancy.InferiorIncisives.ToStringUnit()
                }
            };


            SetTable(doc, TextsResource.DentalBone_Title, itemsToDisplay);
        }

        private static void SetMoyers(Document doc, ResultsMeasures results)
        {
            var itemsToDisplay = new Dictionary<string, string>
            {
                {TextsResource.HigherRigth, results.Moyers.RightSuperior.ToStringUnit()},
                {TextsResource.HigherLeft, results.Moyers.LeftSuperior.ToStringUnit()},
                {TextsResource.LowerRigth, results.Moyers.RightInferior.ToStringUnit()},
                {TextsResource.LowerLeft, results.Moyers.LeftInferior.ToStringUnit()}
            };


            SetTable(doc, TextsResource.Moyers_Title, itemsToDisplay);
        }

        private static void SetTanaka(Document doc, ResultsMeasures results)
        {
            var itemsToDisplay = new Dictionary<string, string>
            {
                {TextsResource.Higher, results.Tanaka.Superior.ToStringUnit()},
                {TextsResource.Lower, results.Tanaka.Inferior.ToStringUnit()}
            };


            SetTable(doc, TextsResource.Tanaka_Title, itemsToDisplay);
        }

        private static void SetBolton(Document doc, ResultsMeasures results)
        {
            var itemsToDisplay = new Dictionary<string, string>
            {
                {
                    string.Format("{0} - {1}", TextsResource.Bolton_Total,
                        GetBoltonExcessLabel(results.BoltonTotal.IsSuperiorExcess)),
                    results.BoltonTotal.IsSuperiorExcess
                        ? results.BoltonTotal.SuperiorExcess.ToStringUnit()
                        : results.BoltonTotal.InferiorExcess.ToStringUnit()
                },
                {
                    string.Format("{0} - {1}", TextsResource.Bolton_Antero,
                        GetBoltonExcessLabel(results.BoltonPreviousRelation.IsSuperiorExcess)),
                    results.BoltonTotal.IsSuperiorExcess
                        ? results.BoltonPreviousRelation.SuperiorExcess.ToStringUnit()
                        : results.BoltonPreviousRelation.InferiorExcess.ToStringUnit()
                }
            };


            SetTable(doc, TextsResource.Bolton_Title, itemsToDisplay);
        }

        private static void SetPont(Document doc, ResultsMeasures results)
        {
            var itemsToDisplay = new Dictionary<string, string>
            {
                {TextsResource.Pont_14To24, results.Pont.Pont14To24.ToStringUnit()},
                {TextsResource.Pont_16To26, results.Pont.Pont16To26.ToStringUnit()},
                {TextsResource.Pont_LongArch, results.Pont.ArchLong.ToStringUnit()}
            };


            SetTable(doc, TextsResource.Pont_Title, itemsToDisplay);
        }

        private static void SetCefalometricDiscrepancy(Document doc, ResultsMeasures results)
        {
            var itemsToDisplay = new Dictionary<string, string>
            {
                {TextsResource.Higher, results.CefalometricDiscrepancy.Superior.ToStringWithText(" +/- 2 (mm)")},
                {TextsResource.Lower, results.CefalometricDiscrepancy.Inferior.ToStringWithText(" +/- 2 (mm)")},
            };

            SetTable(doc, TextsResource.CefaloDiscrepancy_Title, itemsToDisplay);
        }

        private static void SetTotalDiscrepancy(Document doc, ResultsMeasures results)
        {
            var itemsToDisplay = new Dictionary<string, string>
            {
                {TextsResource.Higher, results.TotalDiscrepancy.Superior.ToStringWithText(" +/- 4 (mm)")},
                {TextsResource.Lower, results.TotalDiscrepancy.Inferior.ToStringWithText(" +/- 4 (mm)")},
            };

            SetTable(doc, TextsResource.TotalDiscrepancy_Title, itemsToDisplay);
        }

        private static void SetTitle(Document doc)
        {
            // Set Logo
            //System.Drawing.Image bitmap = (System.Drawing.Image)ImagesResources.ResourceManager.GetObject("Logo_Inverso_black");
            //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bitmap, BaseColor.White);
            //image.ScalePercent(26f);
            //image.Alignment = Element.ALIGN_RIGHT;
            
            //doc.Add(image);

            // Set Title
            var cell = new PdfPCell(new Phrase(TextsResource.Title, fontTitle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = darkBlue,
                FixedHeight = 30f,
                BorderWidth = (float)0,
                Padding = (float)5,
            };

            var table = new PdfPTable(1) {WidthPercentage = 100};
            table.AddCell(cell);
            doc.Add(table);

            // Empty rows after title
            doc.Add(new Paragraph(" "));
        }

        private static void SetTable(Document doc, string title, Dictionary<string, string> rowItems)
        {
            var table = new iTextSharp.text.pdf.PdfPTable(2) {WidthPercentage = 100};

            float[] widths = new float[] { 2f, 1f };
            table.SetWidths(widths);

            // table title
            var titleCell = new PdfPCell(new Phrase(title, fontTableHeader))
            {
                Colspan = 2,
                MinimumHeight = 28f,
                BackgroundColor = lightblue,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            table.AddCell(titleCell);
            
            // table rows
            var itemsCount = 0;

            foreach (var item in rowItems)
            {
                var color = itemsCount % 2 == 0
                        ? BaseColor.White : lightblue;

                SetRowCell(table, item.Key, Element.ALIGN_LEFT, color);

                SetRowCell(table, string.IsNullOrEmpty(item.Value) ? "-" : item.Value, Element.ALIGN_RIGHT, color);

                itemsCount++;
            }
          
            doc.Add(table);

            // Empty rows after title
            doc.Add(new Paragraph(" "));
        }

        private static void SetRowCell(PdfPTable table, string text, int alignment, BaseColor backgroundColor)
        {
            var rowCell = new PdfPCell(new Phrase(text, fontTableRow))
            {
                MinimumHeight = 20f,
                BackgroundColor = backgroundColor,
                HorizontalAlignment = alignment,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            
            table.AddCell(rowCell);
        }

        private static string GetBoltonExcessLabel(bool isSuperior)
        {
            return isSuperior ? TextsResource.HigherExcess : TextsResource.LowerExcess;
        }

        private static void InitializeCultures()
        {
            Thread.CurrentThread.CurrentUICulture =
                Thread.CurrentThread.CurrentCulture.Name.Contains("en")
                    ? Thread.CurrentThread.CurrentUICulture = new CultureInfo("en")
                    : Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
        }
    }
}
