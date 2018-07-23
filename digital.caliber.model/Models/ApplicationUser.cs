using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace digital.caliber.model.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(255)]
        public string FirstName { get; set; }

        [Required, MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        public DateTime RegisteredOn { get; set; }
    }
}
