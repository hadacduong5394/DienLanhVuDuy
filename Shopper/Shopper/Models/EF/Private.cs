namespace Shopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Private")]
    public partial class Private
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string Content { get; set; }
    }
}
