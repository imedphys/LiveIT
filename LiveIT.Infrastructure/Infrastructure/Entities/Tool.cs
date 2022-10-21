using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class Tool
    {
        public Tool()
        {
            Ratings = new HashSet<Rating>();
            SubTypeTools = new HashSet<SubTypeTool>();
        }

        public int ToolId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionEl { get; set; }
        public string DescriptionPt { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public int? PublicationYear { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public byte IsActive { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<SubTypeTool> SubTypeTools { get; set; }
    }
}
