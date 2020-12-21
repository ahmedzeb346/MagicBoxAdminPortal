using MagicBoxAdminPortal.Entities.LBCOffer;
using MagicBoxAdminPortal.Interfaces.AddLBCOffer;
using MagicBoxSupportApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Controllers.v1.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LBCOffersController : ControllerBase
    {
        private readonly IAddLBCOfferReposetory _addLBCOfferReposetory;
        public LBCOffersController(IAddLBCOfferReposetory addLBCOfferReposetory) {
            _addLBCOfferReposetory = addLBCOfferReposetory;
        }
        [HttpPost("GetLBCOffers")]
        public BaseResponse GetLBCOffers(string File_ID)
        {
            BaseResponse resp = new BaseResponse();
            resp = _addLBCOfferReposetory.GetLBCOffers(File_ID);
            return resp;
        }
        [HttpPost("AddLBCOffer")]
        public BaseResponse AddLBCOffer(LBCOfferRequest inventoryRequest)
        {
            BaseResponse resp = new BaseResponse();
            resp = _addLBCOfferReposetory.AddLBCOffer(inventoryRequest);
            return resp;
        }
        [HttpPost("EditLBCOffer")]
        public BaseResponse EditLBCOffer(LBCOfferRequest inventoryRequest)
        {
            BaseResponse resp = new BaseResponse();
            resp = _addLBCOfferReposetory.EditLBCOffer(inventoryRequest);
            return resp;
        }
        
        [HttpPost("DeleteLBCOffer")]
        public BaseResponse DeleteLBCOffer(string pOfferID, string PENDED_BY)
        {
            BaseResponse resp = new BaseResponse();
            resp = _addLBCOfferReposetory.DeleteLBCOffer(pOfferID, PENDED_BY);
            return resp;
        }
        
    }
}
