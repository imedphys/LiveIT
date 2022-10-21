using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class AccesibilityMenyIp
    {
        public int AccessibilityMenuIpid { get; set; }
        public int AccesibilityMenuId { get; set; }
        public string RemoteAddress { get; set; }
    }
}
