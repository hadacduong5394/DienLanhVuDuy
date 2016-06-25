using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shopper.Models.ViewDetailModel
{
    public class PayMan
    {
        [Display(Name="Người nhận hàng")]
        [StringLength(50,ErrorMessage="Tên không quá 50 kí tự")]
        [Required(ErrorMessage="Nhập tên người nhận hàng")]
        public string name { get; set; }

        [Display(Name="Địa chỉ giao hàng")]
        [StringLength(250)]
        [Required(ErrorMessage="Nhập địa chỉ giao hàng")]
        public string address { get; set; }

        [Display(Name="Điện thoại liên hệ")]
        [Phone(ErrorMessage="Nhập số điện thoại")]
        [StringLength(11,MinimumLength=10,ErrorMessage="Nhập số điện thoại")]
        [Required(ErrorMessage="Nhập số điện thoại liên hệ")]
        public string phone { get; set; }

        [Display(Name="Email")]
        [Required(ErrorMessage="Nhập Email")]
        [DataType(DataType.EmailAddress, ErrorMessage="Không đúng định dạng Email")]
        public string email { get; set; }
    }
}