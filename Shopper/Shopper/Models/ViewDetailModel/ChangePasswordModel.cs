using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopper.Models.ViewDetailModel
{
    public class ChangePasswordModel
    {
        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Hãy điền mật khẩu mới")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6 đến 30 kí tự")]
        public string newPassword { get; set; }

        [Display(Name = "Xác nhận lại mật khẩu")]
        [Compare("newPassword", ErrorMessage = "Xác nhận mật khẩu chưa đúng")]
        public string confirmPassword { get; set; }
    }
}