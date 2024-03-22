using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebBlog.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    public string Sex { get; set; }

    public int Age { get; set; }

    public string HomeAdress { get; set; }

    public string EmailConfirmed { get; set; }

    public int AccessFailedCount { get; set; }

}

