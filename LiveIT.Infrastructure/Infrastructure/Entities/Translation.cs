using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class Translation
    {
        public int TranslationId { get; set; }
        public string TextEn { get; set; }
        public string TextEl { get; set; }
        public string TextPt { get; set; }
    }
}
