using Microsoft.AspNetCore.Mvc;

namespace WebBlog.Areas.Identity.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
