using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace digital.caliber.model.Models
{
    public class Measures
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        [MaxLength(255), Required]
        public string PatientName { get; set; }

        [MaxLength(50), Required]
        public string HcNumber { get; set; }

        public DateTime DateMessure { get; set; }

        public virtual MeasuresTeeths Teeths { get; set; }

        public virtual MeasuresMouth Mouth { get; set; }
    }
}
