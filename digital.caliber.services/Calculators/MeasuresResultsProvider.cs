using System;
using System.Threading.Tasks;
using digital.caliber.model.Models;
using digital.caliber.model.ViewModels;
using digital.caliber.services.Calculators;
using digital.caliber.services.CustomExceptions;

namespace digital.caliber.services
{
    public class MeasuresResultsProvider
    {
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns></returns>
        public static async Task<ResultsMeasures> GetResult(MeasuresMouthViewModel mouthMessure, MeasuresTeethsViewModel theethMessure)
        {
            var results = new ResultsMeasures();

            try
            {
                results.DentalBoneDiscrepancy = DentalBoneDiscrepancyCalculator.GetResult(mouthMessure, theethMessure);

                results.Tanaka = TanakaCalculator.GetResult(theethMessure);

                results.Moyers = await MoyersCalculator.GetResult(mouthMessure, theethMessure);

                results.Pont = await PontCalculator.GetResult(theethMessure);

                results.BoltonTotal = await BoltonCalculator.GetBoltonTotalResult(theethMessure);

                results.BoltonPreviousRelation = await BoltonCalculator.GetBoltonPreviousResult(theethMessure);

                results.CefalometricDiscrepancy =
                    await CefalometricDiscrepancyCalculator.GetResult(theethMessure.ProtIncisiveSup,
                        theethMessure.ProtIncisiveSup);

                results.TotalDiscrepancy =
                    await TotalDiscrepancyCalculator.GetResult(results.DentalBoneDiscrepancy,
                        results.CefalometricDiscrepancy);
            }
            catch (Exception ex)
            {
                throw new CalculationCustomException("Error en calculos.", ex);
            }

            return results;
        }
    }
}
