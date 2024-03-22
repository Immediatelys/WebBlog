using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebBlog.Areas.Identity.Data;

namespace WebBlog.Areas.Identity.Models.User
{
    public class AddUserRoleModel
    {
        public AppUser user { get; set; }

        [DisplayName("Gán role cho user ")]
        public string[] RoleNames { get; set; }

/*
        public List<IdentityRoleClaim<string>> claimsInRole { get; set; }
        public List<IdentityUserClaim<string>> claimsInUserClaim { get; set; }*/

    }
}
