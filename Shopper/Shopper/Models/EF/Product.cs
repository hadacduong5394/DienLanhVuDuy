namespace Shopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(20, ErrorMessage = "Mã vạch không quá 20 kí tự")]
        [Display(Name = "Mã Vạch")]
        [Required(ErrorMessage = "Mã vạch không được bỏ trống")]
        public string Code { get; set; }

        public string MetaTitile { get; set; }

        [StringLength(250, ErrorMessage = "Tên Sản phẩm không quá 250 kí tự")]
        [Display(Name = "Tên Sản Phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống")]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Ảnh Sản Phẩm")]
        [Required(ErrorMessage = "Chọn 1 ảnh cho sản phẩm")]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [Display(Name = "Giá bán")]
        [Required(ErrorMessage = "Hãy nhập giá bán")]
        [Range(1000, double.MaxValue, ErrorMessage = "hãy nhập giá bán")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá sau khi giảm")]
        [Range(0, double.MaxValue, ErrorMessage = "hãy nhập giá bán")]
        public decimal? PromotionPrice { get; set; }

        public bool? IncludeVAT { get; set; }

        [Display(Name = "Số Lượng")]
        [Required(ErrorMessage = "Hãy nhập số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "hãy nhập số lượng")]
        public int? Quantity { get; set; }

        [Display(Name = "Danh Mục")]
        public long? CatagoryID { get; set; }

        [Display(Name = "Mô tả chi tiết sản phẩm")]
        [Required(ErrorMessage = "Chi tiết sản phẩm không được trống!")]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeyword { get; set; }

        [StringLength(250)]
        public string MetaDescription { get; set; }

        [Display(Name = "Trạng thái")]
        public bool? Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
    }
}
