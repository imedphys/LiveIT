using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class CatalogueType
    {
        public CatalogueType()
        {
            CatalogueTypeCatalogues = new HashSet<CatalogueTypeCatalogue>();
        }

        public int CatalogueTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CatalogueTypeCatalogue> CatalogueTypeCatalogues { get; set; }
    }
}
