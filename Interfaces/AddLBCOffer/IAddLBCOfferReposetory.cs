using MagicBoxAdminPortal.Entities.LBCOffer;
using MagicBoxSupportApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Interfaces.AddLBCOffer
{
    public interface IAddLBCOfferReposetory
    {
        BaseResponse GetLBCOffers(string File_ID);
        BaseResponse AddLBCOffer(LBCOfferRequest inventoryRequest);
        BaseResponse EditLBCOffer(LBCOfferRequest inventoryRequest);
        BaseResponse DeleteLBCOffer(string pOfferID, string PENDED_BY);
    }
}
