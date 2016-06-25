namespace Shopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StatusName")]
    public partial class StatusName
    {
        public bool ID { get; set; }

        [StringLength(50)]
        public string Content { get; set; }
    }
}
