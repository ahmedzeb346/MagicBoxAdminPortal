using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.generalrepositoryinterface.CreateHashByKey
{
    public interface ICreateHashByKey
    {
         string CreateByKeyHash(string valuetoHash, string keyValue);
    }
}
