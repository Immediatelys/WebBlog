using Microsoft.AspNetCore.Identity;

namespace WebBlog.Areas.Identity.Models
{
    public class RoleModel : IdentityRole
    {
        public string RoleName { get; set; }
    }
}
