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

        public DateTime DateMessure { get; set; }

        public MeasuresTeethsViewModel Teeths { get; set; }

        public MeasuresMouthViewModel Mouth { get; set; }

        public static MeasureViewModel ToViewModel(Measures measure)
        {
            var model = new MeasureViewModel();

            return model;
        }

        public static List<MeasureViewModel> ToViewModel(List<Measures> measures)
        {
            var models = new List<MeasureViewModel>();

            return models;
        }
    }
}
