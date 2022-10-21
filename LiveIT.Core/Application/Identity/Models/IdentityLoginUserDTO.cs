using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiveIT.Application
{
    public class IdentityLoginUserDTO
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is a required field.")]
        [MinLength(2), MaxLength(64)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is a required field.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
