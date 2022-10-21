using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class TypeTool
    {
        public int TypeToolId { get; set; }
        public int TypeId { get; set; }
        public int ToolId { get; set; }

        public virtual Tool Tool { get; set; }
    }
}
