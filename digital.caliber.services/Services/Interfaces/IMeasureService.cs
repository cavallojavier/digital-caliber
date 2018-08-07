using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using digital.caliber.model.ViewModels;

namespace digital.caliber.services.Services
{
    public interface IMeasureService
    {
        Task<List<MeasureResumeViewModel>> GetMeasures(string userId, int? maxResults);

        Task<MeasureViewModel> GetMeasure(string userId, int id);

        Task<int> SaveMeasure(string userId, MeasureViewModel viewModel);

        Task DeleteMeasure(string userId, int id);

        Task<ResultsMeasures> GetResult(string userId, int id);
    }
}
