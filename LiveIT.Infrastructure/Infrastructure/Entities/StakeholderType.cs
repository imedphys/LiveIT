using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class StakeholderType
    {
        public StakeholderType()
        {
            StakeholderTypeStakeholders = new HashSet<StakeholderTypeStakeholder>();
        }

        public int StakeholderTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StakeholderTypeStakeholder> StakeholderTypeStakeholders { get; set; }
    }
}
