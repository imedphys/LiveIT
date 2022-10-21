using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class SubTypeTool
    {
        public int TypeToolId { get; set; }
        public int ToolId { get; set; }
        public int SubTypeId { get; set; }

        public virtual SubType SubType { get; set; }
        public virtual Tool Tool { get; set; }
    }
}
