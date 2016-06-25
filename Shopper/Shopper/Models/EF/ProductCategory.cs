namespace Shopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        public long ID { get; set; }

        [StringLength(250, ErrorMessage = "Tên danh mục dài không quá 250 kí tự")]
        [Display(Name = "Tên Danh Mục")]
        [Required(ErrorMessage = "Danh mục không được trống")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Tên đường link dài không quá 250 kí tự")]
        [Display(Name = "Tên link")]
        public string MetaTitile { get; set; }

        public long? ParentID { get; set; }

        [Display(Name = "Thứ Tự sắp xếp")]
        [Required(ErrorMessage="Hãy nhập thứ tự sắp xếp")]
        public int? DisplayOrder { get; set; }

        public string SeoTitile { get; set; }

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

        public bool? Status { get; set; }

        [Display(Name = "Trạng Thái")]
        public bool? ShowOnHome { get; set; }
    }
}
