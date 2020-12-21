using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Entities.LBCOffer
{   
    public class LBCOfferRequest
    {
        public DateTime DTPStartDate { get; set; }
        public DateTime DTPEndDate { get; set; }
        public string pFranchise_ID { get; set; }
        public string pOfferDescription { get; set; }
        public string pOfferType { get; set; }
        public string pOfferPrice { get; set; }
        public string pOfferVolume { get; set; }
        public string pLBC_OfferPrice { get; set; }
        public string pCreatedBy { get; set; }
        public string pFileId { get; set; }
    }
}
