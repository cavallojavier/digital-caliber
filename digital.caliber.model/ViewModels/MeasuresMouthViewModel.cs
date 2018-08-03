using System.ComponentModel.DataAnnotations.Schema;

namespace digital.caliber.model.Models
{
    public class MeasuresMouthViewModel
    {
        public int Id { get; set; }

        public int MeasureId { get; set; }

        public decimal LeftSuperiorIncisive { get; set; }

        public decimal RightSuperiorIncisive { get; set; }

        public decimal LeftSuperiorCanine { get; set; }

        public decimal RightSuperiorCanine { get; set; }

        public decimal LeftSuperiorPremolar { get; set; }

        public decimal RightSuperiorPremolar { get; set; }

        public decimal LeftInferiorIncisive { get; set; }

        public decimal RightInferiorIncisive { get; set; }

        public decimal LeftInferiorCanine { get; set; }

        public decimal RightInferiorCanine { get; set; }

        public decimal LeftInferiorPremolar { get; set; }

        public decimal RightInferiorPremolar { get; set; }
    }
}
