using System;
using System.Collections.Generic;
using System.Text;

namespace LiveIT.Toolkit.Models
{
    public class CatalogueTypesSelectionDTO
    {
        public int CatalogueTypeId { get; set; }
        public int CatalogueId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
