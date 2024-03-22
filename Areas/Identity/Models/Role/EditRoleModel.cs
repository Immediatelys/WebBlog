using Microsoft.AspNetCore.Identity;

namespace WebBlog.Areas.Identity.Models.Role
{
    public class EditRoleModel
    {
        public string Name { get; set; }
        public IdentityRole Role { get; set; }
    }
}
