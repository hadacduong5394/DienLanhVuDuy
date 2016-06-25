namespace Shopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [Display(Name = "Mã")]
        [Required]
        public int ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Hiển thị liên hệ đầu trang")]
        [Required(ErrorMessage = "Liên hệ không được trống")]
        public string Text { get; set; }

        [StringLength(250)]
        [Display(Name = "Link chỉ tới")]
        [Required(ErrorMessage = "link liên hệ không được trống!")]
        public string Link { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        public string Target { get; set; }

        public bool? Status { get; set; }

        public int? MenuTypeID { get; set; }
    }
}
