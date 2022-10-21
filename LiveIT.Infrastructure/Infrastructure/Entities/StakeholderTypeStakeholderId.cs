using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class StakeholderTypeStakeholderId
    {
        public int StakeholderTypeStakeholderId1 { get; set; }
        public int StakeholderId { get; set; }
        public int StakeholderTypeId { get; set; }

        public virtual Stakeholder Stakeholder { get; set; }
        public virtual StakeholderType StakeholderType { get; set; }
    }
}
