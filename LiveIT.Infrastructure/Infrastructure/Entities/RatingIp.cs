using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class RatingIp
    {
        public int Ipid { get; set; }
        public int ToolId { get; set; }
        public string RemoteAddress { get; set; }
        public DateTime SubmittedDateTime { get; set; }
    }
}
