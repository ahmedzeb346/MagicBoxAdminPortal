using MagicBoxAdminPortal.Entities.MgtInventoryRequest;
using MagicBoxAdminPortal.Repositories.RetailerMgtRepository;
using MagicBoxSupportApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Interfaces.MagicBoxInventry
{
    public interface IMagicBoxInventryRepository
    {
        BaseResponse AddMagicBoxInventory(InventoryRequest inventoryRequest);
        BaseResponse UpdateMagicBoxInventory(InventoryRequest inventoryRequest);
        BaseResponse GetMagicBoxInventory(DataSourceRequest request);
    }
}
