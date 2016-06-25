namespace Shopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public long ID { get; set; }

        public DateTime? CreateDate { get; set; }

        public long? CustomerID { get; set; }

        [Display(Name = "Người nhận hàng")]
        [StringLength(50, ErrorMessage = "Tên không quá 50 kí tự")]
        [Required(ErrorMessage = "Nhập tên người nhận hàng")]
        public string ShipName { get; set; }

        [Display(Name = "Điện thoại liên hệ")]
        [Phone(ErrorMessage = "Nhập số điện thoại")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Nhập số điện thoại")]
        [Required(ErrorMessage = "Nhập số điện thoại liên hệ")]
        public string ShipMobile { get; set; }

        [StringLength(200)]
        [Display(Name = "Địa chỉ giao hang")]
        [Required(ErrorMessage = "Nhập địa chỉ giao hàng")]
        public string ShipAddress { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Nhập Email")]
        [EmailAddress(ErrorMessage = "Không đúng định dạng Email")]
        [StringLength(100)]
        public string ShipEmail { get; set; }

        public int? Status { get; set; }
    }
}
