using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Infrastructure.Entities;
using LiveITAPI.Models;

namespace LiveITAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class StakeholdersController : ControllerBase
    {
        private readonly liveitContext _context;

        public StakeholdersController(liveitContext context)
        {
            _context = context;
        }

        // GET: api/Stakeholders/GetStakeholders
        [HttpGet("GetStakeholders")]
        public ActionResult<IEnumerable<StakeholderModel>> GetStakeholders()
        {
            List<StakeholderModel> stakeholderModels = _context.Stakeholders.Select(k => new StakeholderModel
            {
                Name = k.Name,
                Country = _context.Countries.Where(l => l.CountryId == k.CountryId).FirstOrDefault().Name,
                Website = k.Website,
                FacebookUrl = k.FacebookUrl,
                TwitterUrl = k.TwitterUrl,
                YoutubeUrl = k.YoutubeUrl,
                Email = k.Email,
                LinkedInUrl = k.LinkedinUrl,
                StakeholderTypeModels = _context.StakeholderTypeStakeholders.Include(l => l.StakeholderType)
                    .Where(l => l.StakeholderId == k.StakeholderId).Select(l => new StakeholderTypeModel
                    {
                        Name = l.StakeholderType.Name,
                        StakeholderTypeId = l.StakeholderType.StakeholderTypeId
                    }).ToList()
            }).ToList();

            return stakeholderModels;
        }
    }
}
