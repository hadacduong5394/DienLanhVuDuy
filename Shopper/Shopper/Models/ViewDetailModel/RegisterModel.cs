using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shopper.Models.ViewDetailModel
{
    public class RegisterModel
    {
        [Display(Name="Tên đăng nhập")]
        [Required(ErrorMessage="Nhập tên đăng nhập")]
        [StringLength(50,MinimumLength=6,ErrorMessage="Tên đăng nhập từ 6 đến 50 kí tự")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Tên đăng nhập không được có kí tự trắng")]
        public string userName { set; get; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6 đến 50 kí tự")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Mật khẩu không không được có kí tự trắng")]
        public string password { get;set;}
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("password",ErrorMessage="Nhập lại mật khẩu")]
        public string confirmPassword { get; set; }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Nhập họ tên")]
        [StringLength(250, ErrorMessage = "Tên không dài quá 250 kí tự")]
        public string name { get; set; }
        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Nhập điện thoại")]
        [Phone(ErrorMessage="Nhập số điện thoại")]
        public string phoneNumber { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Nhập Email")]
        [EmailAddress(ErrorMessage="Nhập đúng định dạng Email")]
        public string email { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ")]
        [StringLength(250, ErrorMessage = "Địa chỉ không dài quá 250 kí tự")]
        public string address { get; set; }

    }
}