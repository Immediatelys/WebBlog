using Microsoft.AspNetCore.Mvc;

namespace WebBlog.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Route("/manage")]
    public class ManageController : Controller
    {

        [HttpGet("/manage/index")]
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
