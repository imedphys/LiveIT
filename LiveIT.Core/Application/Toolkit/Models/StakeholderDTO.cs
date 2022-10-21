using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiveIT.Toolkit.Models
{
    public class StakeholderDTO
    {
        public int StakeholderId { get; set; }
        public string PassToken { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        [MinLength(3, ErrorMessage = "Name is too short"), MaxLength(40, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
        [MinLength(0, ErrorMessage = "Website is too short"), MaxLength(250, ErrorMessage = "Website is too long")]
        public string Website { get; set; }
        [MinLength(0, ErrorMessage = "FacebookUrl is too short"), MaxLength(500, ErrorMessage = "FacebookUrl is too long")]
        public string FacebookUrl { get; set; }
        [MinLength(0, ErrorMessage = "TwitterUrl is too short"), MaxLength(200, ErrorMessage = "TwitterUrl is too long")]
        public string TwitterUrl { get; set; }
        [MinLength(0, ErrorMessage = "YoutubeUrl is too short"), MaxLength(150, ErrorMessage = "YoutubeUrl is too long")]
        public string YoutubeUrl { get; set; }
        [MinLength(0, ErrorMessage = "LinkedInUrl is too short"), MaxLength(150, ErrorMessage = "LinkedInUrl is too long")]
        public string LinkedInUrl { get; set; }
        [MinLength(0, ErrorMessage = "Email is too short"), MaxLength(50, ErrorMessage = "Email is too long")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> CountryListItems { get; set; }
        [Display(Name = "Country")]
        public string SelectedCountry { get; set; }

        public IEnumerable<StakeholderTypesSelectionDTO> StakeholderTypesSelectionDTOs { get; set; }

        public int StakeHolderTypeId { get; set; }
        public string StakeHolderTypeName { get; set; }
    }
}
