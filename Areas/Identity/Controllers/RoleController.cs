using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBlog.Areas.Identity.Data;
using WebBlog.Data;

namespace WebBlog.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Route("/role")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly WebBlogDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        [TempData]
        public string StatusMessage { get; set; }
        public RoleController(RoleManager<IdentityRole> roleManager, WebBlogDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("/role/home")]
        public IActionResult Index()
        {


            return View(_userManager);
        }

        public async Task<IActionResult> CreateRole()
        {
            var rolesNames = typeof(RoleName).GetFields().ToList();
            foreach (var r in rolesNames)
            {
                var roleName = (string)r.GetRawConstantValue();
                var rFound = await _roleManager.FindByNameAsync(roleName);

                if (rFound != null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }

            }

            // create admin
            var userAdmin = await _userManager.FindByEmailAsync("admin");
            if (userAdmin == null)
            {
                userAdmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",

                };
                await _userManager.CreateAsync(userAdmin, "Abc@123");
                await _userManager.AddToRoleAsync(userAdmin, RoleName.Admin);
            }

            return RedirectToAction("Index");
        }


    }
}
