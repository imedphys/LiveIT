using System;
using System.Collections.Generic;
using System.Text;

namespace LiveIT.Toolkit.Models
{
    public class ToolSubTypesSelectionDTO
    {
        public int ToolSubTypeId { get; set; }
        public int ToolId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
