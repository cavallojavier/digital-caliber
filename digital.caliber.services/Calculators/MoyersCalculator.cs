using System.Threading.Tasks;
using digital.caliber.model.Models;
using digital.caliber.model.ViewModels;
using digital.caliber.services.CalculationTables;

namespace digital.caliber.services.Calculators
{
    public class MoyersCalculator
    {
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="mouthMessure">The mouth messure.</param>
        /// <param name="theethMessure">The theeth messure.</param>
        /// <returns></returns>
        public static async Task<Moyers> GetResult(MeasuresMouthViewModel mouthMessure, MeasuresTeethsViewModel theethMessure)
        {
            var result = new Moyers();

            var theeths = TheethsSum.GetResults(theethMessure);
            var mouthCalculations = MouthSum.GetResults(mouthMessure);

            var incisivesSum = theeths.SumInferiorFour;

            result.RightSuperior = await GetMoyerResult(incisivesSum, mouthCalculations.RightSuperiorAvailableSpace, true);

            result.RightInferior = await GetMoyerResult(incisivesSum, mouthCalculations.RightInferiorAvailableSpace, false);

            result.LeftSuperior = await GetMoyerResult(incisivesSum, mouthCalculations.LeftSuperiorAvailableSpace, true);

            result.LeftInferior = await GetMoyerResult(incisivesSum, mouthCalculations.LeftInferiorAvailableSpace, false);

            return result;
        }

        /// <summary>
        /// Gets the moyer result.
        /// </summary>
        /// <param name="inferiorIncisivesSum">The inferior incisives sum.</param>
        /// <param name="availableSpace">The available space.</param>
        /// <param name="isSuperior">if set to <c>true</c> [is superior].</param>
        /// <returns></returns>
        private static async Task<decimal?> GetMoyerResult(decimal inferiorIncisivesSum, decimal availableSpace, bool isSuperior)
        {
            // Find reference in Moyers table
            var referenceValue = isSuperior ? await MoyersTable.FindMoyerSuperiorValue(inferiorIncisivesSum)
                                                : await MoyersTable.FindMoyerInferiorValue(inferiorIncisivesSum);

            // Moyers = step 4 - step 1
            return availableSpace - referenceValue;
        }
    }
}
