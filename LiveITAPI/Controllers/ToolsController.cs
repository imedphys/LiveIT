using Infrastructure.Entities;
using LiveITAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveITAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : Controller
    {
        private readonly liveitContext _context;

        public ToolsController(liveitContext context)
        {
            _context = context;
        }

        // GET: api/Catalogues/GetCatalogues
        [HttpGet("GetTools")]
        public ActionResult<IEnumerable<ToolModel>> GetTools()
        {
            List<ToolModel> toolModels = _context.Tools.Select(k => new ToolModel
            {
                Name = k.Name,
                Description = k.Description,
                DescriptionEL = k.DescriptionEl,
                DescriptionPT = k.DescriptionPt,
                ImageUrl = k.ImageUrl,
                Link = k.Link,
                PublicationYear = k.PublicationYear,
                RegistrationDateTime = k.RegistrationDateTime,
                VideoUrl = k.VideoUrl,
                IsActive = k.IsActive,
                ToolSubTypes = _context.SubTypeTools.Include(l => l.SubType)
                    .Where(l => l.ToolId == k.ToolId).Select(l => new SubTypeModel
                    {
                        Name = l.SubType.Name,
                        NameEl = l.SubType.NameEl,
                        NamePt = l.SubType.NamePt,
                        SubTypeId = l.SubType.SubTypeId
                    }).ToList()
            }).ToList();

            return toolModels;
        }
    }
}
