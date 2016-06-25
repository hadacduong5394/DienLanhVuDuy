namespace Shopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string MetaTitile { get; set; }

        [StringLength(250)]
        [Display(Name="Tên bài viết")]
        [Required(ErrorMessage="Nhập tên bài viết")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name="Mô tả chung")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name="Ảnh bài viết")]
        [Required(ErrorMessage="Chọn ảnh bài viết")]
        public string Image { get; set; }

        public long? CatagoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name="Nội Dung")]
        public string Detail { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public int? Warranty { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeyword { get; set; }

        [StringLength(250)]
        public string MetaDescription { get; set; }

        public bool? Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(500)]
        [Display(Name="Trạng thái")]
        public string Tags { get; set; }
    }
}
