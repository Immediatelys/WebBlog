using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Identity.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Phải nhập {0}")]
        [Display(Name = "Địa chỉ email hoặc tên tài khoản", Prompt = "Địa chỉ username")]
        public string UserNameOrEmail { get; set; }


        [Required(ErrorMessage = "Phải nhập {0}")]
        [Display(Name = "Mật khẩu", Prompt = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }




        public bool RememberMe { get; set; }
    }
}