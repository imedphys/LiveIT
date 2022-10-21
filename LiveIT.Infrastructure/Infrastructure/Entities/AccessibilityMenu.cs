using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class AccessibilityMenu
    {
        public int AccesibilityMenuId { get; set; }
        public int LanguageCode { get; set; }
        public int Fonts { get; set; }
        public int Contrast { get; set; }
        public int Colored { get; set; }
        public int Underline { get; set; }
    }
}
