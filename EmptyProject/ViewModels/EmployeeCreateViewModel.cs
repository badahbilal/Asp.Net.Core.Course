using EmptyProject.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.ViewModels
{
    public class EmployeeCreateViewModel
    {

        [Required]
        [MaxLength(20)]
        [MinLength(10)]
        [Display(Name = "Enter The Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Add Email Value")]
        [EmailAddress]
        [Display(Name = "Enter The Email")]
        public string Email { get; set; }

        public IFormFile Image { get; set; }

        [Display(Name = "Select The Department")]
        [Required]
        public Department? Department { get; set; }
    }
}
