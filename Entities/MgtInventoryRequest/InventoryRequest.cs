using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Entities.MgtInventoryRequest
{
    public class InventoryRequest
    {
        public string pMb_ID { get; set; }
        public string pMbmsisdnno { get; set; }
        public string pIccid { get; set; }
        public string pIsissued { get; set; }
        public string pUnitid { get; set; }
        public string pBidnings { get; set; }
        public string UserNameCreatedBy { get; set; }
        public string pCreatedBy { get; set; }
    }
}
