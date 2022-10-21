using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveITAPI.Models
{
    public class ToolModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionEL { get; set; }
        public string DescriptionPT { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public int? PublicationYear { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public byte IsActive { get; set; }
        public IEnumerable<SubTypeModel> ToolSubTypes { get; set; }
    }
}
