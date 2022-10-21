using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class TypeSubType
    {
        public int TypeSubTypeId { get; set; }
        public int TypeId { get; set; }
        public int SubTypeId { get; set; }

        public virtual SubType SubType { get; set; }
        public virtual Type Type { get; set; }
    }
}
