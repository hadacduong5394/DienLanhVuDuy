using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;
using System.ComponentModel.DataAnnotations;

namespace Shopper.Models.ViewDetailModel
{
    public class LoginModel
    {
        [Display(Name="Tên đăng nhập")]
        [Required(ErrorMessage="Nhập tên đăng nhập")]
        public string userName { get; set; }
        [Display(Name="Mật khẩu")]
        [Required(ErrorMessage = "Nhập mật khẩu")]
        public string passWord { get; set; }

        public bool isKeep { get; set; }

    }
}