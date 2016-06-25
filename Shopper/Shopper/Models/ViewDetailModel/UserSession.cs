using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopper.Models.ViewDetailModel
{
    [Serializable]
    public class UserSession
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
    }
}