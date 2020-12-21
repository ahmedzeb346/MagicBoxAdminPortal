using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Entities.GETTRANDETAILS
{
   
    public class CallReportProcedure
    {
        public string TranID { get; set; }
        public string ChannelType { get; set; }
        public string API_name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
