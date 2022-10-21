using Common;
using Infrastructure.Entities;
using LiveIT.Application;
using LiveIT.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System.Linq;
using System.Threading.Tasks;

namespace ARI.Controllers
{
    public class AuthController : Controller
    {
        private readonly liveitContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly GoogleReCAPTCHAService _googleReCAPTCHAService;
        private readonly IEmailService _emailService;
        private readonly IHostingEnvironment _environment;

        public AuthController(liveitContext ariContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            GoogleReCAPTCHAService googleReCAPTCHAService,
            IEmailService emailService,
            IHostingEnvironment IHostingEnvironment)
        {
            _context = ariContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _googleReCAPTCHAService = googleReCAPTCHAService;
            _environment = IHostingEnvironment;
            _emailService = emailService;
        }

        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Stakeholders", "Admin");
            }
            else
                return View(nameof(Login));
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Stakeholders", "Admin");
            }
            else
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password,Token")] IdentityLoginUserDTO identityUserDto)
        {
            //Google reCAPTCHA validation
            var _GoogleReCAPTCHA = _googleReCAPTCHAService.VerifyReCAPTCHA(identityUserDto.Token);

            if (!_GoogleReCAPTCHA.Result.success && _GoogleReCAPTCHA.Result.score <= 0.5)
                return AuthenticationError(View(), string.Empty, "Please try again later. 124");

            string hash = IdentityConversions.MD5Hash(identityUserDto.Password);
            var user = _context.Users.Where(k => k.Email == identityUserDto.Email.Trim() && k.PasswordHash == hash).FirstOrDefault();

            // User doesn't exist or invalid model
            if (user == null || !ModelState.IsValid)
                return AuthenticationError(View(), string.Empty, "Please try again later. 141");

            //Find if user already exist in the userManager
            var _identityUser = await _userManager.FindByEmailAsync(user.Email);
            if (_identityUser == null)
            {
                var userManagerResult = await _userManager.CreateAsync(new IdentityUser
                {
                    Id = user.IdentityId,
                    Email = identityUserDto.Email.Trim(),
                    UserName = identityUserDto.Email.Trim(),
                    EmailConfirmed = true
                }, identityUserDto.Password);

                //Couldn't create user manager
                if (!userManagerResult.Succeeded)
                    return AuthenticationError(View(), string.Empty, "Please try again later. 135");
            }
            else
            {
                if (_identityUser.PasswordHash == null)
                {
                    _identityUser.PasswordHash = identityUserDto.Password;
                    var userManagerResult = await _userManager.UpdateAsync(_identityUser);

                    //Couldn't update user manager
                    if (!userManagerResult.Succeeded)
                        return AuthenticationError(View(), string.Empty, "Please try again later. 122");
                }
            }

            var __identityUser = await _userManager.FindByEmailAsync(user.Email);
            var signInResult = await _signInManager.PasswordSignInAsync(identityUserDto.Email.Trim(), identityUserDto.Password, false, false);

            //Couldn't sign to signin manager
            if (!signInResult.Succeeded)
                return AuthenticationError(View(), string.Empty, "Please try again later. 118");

            return RedirectToAction("Stakeholders", "Admin");
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        private ViewResult AuthenticationError(ViewResult viewResult, string key, string message)
        {
            ModelState.AddModelError(key, message);
            return viewResult;
        }
    }
}
