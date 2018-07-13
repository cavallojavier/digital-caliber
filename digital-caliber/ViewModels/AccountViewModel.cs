using System.ComponentModel.DataAnnotations;

namespace digital.caliber.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class AccountRegisterViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
