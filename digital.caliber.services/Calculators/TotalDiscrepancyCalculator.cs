using System.Threading.Tasks;
using digital.caliber.model.ViewModels;

namespace digital.caliber.services.Calculators
{
    public static class TotalDiscrepancyCalculator
    {
        public static async Task<TotalDiscrepancy> GetResult(DentalBoneDiscrepancy dentalBoneDiscrepancy, CefalometricDiscrepancy cefalometricDiscrepancy)
        {
            return await Task.FromResult(new TotalDiscrepancy()
            {
                Superior = cefalometricDiscrepancy.Superior - dentalBoneDiscrepancy.Superior,
                Inferior = cefalometricDiscrepancy.Inferior - dentalBoneDiscrepancy.Inferior
            });
        }
    }
}
