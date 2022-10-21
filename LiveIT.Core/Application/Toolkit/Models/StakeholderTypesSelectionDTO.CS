using System;
using System.Collections.Generic;
using System.Text;

namespace LiveIT.Toolkit.Models
{
    public class StakeholderTypesSelectionDTO
    {
        public int StakeholderTypeId { get; set; }
        public int StakeholderId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
