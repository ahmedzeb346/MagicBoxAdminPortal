using MagicBoxAdminPortal.Entities.MgtInventoryRequest;
using MagicBoxAdminPortal.Interfaces.MagicBoxInventry;
using MagicBoxAdminPortal.Repositories.RetailerMgtRepository;
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
    public class MagicBoxInventryController : ControllerBase
    {
        private readonly  IMagicBoxInventryRepository _magicBoxInventryRepository;
        public MagicBoxInventryController(IMagicBoxInventryRepository magicBoxInventryRepository)
        {
            _magicBoxInventryRepository = magicBoxInventryRepository;
        }
        [HttpPost("GetMagicBoxInventory")]
        public BaseResponse GetMagicBoxInventory(DataSourceRequest request)
        {
            BaseResponse resp = new BaseResponse();
            resp = _magicBoxInventryRepository.GetMagicBoxInventory(request);
            return resp;
        }
        [HttpPost("AddMagicBoxInventory")]
        public BaseResponse AddMagicBoxInventory(InventoryRequest inventoryRequest)
        {
            BaseResponse resp = new BaseResponse();
            resp = _magicBoxInventryRepository.AddMagicBoxInventory(inventoryRequest);
            return resp;
        }
        [HttpPost("UpdateMagicBoxInventory")]
        public BaseResponse UpdateMagicBoxInventory(InventoryRequest inventoryRequest)
        {
            BaseResponse resp = new BaseResponse();
            resp = _magicBoxInventryRepository.UpdateMagicBoxInventory(inventoryRequest);
            return resp;
        }
    }
}
