using Application.Security;
using LiveIT.Common;
using LiveIT.Models;
using LiveIT.Toolkit.Models;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace LiveIT.Controllers
{
    //[Authorize(Roles = "IT,Υπεύθυνος Ροών Εταιρίας")]
    public class ToolkitController : Controller
    {
        private readonly liveitContext _context;
        //private IUserRepository _userRepository;
        private readonly IHostingEnvironment _environment;
        private readonly CustomIDataProtection protector;

        public ToolkitController(liveitContext context, IHostingEnvironment IHostingEnvironment, CustomIDataProtection customIDataProtection)
        {
            _context = context;
            _environment = IHostingEnvironment;
            protector = customIDataProtection;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(SearchToolkit));
        }

        public IActionResult SearchToolkit()
        {
            AccessibilityMenuIPDTO accessibilityMenuIPDTO = new AccessibilityMenuIPDTO();
            string ipFromExtensionMethod = HttpContext.GetRemoteIPAddress().ToString();
            if (_context.AccesibilityMenyIps.Where(k => k.RemoteAddress == ipFromExtensionMethod).Count() > 0)
            {
                int accessibilityMenuId = _context.AccesibilityMenyIps.Where(k => k.RemoteAddress == ipFromExtensionMethod).FirstOrDefault().AccesibilityMenuId;
                accessibilityMenuIPDTO = _context.AccessibilityMenus.Where(k => k.AccesibilityMenuId == accessibilityMenuId).Select(k => new AccessibilityMenuIPDTO
                {
                    IP = ipFromExtensionMethod,
                    LanguageId = k.LanguageCode,
                    Colored = k.Colored,
                    Fonts = k.Fonts,
                    Contrast = k.Contrast,
                    Underline = k.Underline
                }).FirstOrDefault();
            }

            var countries = _context.Countries.ToList();
            var countryListItems = new List<SelectListItem>();
            string countryText = (accessibilityMenuIPDTO.LanguageId == 0 ? "Country" : (accessibilityMenuIPDTO.LanguageId == 1 ? "Χώρα" : "País"));
            countryListItems.Add(new SelectListItem { Text = "- "+ countryText + "- ", Value = "-" });
            foreach (var country in countries)
                countryListItems.Add(new SelectListItem { Text = country.Name, Value = country.CountryId.ToString() });

            var stakeholderTypes = _context.StakeholderTypes.ToList();
            var stakeholderTypeListItems = new List<SelectListItem>();
            string typeText = (accessibilityMenuIPDTO.LanguageId == 0 ? "Type" : (accessibilityMenuIPDTO.LanguageId == 1 ? "Τύπος" : "Tipo"));
            stakeholderTypeListItems.Add(new SelectListItem { Text = "- " + typeText + "- ", Value = "-" });
            foreach (var stakeholderType in stakeholderTypes)
                stakeholderTypeListItems.Add(new SelectListItem { Text = stakeholderType.Name, Value = stakeholderType.StakeholderTypeId.ToString() });

            var catalogueTypes = _context.CatalogueTypes.ToList();
            var catalogueTypeListItems = new List<SelectListItem>();
            catalogueTypeListItems.Add(new SelectListItem { Text = "- " + typeText + "- ", Value = "-" });
            foreach (var catalogueType in catalogueTypes)
                catalogueTypeListItems.Add(new SelectListItem { Text = catalogueType.Name, Value = catalogueType.CatalogueTypeId.ToString() });

            var subCategoryListItems = new List<SelectListItem>();
            string subCategoryText = (accessibilityMenuIPDTO.LanguageId == 0 ? "Subcategory" : (accessibilityMenuIPDTO.LanguageId == 1 ? "Υποκατηγορία" : "subcategoria"));
            subCategoryListItems.Add(new SelectListItem { Text = "- " + subCategoryText + "- ", Value = "-" });

            var categories = _context.Types.ToList();
            var categoryListItems = new List<SelectListItem>();
            string categoryText = (accessibilityMenuIPDTO.LanguageId == 0 ? "Category" : (accessibilityMenuIPDTO.LanguageId == 1 ? "Κατηγορία" : "Categoria"));
            categoryListItems.Add(new SelectListItem { Text = "- " + categoryText + "- ", Value = "-" });
            foreach (var category in categories)
            {
                if (accessibilityMenuIPDTO.LanguageId == 0)
                    categoryListItems.Add(new SelectListItem { Text = category.Name, Value = category.TypeId.ToString() });
                else if (accessibilityMenuIPDTO.LanguageId == 1)
                    categoryListItems.Add(new SelectListItem { Text = category.NameEl, Value = category.TypeId.ToString() });
                else if (accessibilityMenuIPDTO.LanguageId == 2)
                    categoryListItems.Add(new SelectListItem { Text = category.NamePt, Value = category.TypeId.ToString() });
            }

            List<TranslationDTO> translationDTOs = new List<TranslationDTO>();
            if (accessibilityMenuIPDTO.LanguageId == 0)
                translationDTOs = _context.Translations.Select(k => new TranslationDTO { TranslationId = k.TranslationId, Text = k.TextEn }).ToList();
            else if (accessibilityMenuIPDTO.LanguageId == 1)
                translationDTOs = _context.Translations.Select(k => new TranslationDTO { TranslationId = k.TranslationId, Text = k.TextEl }).ToList();
            else if (accessibilityMenuIPDTO.LanguageId == 2)
                translationDTOs = _context.Translations.Select(k => new TranslationDTO { TranslationId = k.TranslationId, Text = k.TextPt }).ToList();

            return View(new ToolkitDTO
            {
                SelectedStakeholderType = "-",
                StakeholderTypeListItems = stakeholderTypeListItems,
                SelectedCatalogueType = "-",
                CatalogueTypeListItems = catalogueTypeListItems,
                SelectedCountry = "-",
                CountryListItems = countryListItems,
                SelectedCategory = "-",
                CategoryListItems = categoryListItems,
                SelectedSubCategory = "-",
                SubCategoryListItems = subCategoryListItems,
                SelectedAccessibility = "-",
                SelectedPlatform = "-",
                AccessibilityMenuIPDTO = accessibilityMenuIPDTO,
                TranslationDTOs = translationDTOs
            });
        }

        [HttpGet(Name = "Get subcategories")]
        [AllowAnonymous]
        public JsonResult GetSubCategories(int categoryId)
        {
            List<SubType> typeSubTypes = _context.TypeSubTypes.Include(k => k.SubType).Where(k => k.TypeId == categoryId).Select(k => k.SubType).ToList();

            string ipFromExtensionMethod = HttpContext.GetRemoteIPAddress().ToString();
            if (_context.AccesibilityMenyIps.Where(k => k.RemoteAddress == ipFromExtensionMethod).Count() == 0)
            {
                AccessibilityMenu accessibilityMenu = new AccessibilityMenu();
                accessibilityMenu.Fonts = 0;
                accessibilityMenu.Contrast = 0;
                accessibilityMenu.Underline = 0;
                accessibilityMenu.Colored = 0;
                accessibilityMenu.LanguageCode = 0;
                _context.AccessibilityMenus.Add(accessibilityMenu);
                _context.SaveChanges();

                //get last
                var lastAcc = _context.AccessibilityMenus.OrderByDescending(k=> k.AccesibilityMenuId).FirstOrDefault();
                AccesibilityMenyIp accesibilityMenyIp = new AccesibilityMenyIp();
                accesibilityMenyIp.AccesibilityMenuId = lastAcc.AccesibilityMenuId;
                accesibilityMenyIp.RemoteAddress = ipFromExtensionMethod;
                _context.AccesibilityMenyIps.Add(accesibilityMenyIp);
                _context.SaveChanges();
            }

            int accessibilityMenuId = _context.AccesibilityMenyIps.Where(k => k.RemoteAddress == ipFromExtensionMethod).FirstOrDefault().AccesibilityMenuId;
            int languageId = _context.AccessibilityMenus.Where(k => k.AccesibilityMenuId == accessibilityMenuId).FirstOrDefault().LanguageCode;

            string subCategoryText = (languageId == 0 ? "Subcategory" : (languageId == 1 ? "Υποκατηγορία" : "subcategoria"));
            var typeSubTypeListItems = new List<SelectListItem>().Append(new SelectListItem { Text = "- " + subCategoryText + "- ", Value = "0" }).ToList();
            foreach (var item in typeSubTypes)
            {
                if(languageId == 0)
                    typeSubTypeListItems.Add(new SelectListItem { Text = item.Name, Value = item.SubTypeId.ToString() });
                else if(languageId == 1)
                    typeSubTypeListItems.Add(new SelectListItem { Text = item.NameEl, Value = item.SubTypeId.ToString() });
                else if (languageId == 2)
                    typeSubTypeListItems.Add(new SelectListItem { Text = item.NamePt, Value = item.SubTypeId.ToString() });
            }

            return Json(typeSubTypeListItems);
        }

        [HttpGet(Name = "Get stakeholder info")]
        public JsonResult GetStakeholders(string countryId, string stakeholderTypeId, string orderId, string inputField)
        {
            if (countryId == null || !int.TryParse(countryId, out int value) &&
                stakeholderTypeId == null || !int.TryParse(stakeholderTypeId, out int value1) &&
                orderId == null || !int.TryParse(orderId, out int value2))
                return null;

            List<Stakeholder> stakeholders = new List<Stakeholder>();
            //Country
            if(countryId == "0")
                stakeholders = _context.Stakeholders.Include(k => k.Country).ToList();
            else 
                stakeholders = _context.Stakeholders.Include(k => k.Country).Where(k => k.Country.CountryId == int.Parse(countryId)).ToList();


            //Type
            List<int> stakeholderIds = new List<int>();
            if (stakeholderTypeId == "0")
                stakeholderIds = _context.StakeholderTypeStakeholders.Select(k=>k.StakeholderId).ToList();
            else
                stakeholderIds = _context.StakeholderTypeStakeholders.Where(k=>k.StakeholderTypeId == int.Parse(stakeholderTypeId)).Select(k=>k.StakeholderId).ToList();

            stakeholders = stakeholders.Where(k => stakeholderIds.Contains(k.StakeholderId)).ToList();

            List<StakeholderDTO> stakeholdersDTOs = new List<StakeholderDTO>();
            foreach (var stakeholder in stakeholders)
            {
                stakeholdersDTOs.Add(new StakeholderDTO
                {
                    StakeholderId = stakeholder.StakeholderId,
                    Name = stakeholder.Name,
                    StakeHolderTypeName = _context.StakeholderTypeStakeholders.Include(k=>k.StakeholderType).Where(k=> k.StakeholderId == stakeholder.StakeholderId).Select(k=>k.StakeholderType.Name).ToList().Aggregate((x,y) => x + "," + y).ToString(),
                    Website = stakeholder.Website.Contains("http") ? stakeholder.Website : "https://" + stakeholder.Website,
                    FacebookUrl = stakeholder.FacebookUrl,
                    TwitterUrl = stakeholder.TwitterUrl,
                    YoutubeUrl = stakeholder.YoutubeUrl,
                    LinkedInUrl = stakeholder.LinkedinUrl,
                    Email = stakeholder.Email,
                    SelectedCountry = stakeholder.Country.Name
                });
            }

            if (!String.IsNullOrEmpty(inputField))
            {
                stakeholdersDTOs = stakeholdersDTOs.Where(k => k.StakeHolderTypeName.Contains(inputField)
                || k.SelectedCountry.Contains(inputField) || k.Name.Contains(inputField)).ToList();
            }

            if (int.Parse(orderId) == 0)
                stakeholdersDTOs = stakeholdersDTOs.OrderBy(k => k.Name).ToList();
            else
                stakeholdersDTOs = stakeholdersDTOs.OrderBy(k => k.SelectedCountry).ToList();

            return Json(stakeholdersDTOs);
        }

        [HttpGet(Name = "Get catalogue info")]
        public JsonResult GetCatalogues(string catalogueTypeId)
        {
            if (!int.TryParse(catalogueTypeId, out int value1))
                return null;

            List<Catalogue> catalogues  = _context.Catalogues.ToList();

            //Type
            List<int> catalogueTypeIds = new List<int>();
            if (catalogueTypeId == "0")
                catalogueTypeIds = _context.CatalogueTypeCatalogues.Select(k => k.CatalogueId).ToList();
            else
                catalogueTypeIds = _context.CatalogueTypeCatalogues.Where(k => k.CatalogueTypeId == int.Parse(catalogueTypeId)).Select(k => k.CatalogueId).ToList();

            catalogues = catalogues.Where(k => catalogueTypeIds.Contains(k.CatalogueId)).ToList();

            List<CatalogueDTO> cataloguesDTOs = new List<CatalogueDTO>();
            foreach (var catalogue in catalogues)
            {
                cataloguesDTOs.Add(new CatalogueDTO
                {
                    CatalogueId = catalogue.CatalogueId,
                    Name = string.IsNullOrWhiteSpace(catalogue.Name) ? "-" : catalogue.Name,
                    Author = string.IsNullOrWhiteSpace(catalogue.Author)? "-": catalogue.Author,
                    Year = catalogue.Year,
                    TargetPopulation = string.IsNullOrWhiteSpace(catalogue.TargetPopulation) ? "-" : catalogue.TargetPopulation,
                    Theme = string.IsNullOrWhiteSpace(catalogue.Theme) ? "-" : catalogue.Theme,
                    Country = string.IsNullOrWhiteSpace(catalogue.Country) ? "-" : catalogue.Country,
                    Link = string.IsNullOrWhiteSpace(catalogue.Link) ? "-" : catalogue.Link,
                });
            }

            return Json(cataloguesDTOs);
        }

        [HttpGet(Name = "Get Pallet info")]
        public JsonResult GetTools(string subcategoryId, string remoteIp)
        {
            if (subcategoryId == null || !int.TryParse(subcategoryId, out int value))
                return null;

            var tools = _context.SubTypeTools.Include(k=>k.Tool).Where(k => k.SubTypeId == int.Parse(subcategoryId) && k.Tool.IsActive == 1).ToList();

            List<Rating> ratings = _context.Ratings.Where(k => tools.Select(k => k.ToolId).Contains(k.ToolId)).ToList();

            List<RatingDTO> ratingDTOs = new List<RatingDTO>();
            foreach (var tool in tools)
            {
                if (ratings.Any(k => k.ToolId == tool.ToolId))
                {
                    ratingDTOs.Add(new RatingDTO
                    {
                        ToolId = tool.ToolId,
                        AverageScore = (ratings.Where(k => k.ToolId == tool.ToolId).Average(x => x.Usefulness) + ratings.Where(k => k.ToolId == tool.ToolId).Average(x => x.Innovation) + ratings.Where(k => k.ToolId == tool.ToolId).Average(x => x.Safety)) / 3,
                        Usefulness = ratings.Where(k => k.ToolId == tool.ToolId).Average(x => x.Usefulness),
                        Innovation = ratings.Where(k => k.ToolId == tool.ToolId).Average(x => x.Innovation),
                        Safety = ratings.Where(k => k.ToolId == tool.ToolId).Average(x => x.Safety),
                        Votes = ratings.Where(k => k.ToolId == tool.ToolId).Count()
                    });
                }
                else
                {
                    ratingDTOs.Add(new RatingDTO
                    {
                        ToolId = tool.ToolId,
                        AverageScore = 0,
                        Usefulness = 0,
                        Innovation = 0,
                        Safety = 0,
                    });
                }
            }

            string ipFromExtensionMethod = HttpContext.GetRemoteIPAddress().ToString();
            if (_context.AccesibilityMenyIps.Where(k => k.RemoteAddress == ipFromExtensionMethod).Count() == 0)
                return Json("");

            int accessibilityMenuId = _context.AccesibilityMenyIps.Where(k => k.RemoteAddress == ipFromExtensionMethod).FirstOrDefault().AccesibilityMenuId;
            int languageId = _context.AccessibilityMenus.Where(k => k.AccesibilityMenuId == accessibilityMenuId).FirstOrDefault().LanguageCode;

            var toolIds = tools.Select(l => l.ToolId).ToList();
            List<ToolDTO> toolDTOs = _context.Tools.Where(k => toolIds.Contains(k.ToolId)).Select(k => new ToolDTO
            {
                ToolId = k.ToolId,
                Name = k.Name,
                Description = ((languageId == 0) ? k.Description : ((languageId == 1) ? k.DescriptionEl : k.DescriptionPt)),
                ImageUrl = string.IsNullOrEmpty(k.ImageUrl) ? "/images/dummy.png" : k.ImageUrl,
                Link = k.Link,
                VideoUrl = k.VideoUrl,
                PublicationYear = k.PublicationYear.Value,
                RegistrationDateTime = k.RegistrationDateTime,
            }).ToList();

            foreach (var toolDTO in toolDTOs)
            {
                toolDTO.RatingDTO = ratingDTOs.Where(x => x.ToolId == toolDTO.ToolId).FirstOrDefault();

                if (toolDTO.VideoUrl.Contains("?v="))
                    toolDTO.VideoUrl = "https://www.youtube.com/embed/" + toolDTO.VideoUrl.Split(new string[] { "?v=" }, StringSplitOptions.None)[1];
                else
                    toolDTO.VideoUrl = "";
            }

            toolDTOs = toolDTOs.OrderByDescending(k => k.RatingDTO.AverageScore).ToList();

            List<int> ratingToolIds = _context.RatingIps.Where(k => k.RemoteAddress == remoteIp).Select(k=>k.ToolId).ToList();

            foreach (var toolDto in toolDTOs)
                if (ratingToolIds.Contains(toolDto.ToolId))
                    toolDto.hasRating = true;

            return Json(toolDTOs);
        }

        [HttpPost]
        public JsonResult RateTool(IFormCollection data)
        {
            if (ModelState.IsValid)
            {

                var radioValue = int.Parse(data["radioValue"].ToString());
                var radio1Value = int.Parse(data["radio1Value"].ToString());
                var radio2Value = int.Parse(data["radio2Value"].ToString());
                var toolValue = int.Parse(data["toolValue"].ToString());
                var remoteIp = HttpContext.GetRemoteIPAddress().ToString();

                var remoteIpsToDelete = _context.RatingIps.Where(k => k.SubmittedDateTime >= DateTime.Now.AddDays(14)).ToList();
                _context.RemoveRange(remoteIpsToDelete);
                _context.SaveChanges();

                if(_context.RatingIps.Where(k => k.ToolId == toolValue && k.RemoteAddress == remoteIp).Count() == 1)
                    return new JsonResult("ok");

                Rating rating = new Rating
                {
                    ToolId = toolValue,
                    Usefulness = radioValue,
                    Innovation = radio1Value,
                    Safety = radio2Value
                };

                RatingIp ratingIp = new RatingIp
                {
                    ToolId = toolValue,
                    RemoteAddress = remoteIp,
                    SubmittedDateTime = DateTime.Now,
                };

                _context.RatingIps.Add(ratingIp);
                _context.Ratings.Add(rating);
                _context.SaveChanges();

                return new JsonResult("ok");
            }
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchToolkit([Bind("SelectedCategory, SelectedAction, SelectedAccessibility, SelectedPlatform")] ToolkitDTO toolkitDTO)
        {
            return View();
        }

        public IActionResult ToolkitInfo(string toolId)
        {
            if (!int.TryParse(toolId, out int value))
                return null;

            List<Rating> rating = _context.Ratings.Where(k => k.ToolId == int.Parse(toolId)).ToList();

            RatingDTO ratingDTO = new RatingDTO();
            if (rating.Count != 0)
            {
                ratingDTO = new RatingDTO
                {
                    AverageScore = (rating.Average(x => x.Usefulness) + rating.Average(x => x.Innovation) + rating.Average(x => x.Safety)) / 3,
                    Usefulness = rating.Average(x => x.Usefulness),
                    Innovation = rating.Average(x => x.Innovation),
                    Safety = rating.Average(x => x.Safety),
                    Votes = rating.Count
                };
            }
            else
            {
                ratingDTO = new RatingDTO
                {
                    AverageScore = 0,
                    Usefulness = 0,
                    Innovation = 0,
                    Safety = 0,
                };
            }

            AccessibilityMenuIPDTO accessibilityMenuIPDTO = new AccessibilityMenuIPDTO();
            string ipFromExtensionMethod = HttpContext.GetRemoteIPAddress().ToString();
            if (_context.AccesibilityMenyIps.Where(k => k.RemoteAddress == ipFromExtensionMethod).Count() > 0)
            {
                int accessibilityMenuId = _context.AccesibilityMenyIps.Where(k => k.RemoteAddress == ipFromExtensionMethod).FirstOrDefault().AccesibilityMenuId;
                accessibilityMenuIPDTO = _context.AccessibilityMenus.Where(k => k.AccesibilityMenuId == accessibilityMenuId).Select(k => new AccessibilityMenuIPDTO
                {
                    IP = ipFromExtensionMethod,
                    LanguageId = k.LanguageCode,
                    Colored = k.Colored,
                    Fonts = k.Fonts,
                    Contrast = k.Contrast,
                    Underline = k.Underline
                }).FirstOrDefault();
            }

            List<TranslationDTO> translationDTOs = new List<TranslationDTO>();
            if (accessibilityMenuIPDTO.LanguageId == 0)
                translationDTOs = _context.Translations.Select(k => new TranslationDTO { TranslationId = k.TranslationId, Text = k.TextEn }).ToList();
            else if (accessibilityMenuIPDTO.LanguageId == 1)
                translationDTOs = _context.Translations.Select(k => new TranslationDTO { TranslationId = k.TranslationId, Text = k.TextEl }).ToList();
            else if (accessibilityMenuIPDTO.LanguageId == 2)
                translationDTOs = _context.Translations.Select(k => new TranslationDTO { TranslationId = k.TranslationId, Text = k.TextPt }).ToList();

            ToolDTO toolDTO = _context.Tools.Where(k => k.ToolId == int.Parse(toolId)).Select(k => new ToolDTO()
            {
                Name = k.Name,
                Description = ((accessibilityMenuIPDTO.LanguageId == 0) ? k.Description : ((accessibilityMenuIPDTO.LanguageId == 1) ? k.DescriptionEl : k.DescriptionPt)),
                Link = k.Link,
                ImageUrl = k.ImageUrl,
                VideoUrl = k.VideoUrl + "?feature=oembed",
                PublicationYear = k.PublicationYear.Value,
                RegistrationDateTime = k.RegistrationDateTime,
                RatingDTO = ratingDTO,
                AccessibilityMenuIPDTO = accessibilityMenuIPDTO,
                TranslationDTOs = translationDTOs
            }).FirstOrDefault();

            if (toolDTO.VideoUrl.Contains("?v="))
                toolDTO.VideoUrl = "https://www.youtube.com/embed/" + toolDTO.VideoUrl.Split(new string[] { "?v=" }, StringSplitOptions.None)[1];

            return View(toolDTO);
        }

        public IActionResult UpdateAccessibilityMenu(string type, int typeId)
        {
            string ipFromExtensionMethod = HttpContext.GetRemoteIPAddress().ToString();

            if ((type == "fonts" || type == "contrast" || type == "underline" || type == "colored" || type == "languageId") && ipFromExtensionMethod != string.Empty)
            {
                AccesibilityMenyIp accesibilityMenyIp = _context.AccesibilityMenyIps.Where(k => k.RemoteAddress == ipFromExtensionMethod).FirstOrDefault();
                if (accesibilityMenyIp == null)
                {
                    AccessibilityMenu accessibilityMenu = new AccessibilityMenu();
                    if (type == "fonts" && typeId == 1) accessibilityMenu.Fonts = 1;
                    if (type == "contrast" && typeId == 1) accessibilityMenu.Contrast = 1;
                    if (type == "underline" && typeId == 1) accessibilityMenu.Underline = 1;
                    if (type == "colored" && typeId == 1) accessibilityMenu.Colored = 1;
                    if (type == "languageId" && typeId >= 0 && typeId <= 2)accessibilityMenu.LanguageCode = typeId;

                    _context.AccessibilityMenus.Add(accessibilityMenu);
                    _context.SaveChanges();

                    var _accesibilityMenuId = _context.AccessibilityMenus.OrderByDescending(k=>k.AccesibilityMenuId).FirstOrDefault().AccesibilityMenuId;

                    accesibilityMenyIp = new AccesibilityMenyIp();
                    accesibilityMenyIp.RemoteAddress = ipFromExtensionMethod;
                    accesibilityMenyIp.AccesibilityMenuId = _accesibilityMenuId;
                    //accesibilityMenyIp.AccessibilityMenuIp = accessibilityMenu;
                    _context.AccesibilityMenyIps.Add(accesibilityMenyIp);
                    _context.SaveChanges();
                }
                else
                {
                    AccessibilityMenu accessibilityMenu = _context.AccessibilityMenus.Where(k => k.AccesibilityMenuId == accesibilityMenyIp.AccesibilityMenuId).FirstOrDefault();
                    if (type == "fonts")
                        accessibilityMenu.Fonts = (typeId == 1)? 1 : 0;
                    if (type == "contrast")
                        accessibilityMenu.Contrast = (typeId == 1) ? 1 : 0;
                    if (type == "underline")
                        accessibilityMenu.Underline = (typeId == 1) ? 1 : 0;
                    if (type == "colored")
                        accessibilityMenu.Colored = (typeId == 1) ? 1 : 0;
                    if (type == "languageId" && typeId >= 0 && typeId <= 2)
                        accessibilityMenu.LanguageCode = typeId;

                    _context.AccessibilityMenus.Update(accessibilityMenu);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(SearchToolkit));
        }

        private ViewResult AuthenticationError(ViewResult viewResult, string key, string message)
        {
            ModelState.AddModelError(key, message);
            return viewResult;
        }

    }
    public static class HttpContextExtensions
    {
        public static IPAddress GetRemoteIPAddress(this HttpContext context, bool allowForwarded = true)
        {
            if (allowForwarded)
            {
                string header = (context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault());
                if (IPAddress.TryParse(header, out IPAddress ip))
                {
                    return ip;
                }
            }
            return context.Connection.RemoteIpAddress;
        }
    }
}
