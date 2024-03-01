using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace App.Areas.Identity.Models.UserViewModels;

public class UserViewModel
{

    public string Id { get; set; }

    public string UserName { get; set; }

    public int Age { get; set; }

    public string Email { get; set; }


}
