using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LiveIT.Toolkit.Models
{
    public class ToolkitDTO
    {
        public IEnumerable<SelectListItem> CountryListItems { get; set; }
        [Display(Name = "Country")]
        public string SelectedCountry { get; set; }

        public IEnumerable<SelectListItem> StakeholderTypeListItems { get; set; }
        [Display(Name = "Type")]
        public string SelectedStakeholderType { get; set; }

        public IEnumerable<SelectListItem> CatalogueTypeListItems { get; set; }
        [Display(Name = "Type")]
        public string SelectedCatalogueType { get; set; }

        public IEnumerable<SelectListItem> CategoryListItems { get; set; }
        [Display(Name = "Category")]
        public string SelectedCategory { get; set; }

        public IEnumerable<SelectListItem> SubCategoryListItems { get; set; }
        [Display(Name = "SubCategory")]
        public string SelectedSubCategory { get; set; }

        public IEnumerable<SelectListItem> AccessibilityListItems { get; set; }
        [Display(Name = "Type of Accessibility")]
        public string SelectedAccessibility { get; set; }

        public IEnumerable<SelectListItem> PlatformListItems { get; set; }
        [Display(Name = "Platform")]
        public string SelectedPlatform { get; set; }

        public AccessibilityMenuIPDTO AccessibilityMenuIPDTO { get; set; }

        public List<TranslationDTO> TranslationDTOs { get; set; }
    }
}
