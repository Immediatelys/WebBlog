using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebBlog.Areas.Identity.Data;
using WebBlog.Areas.Identity.Models;
using WebBlog.Areas.Identity.Models.Role;
using WebBlog.Data;

namespace WebBlog.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Route("/role/[action]")]
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var r = _roleManager.Roles.OrderBy( r => r.Name).ToList();
            var roles = new List<RoleModel>();
            foreach(var _r in r )
            {
                var rm = new RoleModel()
                {
                    Name = _r.Name,
                    Id = _r.Id,
                };
                roles.Add( rm );
            }
            return View(roles);

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> SeedData()
        {
            var rolesNames = typeof(RoleName).GetFields().ToList();
            foreach (var r in rolesNames)
            {
                var roleName = (string)r.GetRawConstantValue();
                var rFound = await _roleManager.FindByNameAsync(roleName);

                if (rFound == null)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }

            }

            // create admin
            AppUser userAdmin = await _userManager.FindByEmailAsync("admin");
            if (userAdmin == null)
            {
                userAdmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",

                };
                var resultCreateAdmin = await _userManager.CreateAsync(userAdmin, "admin123");
                if(resultCreateAdmin.Succeeded)
                {
                    return RedirectToAction("Index");
                } else
                {
                    ModelState.AddModelError("", "Deo tao dc");
                }
                await _userManager.AddToRoleAsync(userAdmin, RoleName.Admin);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet("/role/edit")]
        public async Task<IActionResult> Edit(string roleid, [Bind("Name")] EditRoleModel model)
        {
            if (roleid == null) return NotFound("Không tìm thấy role");
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound("Không tìm thấy role");
            }

            model.Name = role.Name;
            model.Role = role;
            ModelState.Clear();

            return View(model);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(string roleid)
        {
            if (roleid == null) return NotFound("KHông tìm thấy role");
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound("KHông tìm thấy role");
            }
            var result = await _roleManager.DeleteAsync(role);
            if(result.Succeeded)
            {
                
                return RedirectToAction("Index");
            } else
            {
                ModelState.AddModelError("1","Delete unsuccess");
            }

            return View(role);
        }


    }
}
