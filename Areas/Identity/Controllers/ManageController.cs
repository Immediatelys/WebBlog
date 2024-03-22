using Microsoft.AspNetCore.Mvc;

namespace WebBlog.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Route("/manage/[action]")]
    public class ManageController : Controller
    {

        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }

        // [HttpPost("/manage/index")]
        // public IActionResult Index()
        // {
        //     return View();
        // }
    }
}
