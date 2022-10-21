using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class Country
    {
        public Country()
        {
            Stakeholders = new HashSet<Stakeholder>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Stakeholder> Stakeholders { get; set; }
    }
}
