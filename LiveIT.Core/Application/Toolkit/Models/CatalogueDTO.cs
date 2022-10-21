using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiveIT.Toolkit.Models
{
    public class CatalogueDTO
    {
        public int CatalogueId { get; set; }
        public string PassToken { get; set; }
        [Required(ErrorMessage = "Name is a required field.")]
        [MinLength(3, ErrorMessage = "Name is too short"), MaxLength(150, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
        public string Author { get; set; }
        public int? Year { get; set; }
        public string TargetPopulation { get; set; }
        public string Theme { get; set; }
        public string Country { get; set; }

        [MinLength(1, ErrorMessage = "Link is too short"), MaxLength(750, ErrorMessage = "Link is too long")]
        public string Link { get; set; }

        public IEnumerable<SelectListItem> CountryListItems { get; set; }
        [Display(Name = "Country")]
        public string CountryName { get; set; }

        public IEnumerable<CatalogueTypesSelectionDTO> CatalogueTypesSelectionDTOs { get; set; }
        public string SelectedCatalogueType { get; set; }

        public int StakeHolderTypeId { get; set; }
        public string StakeHolderTypeName { get; set; }
    }
}
