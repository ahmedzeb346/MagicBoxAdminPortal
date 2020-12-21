
using MagicBoxAdminPortal.Entities.RetailerMgt;
using MagicBoxAdminPortal.Repositories.RetailerMgtRepository;
using MagicBoxSupport.Repository.Repositories.RetailerMgtRepository;
using MagicBoxSupportApi.Helpers;
using MagicBoxSupportApi.Models.RetailerMgt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MagicBoxSupport.Repository.Interfaces.RetailerMgt
{
   
    public interface IRetailerMgtRepository
    {
        BaseResponse AddRetailer(RetailerMgtRequest retailerMgtRequest);
        BaseResponse EditRetailer(RetailerMgtRequest retailerMgtRequest);
        BaseResponse DeleteRetailer(string pR_ID, string pUpdatedBy);
        BaseResponse RetailerPasswardReset(RetailerPasswardReset retailerPasswardReset);
        BaseResponse GetBindedRetailers(string mbid_No, string MSISDN_NO);
        BaseResponse GetRetailers(DataSourceRequest request);
    }
}
