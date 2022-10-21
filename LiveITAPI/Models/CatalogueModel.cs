using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveITAPI.Models
{
    public class CatalogueModel
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int? Year { get; set; }
        public string TargetPopulation { get; set; }
        public string Theme { get; set; }
        public string Link { get; set; }
        public string Country { get; set; }
        public IEnumerable<CatalogueTypeModel> CatalogueTypeModels { get; set; }
    }
}
