using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using digital.caliber.model.Data;
using digital.caliber.model.Models;
using digital.caliber.model.ViewModels;
using digital.caliber.services.CustomLogger;
using Microsoft.EntityFrameworkCore;

namespace digital.caliber.services.Services
{
    public class MeasureService : IMeasureService
    {
        private readonly CaliberDbContext _context;
        private readonly ICustomLogger _logger;

        public MeasureService(CaliberDbContext context, ICustomLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<MeasureResumeViewModel>> GetMeasures(string userId, int? maxResults = null)
        {
            var query = _context.Measures.Where(x => x.UserId == userId)
                            .OrderByDescending(x => x.Id).AsQueryable();

            if (maxResults == null || maxResults.GetValueOrDefault() == 0)
                return MeasureResumeViewModel.ToViewModel(await query.ToListAsync()).ToList();

            var result = await query.Take(maxResults.GetValueOrDefault()).ToListAsync();
            return MeasureResumeViewModel.ToViewModel(result).ToList();
        }

        public async Task<MeasureViewModel> GetMeasure(string userId, int id)
        {
            var result = await _context.Measures
                .Include(x => x.Mouth)
                .Include(x => x.Teeths)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            return MeasureViewModel.ToViewModel(result);
        }

        public async Task<int> SaveMeasure(string userId, MeasureViewModel viewModel)
        {
            if (viewModel.Id > 0)
                return await UpdateMeasure(viewModel);

            var model = new Measures()
            {
                UserId = userId,
                PatientName = viewModel.PatientName,
                HcNumber = viewModel.HcNumber,
                DateMeasure = DateTime.UtcNow,
                Mouth = new MeasuresMouth()
                {
                    LeftInferiorCanine = viewModel.Mouth.LeftInferiorCanine,
                    LeftInferiorIncisive = viewModel.Mouth.LeftInferiorIncisive,
                    LeftInferiorPremolar = viewModel.Mouth.LeftInferiorPremolar,
                    LeftSuperiorCanine = viewModel.Mouth.LeftSuperiorCanine,
                    LeftSuperiorIncisive = viewModel.Mouth.LeftSuperiorIncisive,
                    LeftSuperiorPremolar = viewModel.Mouth.LeftSuperiorPremolar,
                    RightInferiorCanine = viewModel.Mouth.RightInferiorCanine,
                    RightInferiorIncisive = viewModel.Mouth.RightInferiorIncisive,
                    RightInferiorPremolar = viewModel.Mouth.RightInferiorPremolar,
                    RightSuperiorCanine = viewModel.Mouth.RightSuperiorCanine,
                    RightSuperiorIncisive = viewModel.Mouth.RightSuperiorIncisive,
                    RightSuperiorPremolar = viewModel.Mouth.RightSuperiorPremolar,
                },
                Teeths = new MeasuresTeeths()
                {
                    Tooth11 = viewModel.Teeths.Tooth11,
                    Tooth12 = viewModel.Teeths.Tooth12,
                    Tooth13 = viewModel.Teeths.Tooth13,
                    Tooth14 = viewModel.Teeths.Tooth14,
                    Tooth15 = viewModel.Teeths.Tooth15,
                    Tooth16 = viewModel.Teeths.Tooth16,
                    Tooth17 = viewModel.Teeths.Tooth17,

                    Tooth21 = viewModel.Teeths.Tooth21,
                    Tooth22 = viewModel.Teeths.Tooth22,
                    Tooth23 = viewModel.Teeths.Tooth23,
                    Tooth24 = viewModel.Teeths.Tooth24,
                    Tooth25 = viewModel.Teeths.Tooth25,
                    Tooth26 = viewModel.Teeths.Tooth26,
                    Tooth27 = viewModel.Teeths.Tooth27,

                    Tooth31 = viewModel.Teeths.Tooth31,
                    Tooth32 = viewModel.Teeths.Tooth32,
                    Tooth33 = viewModel.Teeths.Tooth33,
                    Tooth34 = viewModel.Teeths.Tooth34,
                    Tooth35 = viewModel.Teeths.Tooth35,
                    Tooth36 = viewModel.Teeths.Tooth36,
                    Tooth37 = viewModel.Teeths.Tooth37,

                    Tooth41 = viewModel.Teeths.Tooth41,
                    Tooth42 = viewModel.Teeths.Tooth42,
                    Tooth43 = viewModel.Teeths.Tooth43,
                    Tooth44 = viewModel.Teeths.Tooth44,
                    Tooth45 = viewModel.Teeths.Tooth45,
                    Tooth46 = viewModel.Teeths.Tooth46,
                    Tooth47 = viewModel.Teeths.Tooth47,
                }
            };

            await _context.Measures.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task DeleteMeasure(string userId, int id)
        {
            var measure = await _context.Measures
                .Include(x => x.Mouth)
                .Include(x => x.Teeths)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            _context.Measures.Remove(measure);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultsMeasures> GetResult(string userId, int id)
        {
            var data = await _context.Measures
                .Include(x => x.Mouth)
                .Include(x => x.Teeths)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            var model = MeasureViewModel.ToViewModel(data);
            var result = MeasuresResultsProvider.GetResult(model.Mouth, model.Teeths);

            result.Id = data.Id;
            result.HcNumber = data.HcNumber;
            result.PatientName = data.PatientName;
            result.DateMeasure = data.DateMeasure;

            return result;
        }

        private async Task<int> UpdateMeasure(MeasureViewModel viewModel)
        {
            var model = await _context.Measures.Include(x => x.Teeths).Include(x => x.Mouth)
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            model.PatientName = viewModel.PatientName;
            model.DateMeasure = DateTime.UtcNow;
            model.HcNumber = viewModel.HcNumber;

            model.Teeths.Tooth11 = viewModel.Teeths.Tooth11;
            model.Teeths.Tooth12 = viewModel.Teeths.Tooth12;
            model.Teeths.Tooth13 = viewModel.Teeths.Tooth13;
            model.Teeths.Tooth14 = viewModel.Teeths.Tooth14;
            model.Teeths.Tooth15 = viewModel.Teeths.Tooth15;
            model.Teeths.Tooth16 = viewModel.Teeths.Tooth16;
            model.Teeths.Tooth17 = viewModel.Teeths.Tooth17;

            model.Teeths.Tooth21 = viewModel.Teeths.Tooth21;
            model.Teeths.Tooth22 = viewModel.Teeths.Tooth22;
            model.Teeths.Tooth23 = viewModel.Teeths.Tooth23;
            model.Teeths.Tooth24 = viewModel.Teeths.Tooth24;
            model.Teeths.Tooth25 = viewModel.Teeths.Tooth25;
            model.Teeths.Tooth26 = viewModel.Teeths.Tooth26;
            model.Teeths.Tooth27 = viewModel.Teeths.Tooth27;

            model.Teeths.Tooth31 = viewModel.Teeths.Tooth31;
            model.Teeths.Tooth32 = viewModel.Teeths.Tooth32;
            model.Teeths.Tooth33 = viewModel.Teeths.Tooth33;
            model.Teeths.Tooth34 = viewModel.Teeths.Tooth34;
            model.Teeths.Tooth35 = viewModel.Teeths.Tooth35;
            model.Teeths.Tooth36 = viewModel.Teeths.Tooth36;
            model.Teeths.Tooth37 = viewModel.Teeths.Tooth37;

            model.Teeths.Tooth41 = viewModel.Teeths.Tooth41;
            model.Teeths.Tooth42 = viewModel.Teeths.Tooth42;
            model.Teeths.Tooth43 = viewModel.Teeths.Tooth43;
            model.Teeths.Tooth44 = viewModel.Teeths.Tooth44;
            model.Teeths.Tooth45 = viewModel.Teeths.Tooth45;
            model.Teeths.Tooth46 = viewModel.Teeths.Tooth46;
            model.Teeths.Tooth47 = viewModel.Teeths.Tooth47;

            model.Mouth.LeftInferiorCanine = viewModel.Mouth.LeftInferiorCanine;
            model.Mouth.LeftInferiorIncisive = viewModel.Mouth.LeftInferiorIncisive;
            model.Mouth.LeftInferiorPremolar = viewModel.Mouth.LeftInferiorPremolar;
            model.Mouth.LeftSuperiorCanine = viewModel.Mouth.LeftSuperiorCanine;
            model.Mouth.LeftSuperiorIncisive = viewModel.Mouth.LeftSuperiorIncisive;
            model.Mouth.LeftSuperiorPremolar = viewModel.Mouth.LeftSuperiorPremolar;
            model.Mouth.RightInferiorCanine = viewModel.Mouth.RightInferiorCanine;
            model.Mouth.RightInferiorPremolar = viewModel.Mouth.RightInferiorPremolar;
            model.Mouth.RightInferiorIncisive = viewModel.Mouth.RightInferiorIncisive;
            model.Mouth.RightSuperiorCanine = viewModel.Mouth.RightSuperiorCanine;
            model.Mouth.RightSuperiorIncisive = viewModel.Mouth.RightSuperiorIncisive;
            model.Mouth.RightSuperiorPremolar = viewModel.Mouth.RightSuperiorPremolar;

            _context.Update(model);
            await _context.SaveChangesAsync();

            return model.Id;
        }
    }
}
