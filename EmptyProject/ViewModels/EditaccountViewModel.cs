using EmptyProject.Models;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.ViewModels
{
    public class EditaccountViewModel : AppUser
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation password do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
