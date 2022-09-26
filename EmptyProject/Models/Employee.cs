using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(10)]
        [Display(Name = "Enter The Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Add Email Value")]
        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Enter The Email")]
        public string Email { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "Select The Department")]
        [Required]
        public Department? Department { get; set; }

    }
}
