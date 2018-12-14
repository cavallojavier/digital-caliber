using System.Threading.Tasks;
using digital.caliber.model.ViewModels;

namespace digital.caliber.services.Calculators
{
    public static class CefalometricDiscrepancyCalculator
    {
        public static async Task<CefalometricDiscrepancy> GetResult(decimal? protrusionSuperior, decimal? protrusionInferior)
        {
            return await Task.FromResult(new CefalometricDiscrepancy()
            {
                Superior = (-(protrusionSuperior.GetValueOrDefault() - (decimal)3.5) * 2),
                Inferior = (-(protrusionInferior.GetValueOrDefault() - 1) * 2)
            });
        }
    }
}
