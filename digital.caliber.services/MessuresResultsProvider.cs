﻿using System;
using digital.caliber.model.Models;
using digital.caliber.model.ViewModels;
using digital.caliber.services.Calculators;
using digital.caliber.services.CustomExceptions;

namespace digital.caliber.services
{
    public class MessuresResultsProvider
    {
        private readonly MeasuresMouthViewModel mouthMessure;
        private readonly MeasuresTeethsViewModel theethMessure;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessuresResultsProvider"/> class.
        /// </summary>
        /// <param name="mouthMessure">The mouse messure.</param>
        /// <param name="theethMessure">The theeth messure.</param>
        public MessuresResultsProvider(MeasuresMouthViewModel mouthMessure, MeasuresTeethsViewModel theethMessure)
        {
            this.mouthMessure = mouthMessure;
            this.theethMessure = theethMessure;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns></returns>
        public ResultsMessures GetResult()
        {
            var results = new ResultsMessures();

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
