using System;
using System.Threading.Tasks;
using digital.caliber.model.Models;
using digital.caliber.model.ViewModels;

namespace digital.caliber.services.Calculators
{
    public class PontCalculator
    {
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="theethMessure">The theeth messure.</param>
        /// <returns></returns>
        public static async Task<Pont> GetResult(MeasuresTeethsViewModel theethMessure)
        {
            var pontResult = new Pont();

            var sumResult = TheethsSum.GetResults(theethMessure);
            pontResult.Pont14To24 = CalculationBase.RoundUpResult(sumResult.SumSuperiorFour * 100 / 84);
            pontResult.Pont16To26 = CalculationBase.RoundUpResult(sumResult.SumSuperiorFour * 100 / 65);
            
            pontResult.ArchLong = await CalculationTables.PontTable.FindPontValue(sumResult.SumSuperiorFour);

            return pontResult;
        }
    }
}
