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

            var dentalDiscrepancy = new DentalBoneDiscrepancy();

            dentalDiscrepancy.Superior = bonesSpaces.PerineoSuperiorArch - theeths.SumSuperiorTen;
            dentalDiscrepancy.Inferior = bonesSpaces.PerineoInferiorArch - theeths.SumInferiorTen;

            dentalDiscrepancy.SuperiorAntero = bonesSpaces.Bones13To23 - theeths.SumSuperiorSix;
            dentalDiscrepancy.InferiorAntero = bonesSpaces.Bones33To43 - theeths.SumInferiorSix;

            dentalDiscrepancy.InferiorIncisives = bonesSpaces.InferiorBonesIntercanine - theeths.SumInferiorFour;

            return dentalDiscrepancy;
        }
    }
}
