using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Enter the new name")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
