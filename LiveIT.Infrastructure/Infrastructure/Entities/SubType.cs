using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class SubType
    {
        public SubType()
        {
            SubTypeTools = new HashSet<SubTypeTool>();
            TypeSubTypes = new HashSet<TypeSubType>();
        }

        public int SubTypeId { get; set; }
        public string Name { get; set; }
        public string NameEl { get; set; }
        public string NamePt { get; set; }

        public virtual ICollection<SubTypeTool> SubTypeTools { get; set; }
        public virtual ICollection<TypeSubType> TypeSubTypes { get; set; }
    }
}
