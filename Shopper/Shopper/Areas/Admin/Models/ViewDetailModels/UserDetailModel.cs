using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopper.Areas.Admin.Models.ViewDetailModels
{
    public class UserDetailModel
    {

        public string FulleName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string phone { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
        public string ModifiBy { get; set; }
        public string ModefiDate { get; set; }
        public string Avartar { get; set; }
        public string Privilege { get; set; }
        public string Status { get; set; }


    }
}