using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int ToolId { get; set; }
        public int Usefulness { get; set; }
        public int Innovation { get; set; }
        public int Safety { get; set; }

        public virtual Tool Tool { get; set; }
    }
}
