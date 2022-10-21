using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiveIT.Application
{
    public class IdentityChangePasswordDTO
    {
        [Required]
        [Display(Name = "IdentityID")]
        public string IdentityID { get; set; }
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        [Display(Name = "Password*")]
        [Required(ErrorMessage = "Password is a required field.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password*")]
        [Required(ErrorMessage = "Password is a required field.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
