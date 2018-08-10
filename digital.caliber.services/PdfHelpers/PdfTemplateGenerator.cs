using System;
using System.Collections.Generic;
using System.Text;
using digital.caliber.model.ViewModels;
using digital.caliber.services.Extensions;

namespace digital.caliber.services.PdfHelpers
{
    public static class PdfTemplateGenerator
    {
        public static string GetHtmlString(ResultsMeasures data)
        {
            var sb = new StringBuilder();
            sb.Append("<html><head></head><body>");

            sb.Append($@"<div class='pdf'>
                        <div class='pdf-logo'></div>
                        <div class='header'>
                            <div class='item-row title'>INDICES DENTARIOS</div>
                            <div class='item-row'><span>Paciente: </span><span>{data.PatientName}</span></div>
                            <div class='item-row'><span>Numero de H.C.: </span><span>{data.HcNumber}</span></div>
                            <div class='item-row'><span>Fecha: </span><span>{data.DateMeasure.ToShortDateString()}</span></div>
                        </div>");

            sb.Append(GetBolton(data.BoltonTotal, data.BoltonPreviousRelation));
            sb.Append(GetDiscrepancy(data.DentalBoneDiscrepancy));
            sb.Append(GetMoyers(data.Moyers));
            sb.Append(GetPont(data.Pont));
            sb.Append(GetTanaka(data.Tanaka));

            sb.Append("</body></html>");
            return sb.ToString();
            
        }

        private static string GetDiscrepancy(DentalBoneDiscrepancy data)
        {
            return $@"<div class='grid - container'>
                        <div class='row grid-title'>
                            <div class='col'>DISCREPANCIA OSEO-DENTARIA</div>
                        </div>
                        <div class='row grid-row'>
                            <div class='col-sm-9'>Superior</div>
                            <div class='col-sm-3'>{data.Superior.ToStringDecimal()}</div>
                        </div>
                        <div class='row grid-row alt'>
                            <div class='col-sm-9'>Inferior</div>
                            <div class='col-sm-3'>{data.Inferior.ToStringDecimal()}</div>
                        </div>
                        <div class='row grid-row'>
                            <div class='col-sm-9'>Antero superio</div>
                            <div class='col-sm-3'>{data.SuperiorAntero.ToStringDecimal()}</div>
                        </div>
                        <div class='row grid-row alt'>
                            <div class='col-sm-9'>Antero inferior</div>
                            <div class='col-sm-3'>{data.InferiorAntero.ToStringDecimal()}</div>
                        </div>
                        <div class='row grid-row'>
                            <div class='col-sm-9'>Incisivos Inferiores</div>
                            <div class='col-sm-3'>{data.InferiorIncisives.ToStringDecimal()}</div>
                        </div>
                    </div>";
        }

        private static string GetMoyers(Moyers data)
        {
            return $@"<div class='grid - container'>
                        <div class='row grid-title'>
                            <div class='col'>MOYERS (predición discrepancia)</div>
                        </div>
                        <div class='row grid-row'>
                            <div class='col-sm-9'>Superior Derecho</div>
                            <div class='col-sm-3'>{data.RightSuperior.ToStringDecimal()}</div>
                        </div>
                        <div class='row grid-row alt'>
                            <div class='col-sm-9'>Superior Izquierdo</div>
                            <div class='col-sm-3'>{data.LeftSuperior.ToStringDecimal()}</div>
                        </div>
                        <div class='row grid-row'>
                            <div class='col-sm-9'>Inferior Derecho</div>
                            <div class='col-sm-3'>{data.RightInferior.ToStringDecimal()}</div>
                        </div>
                        <div class='row grid-row alt'>
                            <div class='col-sm-9'>Inferior Izquierdo</div>
                            <div class='col-sm-3'>{data.LeftInferior.ToStringDecimal()}</div>
                        </div>
                    </div>";
        }

        private static string GetTanaka(TanakaJohnston data)
        {
            return $@"<div class='grid - container'>
                        <div class='row grid-title'>
                            <div class='col'>TANAKA-JOHNSTON (C-Pm-Pm)</div>
                        </div>
                        <div class='row grid-row'>
                            <div class='col-sm-9'>Superior</div>
                            <div class='col-sm-3'>{data.Superior.ToStringDecimal()}</div>
                        </div>
                        <div class='row grid-row alt'>
                            <div class='col-sm-9'>Inferior</div>
                            <div class='col-sm-3'>{data.Inferior.ToStringDecimal()}</div>
                        </div>
                    </div>";
        }

        private static string GetBolton(BoltonTotal data, BoltonPreviousRelation prevData)
        {
            return $@"<div class='grid - container'>
                        <div class='row grid-title'>
                            <div class='col'>BOLTON</div>
                        </div>
                        <div class='row grid-row'>
                            <div class='col-sm-9'>Bolton Total - {(data.IsSuperiorExcess ? "Exceso superior" : "Exceso inferior")}</div>
                            <div class='col-sm-3'>{(data.IsSuperiorExcess ? data.SuperiorExcess.ToStringDecimal() : data.SuperiorExcess.ToStringDecimal())}</div>
                        </div>
                        <div class='row grid-row alt'>
                            <div class='col-sm-9'>Bolton Anterior - {(prevData.IsSuperiorExcess ? "Exceso superior" : "Exceso inferior")}</div>
                            <div class='col-sm-3'>{(prevData.IsSuperiorExcess ? prevData.SuperiorExcess.ToStringDecimal() : prevData.SuperiorExcess.ToStringDecimal())}</div>
                        </div>
                    </div>";
        }

        private static string GetPont(Pont data)
        {
            return $@"<div class='grid - container'>
                        <div class='row grid-title'>
                            <div class='col'>PONT</div>
                        </div>
                        <div class='row grid-row'>
                            <div class='col-sm-9'>14 a 24</div>
                            <div class='col-sm-3'>{data.Pont14To24.ToStringDecimal()}</div>
                        </div>
                        <div class='row grid-row alt'>
                            <div class='col-sm-9'>16 a 26</div>
                            <div class='col-sm-3'>{data.Pont16To26.ToStringDecimal()}</div>
                        </div>
                        <div class='row grid-row'>
                            <div class='col-sm-9'>Longitud de Arco</div>
                            <div class='col-sm-3'>{data.ArchLong.ToStringDecimal()}</div>
                        </div>
                    </div>";
        }
    }
}
