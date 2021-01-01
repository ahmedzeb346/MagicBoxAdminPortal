using MagicBoxAdminPortal.Entities.GETTRANDETAILS;
using MagicBoxSupportApi.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Interfaces.Support
{
    public interface ISupportRepository
    {
        BaseResponse GetMappedData();
        bool MNP_Data_Sync(string Desc);
        DataTable Read_MNP_File(string filepath);
        BaseResponse Check_MNP_file(DataTable dtt,  string msg,  DataTable dtError);
        DataTable ReadExcelFile(string filepath);
        BaseResponse GetAuthentication(string username, string pwd, string uSERID, string pAGES);
        void GetErrorCode(string API_NAME,ref DataTable dt);
        void GetXMPResponse(string tran_id, ref string resp);
        void PRC_GET_TRAN_DETAILS(GETTRANDETAILS gETTRANDETAILS, ref DataTable dt);

        void PRC_GET_DATE_TOTAL(string startDate, string endDate, ref DataTable dt);

        ////////clsGeneral/////////////
        DataTable Get_Retailer_Info(string imei);
        string APK_Push_DB_Call(APKPushDBCall aPKPushDBCall);
        string Enable_AGENT_BVMT_FLOWS(int pBIT);
        string DISABLE_AGENT_BVMT_FLOWS(int pBIT);
        string Ufone_JV_Access_DB_Call(string imei, string Ufno_JV);
        string IR_FlOW_Access_DB_Call(IRFlOWAccessDBCall iRFlOWAccessDBCall);

        // login

        bool Validate_LOGIN(ValidateLOGIN validateLOGIN);
        DataSet Call_Report_Procedure(CallReportProcedure callReportProcedure, ref string msgOut);

        DataTable GetAPK_Data(string FileNo);
        void Insert_APK_Rec(string IMEI_No, string APK_VersionNo, string FileNo);
    }
}
