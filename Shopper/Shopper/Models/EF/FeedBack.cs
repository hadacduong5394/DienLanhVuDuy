namespace Shopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeedBack")]
    public partial class FeedBack
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string DienThoai { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }

        [StringLength(250)]
        public string Content { get; set; }
    }
}
