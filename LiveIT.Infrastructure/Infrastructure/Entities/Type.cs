using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class Type
    {
        public Type()
        {
            TypeSubTypes = new HashSet<TypeSubType>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; }
        public string NameEl { get; set; }
        public string NamePt { get; set; }

        public virtual ICollection<TypeSubType> TypeSubTypes { get; set; }
    }
}
