using digital.caliber.model.Models;
using digital.caliber.model.ViewModels;

namespace digital.caliber.services.Calculators
{
    public static class DentalBoneDiscrepancyCalculator
    {
        public static DentalBoneDiscrepancy GetResult(MeasuresMouthViewModel mouseMessure, MeasuresTeethsViewModel theethMessure)
        {
            var bonesSpaces = BonesSpaces.GetBonesCalculation(mouseMessure);
            var theeths = TheethsSum.GetResults(theethMessure);

            var dentalDiscrepancy =
                new DentalBoneDiscrepancy
                {
                    Superior = bonesSpaces.PerineoSuperiorArch - theeths.SumSuperiorTen,
                    Inferior = bonesSpaces.PerineoInferiorArch - theeths.SumInferiorTen,
                    SuperiorAntero = bonesSpaces.Bones13To23 - theeths.SumSuperiorSix,
                    InferiorAntero = bonesSpaces.Bones33To43 - theeths.SumInferiorSix,
                    InferiorIncisives = bonesSpaces.InferiorBonesIntercanine - theeths.SumInferiorFour
                };

            return dentalDiscrepancy;
        }
    }
}
