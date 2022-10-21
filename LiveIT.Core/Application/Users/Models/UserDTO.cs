using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiveIT.Application
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string PassToken { get; set; }
        [Display(Name = "First name")]
        public string Firstname { get; set; }
        [Display(Name = "Last name")]
        public string Lastname { get; set; }
        [Display(Name = "Email address")]
        public string Email { get; set; }
        [Display(Name = "Telephone")]
        [MinLength(6, ErrorMessage = "Value is too short"), MaxLength(14, ErrorMessage = "Value is too long")]
        [RegularExpression("^[0-9+ ]+$", ErrorMessage = "Value is not acceptable.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Last Connection")]
        public string LastConnectionDateTime { get; set; }
        [Display(Name = "Registered")]
        public DateTime Registered { get; set; }


        public IEnumerable<SelectListItem> RoleListItems { get; set; }
        [Display(Name = "Ρόλος")]
        public string SelectedRole { get; set; }
    }
}
