using System;
using Microsoft.AspNetCore.Identity;

namespace digital.caliber.model.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }

        public DateTime RegisteredOn { get; set; }
    }
}
