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

        public async Task<List<MeasureViewModel>> GetMeasures(string userId)
        {
            var result = await _context.Measures.Where(x => x.UserId == userId).ToListAsync();

            return MeasureViewModel.ToViewModel(result);
        }

        public async Task<MeasureViewModel> GetMeasure(string userId, int id)
        {
            var result = await _context.Measures
                .Include(x => x.Mouth)
                .Include(x => x.Teeths)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            return MeasureViewModel.ToViewModel(result);
        }

        public async Task SaveMeasure(string userId, MeasureViewModel viewModel)
        {
            var model = new Measures()
            {
                UserId = userId,
                PatientName = viewModel.PatientName,
                HcNumber = viewModel.HcNumber,
                DateMessure = DateTime.UtcNow,
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

            if (viewModel.Id > 0)
            {
                model.Id = viewModel.Id;
                //_context.Measures.Update(model);
                _context.Entry(model).State = EntityState.Modified;
            }
            else
            {
                await _context.Measures.AddAsync(model);
            }

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
    }
}
