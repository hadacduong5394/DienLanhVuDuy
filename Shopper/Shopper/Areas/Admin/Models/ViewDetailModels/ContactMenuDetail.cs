using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopper.Areas.Admin.Models.ViewDetailModels
{
    public class ContactMenuDetail
    {
        [Display(Name="Mã")]
        public int ID { get; set; }

        [StringLength(250)]
        public string Text { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public int? DisplayOrder { get; set; }
    }
}