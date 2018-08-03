using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using digital.caliber.model.ViewModels;

namespace digital.caliber.services.Services
{
    public interface IMeasureService
    {
        Task<List<MeasureViewModel>> GetMeasures(string userId);

        Task<MeasureViewModel> GetMeasure(string userId, int id);

        Task SaveMeasure(string userId, MeasureViewModel viewModel);

        Task DeleteMeasure(string userId, int id);
    }
}
