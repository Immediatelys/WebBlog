using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace App.Areas.Identity.Models.Manage.IndexViewModel;

public class IndexViewModel
{

    public string Id { get; set; }

    public string UserName { get; set; }

    public int Age { get; set; }

    public int PhoneNumber { get; set; }

    public string Email { get; set; }

    public string HomeAdress { get; set; }


}
