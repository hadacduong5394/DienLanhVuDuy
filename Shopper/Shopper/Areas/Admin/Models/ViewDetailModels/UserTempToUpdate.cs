using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopper.Areas.Admin.Models.ViewDetailModels
{
    public class UserTempToUpdate
    {
        [Display(Name = "Mã Người Dùng")]
        public string ID { get; set; }

        [Display(Name = "Tên Đăng Nhập")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Display(Name = "Mật Khẩu")]
        [StringLength(250, MinimumLength = 6, ErrorMessage = "Mật khẩu không ít hơn 6 kí tự")]
        public string PassWord { get; set; }

        [Display(Name = "Ảnh Đại Diện")]
        [StringLength(250)]
        public string Avartar { get; set; }

        [Display(Name = "Họ Tên")]
        [StringLength(250, ErrorMessage = "Họ Tên Không Quá 250 Kí Tự")]
        [Required(ErrorMessage = "Nhập Họ Tên")]
        public string FullName { get; set; }

        [Display(Name = "Địa Chỉ")]
        [Required(ErrorMessage = "Địa Chỉ không được trống")]
        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(250)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Nhập Email")]
        public string Email { get; set; }

        [StringLength(11, MinimumLength = 10, ErrorMessage = "Nhập số điện thoại")]
        [Required(ErrorMessage = "Điện thoại không được trống")]
        [Display(Name = "Điện thoại")]
        [Phone(ErrorMessage = "Nhập Số Điện Thoại")]
        public string Phone { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Display(Name = "Kích Hoạt")]
        public bool? Status { get; set; }
    }
}