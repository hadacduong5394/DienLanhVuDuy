using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shopper.Models.EF;

namespace Shopper.Areas.Admin.Models
{
    public class RequestFeedbackModel
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string content { get; set; }
        public string CreateDate { get; set; }
        public string myRequest { get; set; }

    }
}