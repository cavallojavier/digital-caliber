using System.Threading.Tasks;
using digital.caliber.model.Models;
using digital.caliber.model.ViewModels;
using digital.caliber.services.CalculationTables;

namespace digital.caliber.services.Calculators
{
    public class BoltonCalculator
    {
        private const decimal BoltonTotalBreakPoint = (decimal)91.3;
        private const decimal BoltonPreviousBreakPoint = (decimal)77.2;

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="theethMessure">The theeth messure.</param>
        /// <returns></returns>
        public static async Task<BoltonTotal> GetBoltonTotalResult(MeasuresTeethsViewModel theethMessure)
        {
            var result = new BoltonTotal();

            var teethes = TheethsSum.GetResults(theethMessure);

            result.Maxilar12Pac = teethes.SumSuperiorTwelve;
            result.Mandibular12Pac = teethes.SumInferiorTwelve;

            result.Total = CalculationBase.RoundUpResult(result.Mandibular12Pac / result.Maxilar12Pac * 100);

            result.IsSuperiorExcess = result.Total < BoltonTotalBreakPoint;

            result.Maxilar12Ideal = await BoltonTable.FindBoltonTotalByMandibularValue(result.Mandibular12Pac);
            result.SuperiorExcess = result.Maxilar12Pac - result.Maxilar12Ideal;

            result.Mandibular12Ideal = await BoltonTable.FindBoltonTotalByMaxilarValue(result.Maxilar12Pac);
            result.InferiorExcess = result.Mandibular12Pac - result.Mandibular12Ideal;

            return result;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="theethMessure">The theeth messure.</param>
        /// <returns></returns>
        public static async Task<BoltonPreviousRelation> GetBoltonPreviousResult(MeasuresTeethsViewModel theethMessure)
        {
            var result = new BoltonPreviousRelation();

            var theeths = TheethsSum.GetResults(theethMessure);

            result.Maxilar6Pac = theeths.SumSuperiorSix;
            result.Mandibular6Pac = theeths.SumInferiorSix;

            result.Total = CalculationBase.RoundUpResult(result.Mandibular6Pac / result.Maxilar6Pac * 100);
            
            result.IsSuperiorExcess = result.Total < BoltonPreviousBreakPoint;

            result.Mandibular6Ideal = await BoltonTable.FindPreviousRelationBoltonByMaxilarValue(result.Maxilar6Pac);
            result.InferiorExcess = result.Mandibular6Pac - result.Mandibular6Ideal;

            result.Maxilar6Ideal = await BoltonTable.FindPreviousRelationBoltonByMandibularValue(result.Mandibular6Pac);
            result.SuperiorExcess = result.Maxilar6Pac - result.Maxilar6Ideal;

            return result;
        }
    }
}