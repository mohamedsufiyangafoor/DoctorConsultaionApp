using System.ComponentModel.DataAnnotations;

namespace AppoitmentScheduleApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required (ErrorMessage="Enter Valid Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Valid Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
