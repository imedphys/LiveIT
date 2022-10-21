using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiveIT.Application
{
    public class IdentityForgotPasswordDTO
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is a required field.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
