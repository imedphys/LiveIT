using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class CatalogueTypeCatalogue
    {
        public int CatalogueTypeCatalogueId { get; set; }
        public int CatalogueId { get; set; }
        public int CatalogueTypeId { get; set; }

        public virtual Catalogue Catalogue { get; set; }
        public virtual CatalogueType CatalogueType { get; set; }
    }
}
