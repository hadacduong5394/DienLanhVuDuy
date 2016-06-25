using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopper.Common
{
    [Serializable]
    public class LoginSession
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}