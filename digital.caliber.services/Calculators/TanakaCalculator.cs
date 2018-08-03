using digital.caliber.model.Models;
using digital.caliber.model.ViewModels;

namespace digital.caliber.services.Calculators
{
    public class TanakaCalculator
    {
        public static TanakaJohnston GetResult(MeasuresTeethsViewModel theethMessure)
        {
            var tanakaResult = new TanakaJohnston();

            var inferiorSum = TheethsSum.GetResults(theethMessure).SumInferiorFour;

            tanakaResult.Superior = (inferiorSum / 2) + 11;
            tanakaResult.Inferior = tanakaResult.Superior - (decimal)0.5;

            return tanakaResult;
        }
    }
}
