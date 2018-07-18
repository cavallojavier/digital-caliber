using System.ComponentModel.DataAnnotations;

namespace digital.caliber.web.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class AccountRegisterViewModel
    {
        [Required, MaxLength(255)]
        public string FirstName { get; set; }

        [Required, MaxLength(255)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }
    }
}
