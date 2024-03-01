using App.Areas.Identity.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebBlog.Areas.Identity.Data;

namespace WebBlog.Areas.Identity.Controllers
{
    [Area("identity")]
    [Route("/account")]
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        private readonly IEmailSender _emailSender;


        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<AccountController> logger)
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
        public IActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                if (result.IsCompletedSuccessfully)
                {
                    return LocalRedirect(returnUrl);
                }


            }
            return View(model);

        }

        [Route("/account/logout")]
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        [HttpGet("/account/register")]
        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("/account/register")]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Đã tạo user mới.");

                    // Phát sinh token để xác nhận email
                    // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    // // https://localhost:5001/confirm-email?userId=fdsfds&code=xyz&returnUrl=
                    // var callbackUrl = Url.ActionLink(
                    //     action: nameof(ConfirmEmail),
                    //     values:
                    //         new
                    //         {
                    //             area = "Identity",
                    //             userId = user.Id,
                    //             code = code
                    //         },
                    //     protocol: Request.Scheme);

                    // await _emailSender.SendEmailAsync(model.Email,
                    //     "Xác nhận địa chỉ email",
                    //     @$"Bạn đã đăng ký tài khoản trên RazorWeb, 
                    //        hãy <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>bấm vào đây</a> 
                    //        để kích hoạt tài khoản.");

                    /*   if (_userManager.Options.SignIn.RequireConfirmedAccount)
                       {
                           return LocalRedirect(Url.Action(nameof(RegisterConfirmation)));
                       }
                       else
                       {
                           await _signInManager.SignInAsync(user, isPersistent: false);
                           return LocalRedirect(returnUrl);
                       }*/

                }

                ModelState.AddModelError("Register Err", "Invalid user");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}
