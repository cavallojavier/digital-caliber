using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using digital.caliber.model.Models;

namespace digital.caliber.model.ViewModels
{
    public class MeasureViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [MaxLength(255), Required]
        public string PatientName { get; set; }

        [MaxLength(50), Required]
        public string HcNumber { get; set; }

        public DateTime DateMeasure { get; set; }

        public MeasuresTeethsViewModel Teeths { get; set; }

        public MeasuresMouthViewModel Mouth { get; set; }

        public static MeasureViewModel ToViewModel(Measures measure)
        {
            var model = new MeasureViewModel()
            {
                Id = measure.Id,
                HcNumber = measure.HcNumber,
                PatientName = measure.PatientName,
                DateMeasure = measure.DateMeasure,
                Mouth = new MeasuresMouthViewModel(),
                Teeths = new MeasuresTeethsViewModel()
            };

            if (measure.Mouth != null)
            {
                model.Mouth = new MeasuresMouthViewModel()
                {
                    Id = measure.Mouth.Id,
                    LeftInferiorIncisive = measure.Mouth.LeftInferiorIncisive,
                    LeftInferiorPremolar = measure.Mouth.LeftInferiorPremolar,
                    LeftInferiorCanine = measure.Mouth.LeftInferiorCanine,
                    RightInferiorPremolar = measure.Mouth.RightInferiorPremolar,
                    RightSuperiorIncisive = measure.Mouth.RightSuperiorIncisive,
                    RightInferiorIncisive = measure.Mouth.RightInferiorIncisive,
                    RightSuperiorPremolar = measure.Mouth.RightSuperiorPremolar,
                    LeftSuperiorPremolar = measure.Mouth.LeftSuperiorPremolar,
                    LeftSuperiorCanine = measure.Mouth.LeftSuperiorCanine,
                    LeftSuperiorIncisive = measure.Mouth.LeftSuperiorIncisive,
                    RightSuperiorCanine = measure.Mouth.RightSuperiorCanine,
                    RightInferiorCanine = measure.Mouth.RightInferiorCanine,
                    MeasureId = measure.Mouth.MeasureId
                };
            }

            if (measure.Teeths != null)
            {
                model.Teeths = new MeasuresTeethsViewModel()
                {
                    Id = measure.Teeths.Id,
                    MeasureId = measure.Teeths.MeasureId,
                    Tooth11 = measure.Teeths.Tooth11,
                    Tooth12 = measure.Teeths.Tooth12,
                    Tooth13 = measure.Teeths.Tooth13,
                    Tooth14 = measure.Teeths.Tooth14,
                    Tooth15 = measure.Teeths.Tooth15,
                    Tooth16 = measure.Teeths.Tooth16,
                    Tooth17 = measure.Teeths.Tooth17,
                    Tooth21 = measure.Teeths.Tooth21,
                    Tooth22 = measure.Teeths.Tooth22,
                    Tooth23 = measure.Teeths.Tooth23,
                    Tooth24 = measure.Teeths.Tooth24,
                    Tooth25 = measure.Teeths.Tooth25,
                    Tooth26 = measure.Teeths.Tooth26,
                    Tooth27 = measure.Teeths.Tooth27,
                    Tooth31 = measure.Teeths.Tooth31,
                    Tooth32 = measure.Teeths.Tooth32,
                    Tooth33 = measure.Teeths.Tooth33,
                    Tooth34 = measure.Teeths.Tooth34,
                    Tooth35 = measure.Teeths.Tooth35,
                    Tooth36 = measure.Teeths.Tooth36,
                    Tooth37 = measure.Teeths.Tooth37,
                    Tooth41 = measure.Teeths.Tooth41,
                    Tooth42 = measure.Teeths.Tooth42,
                    Tooth43 = measure.Teeths.Tooth43,
                    Tooth44 = measure.Teeths.Tooth44,
                    Tooth45 = measure.Teeths.Tooth45,
                    Tooth46 = measure.Teeths.Tooth46,
                    Tooth47 = measure.Teeths.Tooth47
                };
            }

            return model;
        }

        public static IEnumerable<MeasureViewModel> ToViewModel(List<Measures> measures)
        {
            foreach (var measure in measures)
            {
                yield return ToViewModel(measure);
            }
        }
    }

    public class MeasureResumeViewModel
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        public string HcNumber { get; set; }

        public DateTime DateMeasure { get; set; }

        public static MeasureResumeViewModel ToViewModel(Measures measure)
        {
            var model = new MeasureResumeViewModel()
            {
                Id = measure.Id,
                HcNumber = measure.HcNumber,
                PatientName = measure.PatientName,
                DateMeasure = measure.DateMeasure,
            };

            return model;
        }

        public static IEnumerable<MeasureResumeViewModel> ToViewModel(List<Measures> measures)
        {
            foreach (var measure in measures)
            {
                yield return ToViewModel(measure);
            }
        }
    }
}
