using System;
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
        public static ResultsMeasures GetResult(MeasuresMouthViewModel mouthMessure, MeasuresTeethsViewModel theethMessure)
        {
            var results = new ResultsMeasures();

            try
            {
                results.DentalBoneDiscrepancy = DentalBoneDiscrepancyCalculator.GetResult(mouthMessure, theethMessure);

                results.Tanaka = TanakaCalculator.GetResult(theethMessure);

                results.Moyers = MoyersCalculator.GetResult(mouthMessure, theethMessure);

                results.Pont = PontCalculator.GetResult(theethMessure);

                results.BoltonTotal = BoltonCalculator.GetBoltonTotalResult(theethMessure);

                results.BoltonPreviousRelation = BoltonCalculator.GetBoltonPreviousResult(theethMessure);
            }
            catch (Exception ex)
            {
                throw new CalculationCustomException("Error en calculos.", ex);
            }

            return results;
        }
    }
}
