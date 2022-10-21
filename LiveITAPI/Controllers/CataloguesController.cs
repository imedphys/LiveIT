using Infrastructure.Entities;
using LiveITAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveITAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CataloguesController : ControllerBase
    {
        private readonly liveitContext _context;

        public CataloguesController(liveitContext context)
        {
            _context = context;
        }

        // GET: api/Catalogues/GetCatalogues
        [HttpGet("GetCatalogues")]
        public ActionResult<IEnumerable<CatalogueModel>> GetCatalogues()
        {
            List<CatalogueModel> catalogueModels = _context.Catalogues.Select(k => new CatalogueModel
            {
                Name = k.Name,
                Country = k.Country,
                Author = k.Author,
                Year = k.Year,
                TargetPopulation = k.TargetPopulation,
                Theme = k.Theme,
                Link = k.Link,
                CatalogueTypeModels = _context.CatalogueTypeCatalogues.Include(l => l.CatalogueType)
                    .Where(l => l.CatalogueId == k.CatalogueId).Select(l => new CatalogueTypeModel
                    {
                        Name = l.CatalogueType.Name,
                        CatalogueTypeId = l.CatalogueType.CatalogueTypeId
                    }).ToList()
            }).ToList();

            return catalogueModels;
        }
    }
}
