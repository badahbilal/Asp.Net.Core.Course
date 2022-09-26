using System.ComponentModel.DataAnnotations;

namespace EmptyProject.ViewModels
{
    public class AccountLoginViewModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        //[ValidEmailDomainAttribute(Domain: "badah.com", ErrorMessage = "Email Domain must be badah.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool Remember { get; set; }
    }
}
