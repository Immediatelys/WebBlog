using Microsoft.AspNetCore.Identity;
using WebBlog.Areas.Identity.Data;
namespace WebBlog.Areas.Identity.Models.User;

public class UserListModel
{
    public int totalUsers { get; set; }
    public int countPages { get; set; }

    public int ITEMS_PER_PAGE { get; set; } = 10;

    public int currentPage { get; set; }

    public List<UserAndRole> users { get; set; }

    public AppUser user { get; set; }

    public class UserAndRole : AppUser
    {
        public string rolesNames { get; set; }
    }
}