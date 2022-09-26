using System.ComponentModel.DataAnnotations;

namespace EmptyProject.ViewModels
{
    public class AdministrationCreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
