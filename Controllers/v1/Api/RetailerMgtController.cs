
using MagicBoxAdminPortal.Entities.RetailerMgt;
using MagicBoxAdminPortal.Repositories.RetailerMgtRepository;
using MagicBoxSupport.Repository.Interfaces.RetailerMgt;
using MagicBoxSupportApi.Helpers;
using MagicBoxSupportApi.Models.RetailerMgt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxSupportApi.Controllers.v1.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailerMgtController : ControllerBase
    {

        private readonly IRetailerMgtRepository _IRetailerMgtRepository;
        public RetailerMgtController(IRetailerMgtRepository retailerMgtRepository)
        {
            _IRetailerMgtRepository = retailerMgtRepository;
        }
        [HttpPost("GetRetailers")]
        public BaseResponse GetRetailers(DataSourceRequest request)
        {
            BaseResponse resp = new BaseResponse();
            resp = _IRetailerMgtRepository.GetRetailers(request);
            return resp;
        }
        [HttpPost("AddRetailer")]
        public BaseResponse AddRetailer(RetailerMgtRequest retailerMgtRequest)
        {
            BaseResponse resp = new BaseResponse();
            resp =  _IRetailerMgtRepository.AddRetailer(retailerMgtRequest);
            return resp;
        }

        [HttpPost("EditRetailer")]
        public BaseResponse EditRetailer(RetailerMgtRequest retailerMgtRequest)
        {
            BaseResponse resp = new BaseResponse();
            resp =  _IRetailerMgtRepository.EditRetailer(retailerMgtRequest);
            return resp;
        }
        [HttpPost("DeleteRetailer")]
        public BaseResponse DeleteRetailer(string pR_ID, string pUpdatedBy)
        {
            BaseResponse resp = new BaseResponse();
            resp = _IRetailerMgtRepository.DeleteRetailer(pR_ID, pUpdatedBy);
            return resp;
        }

        [HttpPost("RetailerPasswardReset")]
        public BaseResponse RetailerPasswardReset(RetailerPasswardReset retailerPasswardReset)
        {
            BaseResponse resp = new BaseResponse();
            resp =  _IRetailerMgtRepository.RetailerPasswardReset(retailerPasswardReset);
            return resp;
        }

        [HttpPost("GetBindedRetailers")]
        public BaseResponse GetBindedRetailers(string mbid_No, string MSISDN_NO)
        {
            BaseResponse resp = new BaseResponse();
            resp = _IRetailerMgtRepository.GetBindedRetailers(mbid_No, MSISDN_NO);
            return resp;
        }

    }
}
