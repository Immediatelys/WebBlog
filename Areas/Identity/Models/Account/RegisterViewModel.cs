// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {

        //[Display(Name = "Tên tài khoản", Prompt = "Tên tài khoản")]
        //[Required(ErrorMessage = "Phải nhập {0}")]
        /* [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 3)]*/
        public string UserName { get; set; }


        /* [Required(ErrorMessage = "Phải nhập {0}")]
         [EmailAddress(ErrorMessage = "Sai định dạng Email")]
         [Display(Name = "Email", Prompt = "Email")]*/
        public string Email { get; set; }

        /*[Required(ErrorMessage = "Phải nhập {0}")]*/
        /*[Display(Name = "Tuổi", Prompt = "Tuổi")]*//*[Display(Name = "Tuổi", Prompt = "Tuổi")]*/
        public int Age { get; set; }

        /*[Required(ErrorMessage = "Phải nhập {0}")]*/
        /*[Display(Name = "Giới tính", Prompt = "Giới tính")]*/
       /* public string Sex { get; set; }*/


        /*[Required(ErrorMessage = "Phải nhập {0}")]*/
        /* [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 2)]*/
        /* [Display(Name = "Mật khẩu", Prompt = "Mật khẩu")]
         [DataType(DataType.Password)]*/
        public string Password { get; set; }


        public string HomeAdress { get; set; }





    }
}