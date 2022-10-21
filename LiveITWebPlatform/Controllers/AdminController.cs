using Application.Security;
using Infrastructure.Entities;
using LiveIT.Toolkit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveIT.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly liveitContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly CustomIDataProtection protector;

        public AdminController(liveitContext context, IHostingEnvironment IHostingEnvironment, CustomIDataProtection customIDataProtection)
        {
            _context = context;
            _environment = IHostingEnvironment;
            protector = customIDataProtection;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Stakeholders));
        }

        public IActionResult Stakeholders()
        {
            List<StakeholderDTO> stakeholderDTOs = _context.Stakeholders.Select(k => new StakeholderDTO
            {
                StakeholderId = k.StakeholderId,
                PassToken = protector.Decode(k.StakeholderId.ToString()),
                Name = k.Name,
                Email = k.Email,
                Website = k.Website
            }).ToList();

            return View(stakeholderDTOs);
        }

        public IActionResult AddStakeholder()
        {
            var countries = _context.Countries.ToList();
            var countryListItems = new List<SelectListItem>();
            foreach (var country in countries)
                countryListItems.Add(new SelectListItem { Text = country.Name, Value = country.CountryId.ToString() });

            return View(new StakeholderDTO {CountryListItems = countryListItems });
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddStakeholder([Bind("Name,Website,FacebookUrl,TwitterUrl,YoutubeUrl,LinkedInUrl,Email,SelectedCountry")] StakeholderDTO stakeholderDTO)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(AddStakeholder));

            if (string.IsNullOrWhiteSpace(stakeholderDTO.SelectedCountry) || string.IsNullOrWhiteSpace(stakeholderDTO.Name))
                return RedirectToAction(nameof(AddStakeholder));

            var countryId = _context.Countries.Where(k => k.CountryId == int.Parse(stakeholderDTO.SelectedCountry)).FirstOrDefault().CountryId;

            Stakeholder stakeholder = new Stakeholder
            {
                Name = stakeholderDTO.Name != null ? stakeholderDTO.Name : string.Empty,
                Website = stakeholderDTO.Website != null ? stakeholderDTO.Website : string.Empty,
                FacebookUrl = stakeholderDTO.FacebookUrl != null ? stakeholderDTO.FacebookUrl : string.Empty,
                TwitterUrl = stakeholderDTO.TwitterUrl != null ? stakeholderDTO.TwitterUrl : string.Empty,
                YoutubeUrl = stakeholderDTO.YoutubeUrl != null ? stakeholderDTO.YoutubeUrl : string.Empty,
                LinkedinUrl = stakeholderDTO.LinkedInUrl != null ? stakeholderDTO.LinkedInUrl : string.Empty,
                Email = stakeholderDTO.Email != null ? stakeholderDTO.Email : string.Empty,
                CountryId = countryId,
            };
            _context.Stakeholders.Add(stakeholder);
            _context.SaveChanges();

            return RedirectToAction(nameof(Stakeholders));
        }
        public async Task<IActionResult> EditStakeholder(string passToken)
        {
            if (passToken == null || passToken == string.Empty) return NotFound();

            var stakeholderTypes = _context.StakeholderTypes.Include(k=> k.StakeholderTypeStakeholders).ToList();
            IEnumerable<StakeholderTypesSelectionDTO> stakeholderTypesSelectionDTOs = stakeholderTypes
                .Select(k => new StakeholderTypesSelectionDTO
                {
                    StakeholderId = int.Parse(protector.Encode(passToken)),
                    StakeholderTypeId = k.StakeholderTypeId,
                    Name = k.Name,
                    IsSelected = k.StakeholderTypeStakeholders
                        .Where(l=> l.StakeholderTypeId == k.StakeholderTypeId  && 
                            l.StakeholderId == int.Parse(protector.Encode(passToken))).Count() == 0? false : true
                }).ToList();

            var countries = _context.Countries.ToList();
            var countryListItems = new List<SelectListItem>();
            foreach (var country in countries)
                countryListItems.Add(new SelectListItem { Text = country.Name, Value = country.Name });
            try
            {
                var stakholdersDTO = await _context.Stakeholders.Include(k => k.Country).Where(k => k.StakeholderId == int.Parse(protector.Encode(passToken))).Select(k => new StakeholderDTO
                {
                    StakeholderId = k.StakeholderId,
                    PassToken = protector.Decode(k.StakeholderId.ToString()),
                    Name = k.Name,
                    Website = k.Website != null ? k.Website : string.Empty,
                    Email = k.Email != null ? k.Email : string.Empty,
                    FacebookUrl = k.FacebookUrl != null ? k.FacebookUrl : string.Empty,
                    TwitterUrl = k.TwitterUrl != null ? k.TwitterUrl : string.Empty,
                    LinkedInUrl = k.LinkedinUrl != null ? k.LinkedinUrl : string.Empty,
                    YoutubeUrl = k.YoutubeUrl != null ? k.YoutubeUrl : string.Empty,
                    SelectedCountry = k.Country.Name,
                    CountryListItems = countryListItems,
                    StakeholderTypesSelectionDTOs = stakeholderTypesSelectionDTOs.ToList()
                }).FirstOrDefaultAsync();

                return View(stakholdersDTO);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditStakeholder([Bind("Name,Website,FacebookUrl,TwitterUrl,YoutubeUrl,LinkedInUrl,Email,SelectedCountry")] StakeholderDTO stakeholderDTO, int stakeholderId)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Stakeholders));

            if (string.IsNullOrWhiteSpace(stakeholderDTO.SelectedCountry) || string.IsNullOrWhiteSpace(stakeholderDTO.Name))
                return RedirectToAction(nameof(AddStakeholder));

            var countryId = _context.Countries.Where(k => k.Name == stakeholderDTO.SelectedCountry).FirstOrDefault().CountryId;

            var stakeholder = _context.Stakeholders.Where(k => k.StakeholderId == stakeholderId).FirstOrDefault();
            stakeholder.Name = stakeholderDTO.Name != null ? stakeholderDTO.Name : string.Empty;
            stakeholder.Website = stakeholderDTO.Website != null ? stakeholderDTO.Website : string.Empty;
            stakeholder.FacebookUrl = stakeholderDTO.FacebookUrl != null ? stakeholderDTO.FacebookUrl : string.Empty;
            stakeholder.TwitterUrl = stakeholderDTO.TwitterUrl != null ? stakeholderDTO.TwitterUrl : string.Empty;
            stakeholder.YoutubeUrl = stakeholderDTO.YoutubeUrl != null ? stakeholderDTO.YoutubeUrl : string.Empty;
            stakeholder.LinkedinUrl = stakeholderDTO.LinkedInUrl != null ? stakeholderDTO.LinkedInUrl : string.Empty;
            stakeholder.Email = stakeholderDTO.Email != null ? stakeholderDTO.Email : string.Empty;
            stakeholder.CountryId = countryId;
            _context.Update(stakeholder);
            _context.SaveChanges();

            return RedirectToAction(nameof(Stakeholders));
        }
        public IActionResult DeleteStakeholder(string passToken)
        {
            if (passToken == null) NotFound();

            Stakeholder stakeholder = _context.Stakeholders.Where(k => k.StakeholderId == int.Parse(protector.Encode(passToken))).FirstOrDefault();
            List<StakeholderTypeStakeholder> stakeholderTypeStakeholders = _context.StakeholderTypeStakeholders.Where(k => k.StakeholderId == int.Parse(protector.Encode(passToken))).ToList();

            _context.StakeholderTypeStakeholders.RemoveRange(stakeholderTypeStakeholders);
            _context.Stakeholders.Remove(stakeholder);
            _context.SaveChanges();

            return RedirectToAction(nameof(Stakeholders));
        }
        public async Task<IActionResult> UpdateStakeholderType(string passToken, int stakeholdertypeid, int stakeholderid)
        {
            if (_context.StakeholderTypeStakeholders.Any(k => k.StakeholderId == stakeholderid && k.StakeholderTypeId == stakeholdertypeid))
            {
                StakeholderTypeStakeholder stakeholderTypeStakeholder = _context.StakeholderTypeStakeholders
                    .Where(k => k.StakeholderId == stakeholderid && k.StakeholderTypeId == stakeholdertypeid).FirstOrDefault();
                _context.StakeholderTypeStakeholders.Remove(stakeholderTypeStakeholder);
            }
            else
            {
                await _context.StakeholderTypeStakeholders.AddAsync(new StakeholderTypeStakeholder { StakeholderId = stakeholderid, StakeholderTypeId = stakeholdertypeid });
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditStakeholder), new { passToken = passToken });
        }

        public IActionResult Catalogues()
        {
            List<CatalogueDTO> catalogueDTOs = _context.Catalogues.Select(k => new CatalogueDTO
            {
                CatalogueId = k.CatalogueId,
                PassToken = protector.Decode(k.CatalogueId.ToString()),
                Name = k.Name,
                Country = k.Country
            }).ToList();

            return View(catalogueDTOs);
        }
        public IActionResult AddCatalogue()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddCatalogue([Bind("Name,Author,Year,TargetPopulation,Theme,Country,Link")] CatalogueDTO catalogueDTO)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(AddCatalogue));

            if (catalogueDTO.Year == null)
                catalogueDTO.Year = 2000;

            if (string.IsNullOrWhiteSpace(catalogueDTO.Name) || catalogueDTO.Year.Value <= 1900 || catalogueDTO.Year.Value >= 2025)
                return RedirectToAction(nameof(AddCatalogue));

            Catalogue catalogue = new Catalogue
            {
                Name = catalogueDTO.Name,
                Author = catalogueDTO.Author != null ? catalogueDTO.Author : string.Empty,
                TargetPopulation = catalogueDTO.TargetPopulation != null ? catalogueDTO.TargetPopulation : string.Empty,
                Country = catalogueDTO.Country != null ? catalogueDTO.Country : string.Empty,
                Link = catalogueDTO.Link != null ? catalogueDTO.Link : string.Empty,
                Theme = catalogueDTO.Theme != null ? catalogueDTO.Theme : string.Empty,
                Year = catalogueDTO.Year,
            };

            _context.Catalogues.Add(catalogue);
            _context.SaveChanges();

            return RedirectToAction(nameof(Catalogues));
        }
        public async Task<IActionResult> EditCatalogue(string passToken)
        {
            if (passToken == null || passToken == string.Empty) return NotFound();

            var catalogueTypes = _context.CatalogueTypes.Include(k => k.CatalogueTypeCatalogues).ToList();
            IEnumerable<CatalogueTypesSelectionDTO> catalogueTypesSelectionDTOs = catalogueTypes
                .Select(k => new CatalogueTypesSelectionDTO
                {
                    CatalogueId = int.Parse(protector.Encode(passToken)),
                    CatalogueTypeId = k.CatalogueTypeId,
                    Name = k.Name,
                    IsSelected = k.CatalogueTypeCatalogues
                        .Where(l => l.CatalogueTypeId == k.CatalogueTypeId &&
                            l.CatalogueId == int.Parse(protector.Encode(passToken))).Count() == 0 ? false : true
                }).ToList();

            try
            {
                var cataloguesDTO = await _context.Catalogues.Where(k => k.CatalogueId == int.Parse(protector.Encode(passToken))).Select(k => new CatalogueDTO
                {
                    CatalogueId = k.CatalogueId,
                    PassToken = protector.Decode(k.CatalogueId.ToString()),
                    Name = k.Name,
                    Link = k.Link != null ? k.Link : string.Empty,
                    Country = k.Country != null ? k.Country : string.Empty,
                    Author = k.Author != null ? k.Author : string.Empty,
                    Theme = k.Theme != null ? k.Theme : string.Empty,
                    TargetPopulation = k.TargetPopulation != null ? k.TargetPopulation : string.Empty,
                    Year = k.Year,
                    SelectedCatalogueType = _context.CatalogueTypeCatalogues.Include(k => k.CatalogueType).Where(l => l.CatalogueId == k.CatalogueId).FirstOrDefault().CatalogueType.Name,
                    CatalogueTypesSelectionDTOs = catalogueTypesSelectionDTOs.ToList()
                }).FirstOrDefaultAsync();

                return View(cataloguesDTO);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditCatalogue([Bind("Name,Author,Year,TargetPopulation,Theme,Country,Link")] CatalogueDTO catalogueDTO, int catalogueId)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(EditCatalogue));

            if (string.IsNullOrWhiteSpace(catalogueDTO.Name) || catalogueDTO.Year.Value <= 1900 || catalogueDTO.Year.Value >= 2025)
                return RedirectToAction(nameof(EditCatalogue));

            var catalogue = _context.Catalogues.Where(k => k.CatalogueId == catalogueId).FirstOrDefault();
            catalogue.Name = catalogueDTO.Name;
            catalogue.Author = catalogueDTO.Author != null ? catalogueDTO.Author : string.Empty;
            catalogue.TargetPopulation = catalogueDTO.TargetPopulation != null ? catalogueDTO.TargetPopulation : string.Empty;
            catalogue.Country = catalogueDTO.Country != null ? catalogueDTO.Country : string.Empty;
            catalogue.Link = catalogueDTO.Link != null ? catalogueDTO.Link : string.Empty;
            catalogue.Theme = catalogueDTO.Theme != null ? catalogueDTO.Theme : string.Empty;
            catalogue.Year = catalogueDTO.Year;
            _context.Update(catalogue);
            _context.SaveChanges();

            return RedirectToAction(nameof(Catalogues));
        }
        public IActionResult DeleteCatalogue(string passToken)
        {
            if (passToken == null) NotFound();

            Catalogue catalogue = _context.Catalogues.Where(k=> k.CatalogueId == int.Parse(protector.Encode(passToken))).FirstOrDefault();
            List<CatalogueTypeCatalogue> catalogueTypeCatalogues = _context.CatalogueTypeCatalogues.Where(k => k.CatalogueId == int.Parse(protector.Encode(passToken))).ToList();

            _context.CatalogueTypeCatalogues.RemoveRange(catalogueTypeCatalogues);
            _context.Catalogues.Remove(catalogue);
            _context.SaveChanges();

            return RedirectToAction(nameof(Catalogues));
        }
        public async Task<IActionResult> UpdateCatalogueType(string passToken, int cataloguetypeid, int catalogueid)
        {
            if (_context.CatalogueTypeCatalogues.Any(k => k.CatalogueId == catalogueid && k.CatalogueTypeId == cataloguetypeid))
            {
                CatalogueTypeCatalogue catalogueTypeCatalogue = _context.CatalogueTypeCatalogues
                    .Where(k => k.CatalogueId == catalogueid && k.CatalogueTypeId == cataloguetypeid).FirstOrDefault();
                _context.CatalogueTypeCatalogues.Remove(catalogueTypeCatalogue);
            }
            else
            {
                await _context.CatalogueTypeCatalogues.AddAsync(new CatalogueTypeCatalogue { CatalogueId = catalogueid, CatalogueTypeId = cataloguetypeid });
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditCatalogue), new { passToken = passToken });
        }


        public IActionResult Tools()
        {
            List<ToolDTO> toolDTOs = _context.Tools.Select(k => new ToolDTO
            {
                ToolId = k.ToolId,
                PassToken = protector.Decode(k.ToolId.ToString()),
                Name = k.Name,
                Description = k.Description.Length > 10 ? k.Description.Substring(0, 10) + ".." : k.Description != null ? k.Description : string.Empty,
                ImageUrl = k.ImageUrl != null ? k.ImageUrl : string.Empty,
                Link = k.Link != null ? k.Link : string.Empty,
                VideoUrl = k.VideoUrl != null ? k.VideoUrl : string.Empty,
                PublicationYear = k.PublicationYear.Value,
                RegistrationDateTime = k.RegistrationDateTime,
                IsActive = k.IsActive == 1 ? true : false
            }).OrderBy(k => k.RegistrationDateTime).ToList();

            return View(toolDTOs);
        }
        public IActionResult AddTool()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddTool([Bind("Name,Description,DescriptionEL,DescriptionPT,Link,ImageUrl,VideoUrl,PublicationYear")] ToolDTO toolDTO)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(AddTool));

            if (string.IsNullOrWhiteSpace(toolDTO.Name) || toolDTO.PublicationYear <= 1980 || toolDTO.PublicationYear >= 2025)
                return RedirectToAction(nameof(AddTool));

            if (toolDTO.VideoUrl != null)
            {
                if (toolDTO.VideoUrl.Contains("?v="))
                    toolDTO.VideoUrl = "https://www.youtube.com/embed/" + toolDTO.VideoUrl.Split(new string[] { "?v=" }, StringSplitOptions.None)[1];
            }

            Tool tool = new Tool
            {
                Name = toolDTO.Name,
                Description = toolDTO.Description != null ? toolDTO.Description : string.Empty,
                DescriptionEl = toolDTO.DescriptionEL != null ? toolDTO.DescriptionEL : string.Empty,
                DescriptionPt = toolDTO.DescriptionPT != null ? toolDTO.DescriptionPT : string.Empty,
                Link = toolDTO.Link != null ? toolDTO.Link : string.Empty,
                ImageUrl = toolDTO.ImageUrl != null ? toolDTO.ImageUrl : string.Empty,
                VideoUrl = toolDTO.VideoUrl != null ? toolDTO.VideoUrl : string.Empty,
                PublicationYear = toolDTO.PublicationYear,
                RegistrationDateTime = DateTime.Now,
                IsActive = 1
            };
            _context.Tools.Add(tool);
            _context.SaveChanges();

            return RedirectToAction(nameof(Tools));
        }
        public async Task<IActionResult> EditTool(string passToken)
        {
            if (passToken == null || passToken == string.Empty) return NotFound();

            var toolSubTypes = _context.SubTypes.Include(k => k.SubTypeTools).ToList();
            IEnumerable<ToolSubTypesSelectionDTO> toolSubTypesSelectionDTOs = toolSubTypes
                .Select(k => new ToolSubTypesSelectionDTO
                {
                    ToolId = int.Parse(protector.Encode(passToken)),
                    ToolSubTypeId = k.SubTypeId,
                    Name = k.Name,
                    IsSelected = k.SubTypeTools
                        .Where(l => l.SubTypeId == k.SubTypeId &&
                            l.ToolId == int.Parse(protector.Encode(passToken))).Count() == 0 ? false : true
                }).ToList();

            try
            {
                var toolDTO = await _context.Tools.Where(k => k.ToolId == int.Parse(protector.Encode(passToken))).Select(k => new ToolDTO
                {
                    ToolId = k.ToolId,
                    PassToken = protector.Decode(k.ToolId.ToString()),
                    Name = k.Name,
                    Description = k.Description != null ? k.DescriptionEl : string.Empty,
                    DescriptionEL = k.DescriptionEl != null ? k.DescriptionEl : string.Empty,
                    DescriptionPT = k.DescriptionPt != null ? k.DescriptionPt : string.Empty,
                    ImageUrl = k.ImageUrl != null ? k.ImageUrl : string.Empty,
                    VideoUrl = k.VideoUrl != null ? k.VideoUrl : string.Empty,
                    PublicationYear = k.PublicationYear.Value,
                    Link = k.Link != null ? k.Link : string.Empty,
                    toolSubTypesSelectionDTOs = toolSubTypesSelectionDTOs.ToList()
                }).FirstOrDefaultAsync();

                return View(toolDTO);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditTool([Bind("Name,Description,DescriptionEL,DescriptionPT,Link,ImageUrl,VideoUrl,PublicationYear")] ToolDTO toolDTO, int toolId)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(EditTool));
            
            if (string.IsNullOrWhiteSpace(toolDTO.Name) || toolDTO.PublicationYear <= 1980 || toolDTO.PublicationYear >= 2025)
                return RedirectToAction(nameof(EditTool));

            var tool = _context.Tools.Where(k => k.ToolId == toolId).FirstOrDefault();
            tool.Name = toolDTO.Name;
            tool.Description = toolDTO.Description != null ? toolDTO.Description : string.Empty;
            tool.DescriptionEl = toolDTO.DescriptionEL != null ? toolDTO.DescriptionEL : string.Empty; ;
            tool.DescriptionPt = toolDTO.DescriptionPT != null ? toolDTO.DescriptionPT : string.Empty; ;
            tool.Link = toolDTO.Link != null ? toolDTO.Link : string.Empty; ;
            tool.ImageUrl = toolDTO.ImageUrl != null ? toolDTO.ImageUrl : string.Empty; ;
            tool.VideoUrl = toolDTO.VideoUrl != null ? toolDTO.VideoUrl : string.Empty; ;
            tool.PublicationYear = toolDTO.PublicationYear;
            _context.Update(tool);
            _context.SaveChanges();

            return RedirectToAction(nameof(Tools));
        }
        public IActionResult DeleteTool(string passToken)
        {
            if (passToken == null) NotFound();

            Tool tool = _context.Tools.Where(k => k.ToolId == int.Parse(protector.Encode(passToken))).FirstOrDefault();
            List<SubTypeTool> subTypeTools = _context.SubTypeTools.Where(k => k.ToolId == int.Parse(protector.Encode(passToken))).ToList();
            List<Rating> ratings = _context.Ratings.Where(k => k.ToolId == int.Parse(protector.Encode(passToken))).ToList();
            List<RatingIp> ratingIps = _context.RatingIps.Where(k => k.ToolId == int.Parse(protector.Encode(passToken))).ToList();

            _context.SubTypeTools.RemoveRange(subTypeTools);
            _context.Ratings.RemoveRange(ratings);
            _context.RatingIps.RemoveRange(ratingIps);
            _context.Tools.Remove(tool);
            _context.SaveChanges();

            return RedirectToAction(nameof(Tools));
        }
        public async Task<IActionResult> UpdateToolType(string passToken, int subtypeid, int toolid)
        {
            if (_context.SubTypeTools.Any(k => k.ToolId == toolid && k.SubTypeId == subtypeid))
            {
                SubTypeTool subTypeTool = _context.SubTypeTools
                    .Where(k => k.ToolId == toolid && k.SubTypeId == subtypeid).FirstOrDefault();
                _context.SubTypeTools.Remove(subTypeTool);
            }
            else
            {
                await _context.SubTypeTools.AddAsync(new SubTypeTool { ToolId = toolid, SubTypeId = subtypeid });
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditTool), new { passToken = passToken });
        }

        public IActionResult RemoveRating(string passToken)
        {
            if (passToken == null) NotFound();

            Tool tool = _context.Tools.Where(k => k.ToolId == int.Parse(protector.Encode(passToken))).FirstOrDefault();
            List<Rating> ratings = _context.Ratings.Where(k => k.ToolId == int.Parse(protector.Encode(passToken))).ToList();
            List<RatingIp> ratingIps = _context.RatingIps.Where(k => k.ToolId == int.Parse(protector.Encode(passToken))).ToList();

            _context.Ratings.RemoveRange(ratings);
            _context.RatingIps.RemoveRange(ratingIps);
            _context.SaveChanges();

            return RedirectToAction(nameof(Tools));
        }
    }
}
