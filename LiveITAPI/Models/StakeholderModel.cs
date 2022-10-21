using System.Collections.Generic;

namespace LiveITAPI.Models
{
    public class StakeholderModel
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public IEnumerable<StakeholderTypeModel> StakeholderTypeModels { get; set; }
    }
}
