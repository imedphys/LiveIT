using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiveIT.Application
{
    public partial class IdentityUserDTO
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is a required field.")]
        [MinLength(2, ErrorMessage = "First name is too short"), MaxLength(26, ErrorMessage = "First name is too long")]
        [RegularExpression("[\u0370-\u03ff\u1f00-\u1fffa-zA-Z]+", ErrorMessage = "First name is required and must be properly formatted.")]
        public string Firstname { get; set; }
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is a required field.")]
        [MinLength(2, ErrorMessage = "Last name is too short"), MaxLength(26, ErrorMessage = "Last name is too long")]
        [RegularExpression("[\u0370-\u03ff\u1f00-\u1fffa-zA-Z]+", ErrorMessage = "Last name is required and must be properly formatted.")]
        public string Lastname { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is a required field.")]
        [MinLength(2, ErrorMessage = "Email address is too short"), MaxLength(64, ErrorMessage = "Email address is too long")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public byte? EmailConfirmed { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(6, ErrorMessage = "Password is too short"), MaxLength(30, ErrorMessage = "Password is too long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Password is a required field.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Telephone number")]
        [MinLength(6, ErrorMessage = "Value is too short"), MaxLength(10, ErrorMessage = "Value is too long")]
        [RegularExpression("^[0-9+]+$", ErrorMessage = "Value is not acceptable.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public byte? PhoneNumberConfirmed { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
