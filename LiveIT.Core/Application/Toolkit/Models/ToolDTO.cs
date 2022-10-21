using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiveIT.Toolkit.Models
{
    public class ToolDTO
    {
        public int ToolId { get; set; }
        public string PassToken { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        [MinLength(1, ErrorMessage = "Name is too short"), MaxLength(150, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
        [MinLength(0, ErrorMessage = "Description is too short"), MaxLength(2500, ErrorMessage = "Description is too long")]
        public string Description  { get; set; }
        [MinLength(0, ErrorMessage = "Description is too short"), MaxLength(2500, ErrorMessage = "Description is too long")]
        public string DescriptionEL { get; set; }
        [MinLength(0, ErrorMessage = "Description is too short"), MaxLength(2500, ErrorMessage = "Description is too long")]
        public string DescriptionPT { get; set; }
        [MinLength(0, ErrorMessage = "Link is too short"), MaxLength(1000, ErrorMessage = "Link is too long")]
        public string Link { get; set; }
        [MinLength(0, ErrorMessage = "ImageUrl is too short"), MaxLength(250, ErrorMessage = "ImageUrl is too long")]
        public string ImageUrl { get; set; }
        [MinLength(0, ErrorMessage = "VideoUrl is too short"), MaxLength(250, ErrorMessage = "VideoUrl is too long")]
        public string VideoUrl { get; set; }
        [Range(1990, 2024)]
        [RegularExpression("([0-9]+)", ErrorMessage = "Number of the current members must be properly formatted.")]
        public int PublicationYear { get; set; }
        public bool IsActive { get; set; }
        public bool hasRating { get; set; }
        public string CountryCode { get; set; }
        public string Subtype { get; set; }
        public int SubtypeId { get; set; }
        public int TypeId { get; set; }

        public RatingDTO RatingDTO { get; set; }


        public DateTime RegistrationDateTime { get; set; }

        public IEnumerable<SelectListItem> TypeListItems { get; set; }
        [Display(Name = "Type")]
        public string SelectedType { get; set; }

        public IEnumerable<ToolSubTypesSelectionDTO> toolSubTypesSelectionDTOs { get; set; }
        public string SelectedSubType { get; set; }

        public AccessibilityMenuIPDTO AccessibilityMenuIPDTO { get; set; }

        public List<TranslationDTO> TranslationDTOs { get; set; }
    }

    public class RatingDTO 
    {
        public int ToolId { get; set; }
        public double AverageScore { get; set; }
        public double Usefulness { get; set; }
        public double Innovation { get; set; }
        public double Safety { get; set; }
        public int Votes { get; set; }
    }
}
