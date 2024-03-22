using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebBlog.Areas.Identity.Models.User;

public class UserViewModel
{

    public string Id { get; set; }

    public string UserName { get; set; }

    public int Age { get; set; }

    public string Email { get; set; }


}
