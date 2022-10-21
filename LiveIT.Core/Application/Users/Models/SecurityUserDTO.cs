using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiveIT.Users.Models
{
    public class SecurityUserDTO
    {
        public int UserId { get; set; }
        public string PassToken { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Email address")]
        public string Email { get; set; }
        [Display(Name = "Telephone")]
        [MinLength(6, ErrorMessage = "Value is too short"), MaxLength(14, ErrorMessage = "Value is too long")]
        [RegularExpression("^[0-9+ ]+$", ErrorMessage = "Value is not acceptable.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        [Display(Name = " ")]
        public string Photo { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Registered")]
        public DateTime Registered { get; set; }
    }
}
