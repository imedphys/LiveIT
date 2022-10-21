using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class Stakeholder
    {
        public Stakeholder()
        {
            StakeholderTypeStakeholders = new HashSet<StakeholderTypeStakeholder>();
        }

        public int StakeholderId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string Website { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string Email { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<StakeholderTypeStakeholder> StakeholderTypeStakeholders { get; set; }
    }
}
