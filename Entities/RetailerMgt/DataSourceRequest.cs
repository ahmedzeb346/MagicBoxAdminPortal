using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Repositories.RetailerMgtRepository
{
    public class DataSourceRequest
    {

        public string UserRegion { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Filter_col { get; set; }
    }
}
