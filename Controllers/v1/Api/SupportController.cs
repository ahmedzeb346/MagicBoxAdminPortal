using MagicBoxAdminPortal.Entities.GETTRANDETAILS;
using MagicBoxAdminPortal.Interfaces.Support;
using MagicBoxSupportApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Controllers.v1.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupportRepository _supportRepository;
        public SupportController(ISupportRepository supportRepository)
        {
            _supportRepository = supportRepository;
        }
        [HttpPost("GetMappedData")]
        public BaseResponse GetMappedData()
        {
            BaseResponse resp = new BaseResponse();
            resp = _supportRepository.GetMappedData();
            return resp;
        }
        [HttpPost("MNP_Data_Sync")]
        public bool MNP_Data_Sync(string Desc)
        {
            BaseResponse resp = new BaseResponse();
            bool resps = _supportRepository.MNP_Data_Sync(Desc);
            return resps;
        }
        [HttpPost("Read_MNP_File")]
        public DataTable Read_MNP_File(string filepath)
        {
            BaseResponse resp = new BaseResponse();
            DataTable resps = _supportRepository.Read_MNP_File(filepath);
            return resps;
        }
        [HttpPost("MNP_Data_Sync")]
        public bool Check_MNP_file(DataTable dt, ref string msg, ref DataTable dtError)
        {
            BaseResponse resp = new BaseResponse();
            bool resps = _supportRepository.Check_MNP_file(dt, ref msg, ref dtError);
            return resps;
        }
        [HttpPost("ReadExcelFile")]
        public DataTable ReadExcelFile(string filepath)
        {
            BaseResponse resp = new BaseResponse();
            DataTable resps = _supportRepository.ReadExcelFile(filepath);
            return resps;
        }

        [HttpPost("GetAuthentication")]
        public BaseResponse GetAuthentication(string username, string pwd,  string uSERID,  string pAGES)
        {
            BaseResponse resp = new BaseResponse();
            resp = _supportRepository.GetAuthentication(username, pwd,   uSERID, pAGES);
            return resp;
        }

        [HttpPost("GetErrorCode")]
        public void GetErrorCode(string API_NAME,ref DataTable dtt)
        {
            BaseResponse resp = new BaseResponse();
             _supportRepository.GetErrorCode(API_NAME, ref dtt);
           // return resp;
        }
        [HttpPost("GetXMPResponse")]
        public void GetXMPResponse(string tran_id, ref string resps)
        {
            BaseResponse resp = new BaseResponse();
            _supportRepository.GetXMPResponse(tran_id, ref resps);
            // return resp;
        }
       
        [HttpPost("GetXMPResponse")]
        public void PRC_GET_TRAN_DETAILS(GETTRANDETAILS gETTRANDETAILS, ref DataTable dt)
        {
            BaseResponse resp = new BaseResponse();
            _supportRepository.PRC_GET_TRAN_DETAILS(gETTRANDETAILS, ref dt);
            // return resp;
        }
        [HttpPost("PRC_GET_DATE_TOTAL")]
        public void PRC_GET_DATE_TOTAL(string startDate, string endDate, ref DataTable dt)
        {
            BaseResponse resp = new BaseResponse();
            _supportRepository.PRC_GET_DATE_TOTAL(startDate,  endDate, ref  dt);
            // return resp;
        }
        ////////clsGeneral/////////////

        [HttpPost("Get_Retailer_Info")]
        public DataTable Get_Retailer_Info(string imei)
        {
            // DataTable dtt = new DataTable();
            DataTable dtt =  _supportRepository.Get_Retailer_Info(imei);
             return dtt;
        }
        [HttpPost("APK_Push_DB_Call")]
        public string APK_Push_DB_Call(APKPushDBCall aPKPushDBCall)
        {
           /// BaseResponse resp = new BaseResponse();
            string desc= _supportRepository.APK_Push_DB_Call(aPKPushDBCall);
            return desc;
        }

        [HttpPost("Enable_AGENT_BVMT_FLOWS")]
        public string Enable_AGENT_BVMT_FLOWS(int pBIT)
        {
            // BaseResponse resp = new BaseResponse();
            string desc = _supportRepository.Enable_AGENT_BVMT_FLOWS(pBIT);
            return desc;
        }

        [HttpPost("DISABLE_AGENT_BVMT_FLOWS")]
        public string DISABLE_AGENT_BVMT_FLOWS(int pBIT)
        {
            // BaseResponse resp = new BaseResponse();
            string desc = _supportRepository.DISABLE_AGENT_BVMT_FLOWS(pBIT);
            return desc;
        }

        [HttpPost("Ufone_JV_Access_DB_Call")]
        public string Ufone_JV_Access_DB_Call(string imei, string Ufno_JV)
        {
            // BaseResponse resp = new BaseResponse();
            string desc = _supportRepository.Ufone_JV_Access_DB_Call(imei, Ufno_JV);
            return desc;
        }

        [HttpPost("IR_FlOW_Access_DB_Call")]
        public string IR_FlOW_Access_DB_Call(IRFlOWAccessDBCall iRFlOWAccessDBCall)
        {
            string desc = _supportRepository.IR_FlOW_Access_DB_Call(iRFlOWAccessDBCall);
            return desc;
        }

        [HttpPost("Validate_LOGIN")]
        public bool Validate_LOGIN(ValidateLOGIN validateLOGIN)
        {
            bool IsTrue = _supportRepository.Validate_LOGIN(validateLOGIN);
            return IsTrue;
        }
        [HttpPost("Call_Report_Procedure")]
        public DataSet Call_Report_Procedure(CallReportProcedure callReportProcedure, ref string msgOut)
        {
            DataSet DS = _supportRepository.Call_Report_Procedure(callReportProcedure , ref msgOut);
            return DS;
        }

        [HttpPost("GetAPK_Data")]
        public DataTable GetAPK_Data(string FileNo)
        {
            DataTable DT = _supportRepository.GetAPK_Data(FileNo);
            return DT;
        }
        //void Insert_APK_Rec(string IMEI_No, string APK_VersionNo, string FileNo)

        [HttpPost("Insert_APK_Rec")]
        public void Insert_APK_Rec(string IMEI_No, string APK_VersionNo, string FileNo)
        {
            _supportRepository.Insert_APK_Rec(IMEI_No, APK_VersionNo, FileNo);
        }
    }
}
