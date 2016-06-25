using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shopper.Models.ViewDetailModel
{
    public class UserInfoModel
    {
        [Display(Name="Tên đăng nhập")]
        public string UserName { get; set; }
        [Display(Name="Họ tên")]
        [Required(ErrorMessage="Nhập họ tên")]
        [StringLength(250)]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Nhập Email")]
        [StringLength(250)]
        [EmailAddress(ErrorMessage="Nhập đúng định dạng Email")]
        public string Email { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Nhập đại chỉ")]
        public string Address { get; set; }
        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Nhập điện thoại")]
        [Phone(ErrorMessage="Nhập số điện thoại")]
        [StringLength(11,MinimumLength=10,ErrorMessage="Nhập số điện thoại")]
        public string PhoneNumber { get; set; }

    }
}