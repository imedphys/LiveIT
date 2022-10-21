using System;
using System.Collections.Generic;
using System.Text;

namespace LiveIT.Toolkit.Models
{
    public class AdminDTO
    {
        public List<CatalogueDTO> catalogueDTOs { get; set; }
        public List<StakeholderDTO> stakeholderDTOs { get; set; }
        public List<ToolDTO> toolDTOs { get; set; }
    }
}
