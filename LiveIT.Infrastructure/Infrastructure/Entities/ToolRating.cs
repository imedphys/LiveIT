using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class ToolRating
    {
        public int ToolRatingId { get; set; }
        public int ToolId { get; set; }
        public int RatingId { get; set; }

        public virtual Rating Rating { get; set; }
        public virtual Tool ToolRatingNavigation { get; set; }
    }
}
