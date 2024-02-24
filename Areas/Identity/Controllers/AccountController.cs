using Microsoft.AspNetCore.Mvc;

namespace WebBlog.Areas.Identity.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
