using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shopper.Areas.Admin.Models.ViewDetailModels
{
    public class ModelLogin
    {
        [Required(ErrorMessage = "Nhập Tên Đăng Nhập")]
        public string account { get; set; }
        [Required(ErrorMessage = "Nhập  Mật Khẩu")]
        public string password { get; set; }

        public bool RememberMe { get; set; }
    }
}