using App.Areas.Identity.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebBlog.Areas.Identity.Data;
using WebBlog.Data;

namespace WebBlog.Areas.Identity.Controllers
{
    [Area("identity")]
    [Route("/account")]
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        private readonly WebBlogDbContext _context;
        private readonly IEmailSender _emailSender;


        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<AccountController> logger,
            WebBlogDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet("/account/login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        /*        [HttpPost("/account/login")]
                public Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
                {

                    returnUrl ??= Url.Content("~/");
                    ViewData["ReturnUrl"] = returnUrl;
                    if (ModelState.IsValid)
                    {

                        var result = _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                        // Tìm UserName theo Email, đăng nhập lại
                        if ((!result.IsCompletedSuccessfully))
                        {
                            var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
                            if (user != null)
                            {
                                result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);
                            }
                        }

                        if (result.Succeeded)
                        {
                            _logger.LogInformation(1, "User logged in.");
                            return LocalRedirect(returnUrl);
                        }


                        if (result.IsLockedOut)
                        {
                            _logger.LogWarning(2, "Tài khoản bị khóa");
                            return View("Lockout");
                        }
                        else
                        {
                            ModelState.AddModelError("Không đăng nhập được.");
                            return View(model);
                        }
                        return View(model);
                    }
                }*/

        [HttpPost("/account/login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            if (ModelState.IsValid)
            {
                /*AppUser signInuser = _userManager.FindByEmailAsync(model.Email)*/
                ;
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false);
                    /* if ((!result.Succeeded))
                     {
                         var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
                         if (user != null)
                         {
                             result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);
                         }
                     }*/

                    if (result.Succeeded)
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        _logger.LogWarning("2", "Login false");
                    }
                }
                else
                {
                    _logger.LogError("1", "user not valid");
                }

            }
            return View(model);

        }

        [HttpPost("/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logout");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/account/register")]
        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("/account/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                /* var user = new AppUser { UserName = model.UserName, Email = model.Email, HomeAdress = model.HomeAdress, PasswordHash = model.Password, Age = model.Age };*/
                var user = new AppUser { UserName = model.UserName, Email = model.Email, HomeAdress = model.HomeAdress, Age = model.Age };
                /*var user = new AppUser { UserName = model.UserName, Email = model.Email, Password = model.Password  };*/
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Đã tạo user mới.");
                    /* await _signInManager.SignInAsync(user, false);*/
                    return RedirectToAction("Login");
                }
                else
                {
                    _logger.LogInformation("Register failed");
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            else
            {
                ModelState.AddModelError("Error", "Modelstate is not valid");
            }
            // Lỗi khi không tạo đc user hoặc dữ liệu không hợp lệ
            return View(model);
        }

    }
}
