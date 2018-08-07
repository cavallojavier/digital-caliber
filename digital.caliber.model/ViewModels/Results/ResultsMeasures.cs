using System;

namespace digital.caliber.model.ViewModels
{
    public class ResultsMeasures
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        public string HcNumber { get; set; }

        public DateTime DateMeasure { get; set; }

        public DentalBoneDiscrepancy DentalBoneDiscrepancy { get; set; }

        public TanakaJohnston Tanaka { get; set; }

        public Pont Pont { get; set; }

        public Moyers Moyers { get; set; }

        public BoltonTotal BoltonTotal { get; set; }

        public BoltonPreviousRelation BoltonPreviousRelation { get; set; }
    }
}
