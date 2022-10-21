using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class Catalogue
    {
        public Catalogue()
        {
            CatalogueTypeCatalogues = new HashSet<CatalogueTypeCatalogue>();
        }

        public int CatalogueId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int? Year { get; set; }
        public string TargetPopulation { get; set; }
        public string Theme { get; set; }
        public string Country { get; set; }
        public string Link { get; set; }

        public virtual ICollection<CatalogueTypeCatalogue> CatalogueTypeCatalogues { get; set; }
    }
}
