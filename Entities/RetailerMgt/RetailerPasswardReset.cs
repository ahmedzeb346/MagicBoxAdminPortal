using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Entities.RetailerMgt
{
    public class RetailerPasswardReset
    {
        public string pRetailerCNIC { get; set; }
        public string pRetailerID { get; set; }
        public string pRetailerPassward { get; set; }
        public string pRID { get; set; }
        public string pUpdatedBy { get; set; }
        public string pRetailerpasswordHash { get; set; }
    }
}
