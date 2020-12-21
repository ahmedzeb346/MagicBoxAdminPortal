using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxSupportApi.Models.RetailerMgt
{
    public class RetailerMgtRequest
    {
        public string pRetailerCNIC { get; set; } 
        public string pRetailerID { get; set; } 
        public string pRetailerPassword { get; set; }
        public string pRetailerRegion { get; set; }
        public string pFSRetailerID { get; set; }
        public string pFundamoPos { get; set; }
        public string pR_ID { get; set; }
        public string pEAgreementState { get; set; }
        public string pAccessRights { get; set; }
        public string pBVMT_Agent_Sent { get; set; }
        public string pBVMT_Agent_Receive { get; set; }
        public string pWIFI_FALLBACK { get; set; }
        public string pSMS_FALLBACK { get; set; }
        public string pUPSELL { get; set; }
        public string pDAP { get; set; }
        public string pIR_Cash_Withdrawal_Enabled { get; set; }
        public string pIR_BVS_Verfication_Enabled { get; set; }
        public string pIR_Pull_FUND_Enabled { get; set; }
        public string PFourGUPSELLCHECK_Enabled { get; set; }
        public string pDAPACCUPSEL_Enabled { get; set; }
        public string pRETAILER_CREATE { get; set; }
        public string pRSO_TYPE { get; set; }
        public string pRETAILER_CREATE_TYPE { get; set; }
        public string pREPEAT_RECHARGE { get; set; }
        public string pEASY_BAZAR { get; set; }
        public string pRetailerRegistrationEnabled { get; set; }
        public string pOTP_ENABLED { get; set; }
        public string pCompliancePasswordEnabled { get; set; }
        public string p789SimReplacementEnabled { get; set; }
        public string PCREATEDBY { get; set; }
        public string pUpdatedBy { get; set; }


    }
} 