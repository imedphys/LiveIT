using System;
using System.Collections.Generic;
using System.Text;

namespace LiveIT.Toolkit.Models
{
    public class AccessibilityMenuIPDTO
    {
        public int Fonts { get; set; }
        public int Contrast { get; set; }
        public int Colored { get; set; }
        public int Underline { get; set; }
        public int LanguageId { get; set; }
        public string IP { get; set; }
    }
}
