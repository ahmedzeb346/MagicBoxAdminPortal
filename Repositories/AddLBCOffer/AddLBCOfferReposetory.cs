using MagicBoxAdminPortal.Entities.LBCOffer;
using MagicBoxAdminPortal.Interfaces.AddLBCOffer;
using MagicBoxSupportApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Repositories.AddLBCOffer
{
    public class AddLBCOfferReposetory: IAddLBCOfferReposetory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        string Filter_col = "";
        DataTable dt = new DataTable();
        public AddLBCOfferReposetory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public BaseResponse AddLBCOffer(LBCOfferRequest inventoryRequest)
        {
            BaseResponse resp = new BaseResponse();
           // var serializer = new JsonConvert.SerializeObject();
            dynamic result = new ContentResult();
           // serializer.MaxJsonLength = int.MaxValue;
          //  result.ContentType = "application/json";
            try
            {
                DateTime StartDate = Convert.ToDateTime(inventoryRequest.DTPStartDate);
                DateTime EndDate = Convert.ToDateTime(inventoryRequest.DTPEndDate);

                DateTime now = DateTime.Now;
                if (now > StartDate)
                {
                    resp.Message = "Start date time cannot be less than today’s Datetime";
                    result.Content = JsonConvert.SerializeObject(resp);
                    return result;
                }
                if (StartDate > EndDate)
                {
                    resp.Message = "End Date Time cannot be less than Start Datetime";
                    result.Content = JsonConvert.SerializeObject(resp);
                    return result;
                }

                if (inventoryRequest.pFranchise_ID != "" && inventoryRequest.pOfferDescription != "" && inventoryRequest.pOfferType != "" && inventoryRequest.pOfferPrice != "" && inventoryRequest.pOfferVolume != "" && inventoryRequest.pLBC_OfferPrice != "")
                {
                    string FileId = "";
                    //Insert file info In DB
                    InsertFileInfo("LBC_Offers _Sample File1.xlsx", "1", inventoryRequest.pCreatedBy.ToLower(), ref FileId);
                    resp.Message = Call_Insert_LBC_Offer(inventoryRequest.pFranchise_ID, inventoryRequest.pOfferPrice, inventoryRequest.pOfferType.ToUpper(), inventoryRequest.pOfferVolume, inventoryRequest.pOfferDescription, StartDate, EndDate, inventoryRequest.pLBC_OfferPrice, FileId, inventoryRequest.pCreatedBy);
                }
                else
                {
                    resp.Message = "Invalid Inputs";
                    result.Content = JsonConvert.SerializeObject(resp);
                    return result;
                }
                result.Content = JsonConvert.SerializeObject(resp);
                return result;
            }
            catch (Exception ex)
            {

                resp.Message = ex.Message.ToString();
                //serializer.MaxJsonLength = int.MaxValue;
                result.Content = JsonConvert.SerializeObject(resp);
                result.ContentType = "application/json";
               // Log.LogError(ex, "LBCOffers/AddLBCOffer", "2035", " Franchise_ID : " + inventoryRequest.pFranchise_ID + " OfferPrice :" + inventoryRequest.pOfferPrice + " OfferType : " + inventoryRequest.pOfferType + " OfferType : " + inventoryRequest.pOfferType + " OfferVolume : " + inventoryRequest.pOfferVolume);
                return result.Conten;
            }
        }
        private void InsertFileInfo(string File_Name, string File_rec, string CreatedBy, ref string V_Pout)
        {
            BaseResponse response = new BaseResponse();

            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection con = null;
            OracleCommand cmd = null;
            try
            {

                //    using (cmd = new NpgsqlCommand("CALL  PRC_RETAILER_LOGIN_INFO_SAVE(@PRETAILERCNIC,@PRETAILERID,@PRETAILERREGION,@PFSRETAILERID,@PFUNDAMOPOS,@PEAgreementState,@PBVMT_Agent_Sent,@PBVMT_Agent_Receive,@PWIFI_FALLBACK,@PSMS_FALLBACK,@PUPSELL,@pDAP,@PIRCASHWITHDRAWL,@PIRBVSVERIFICATION,@PIRPULLFUND,@P4GUPSELLCHECK,@PDAPACCUPSELL,@PRETAILER_CREATE,@PRSO_TYPE,@PRETAILER_CREATE_TYPE,@PREPEAT_RECHARGE,@PEASY_BAZAR,@PRETAILER_REGISTRATION,@POTP,@PCOMPLIANCE_PASSWORD,@P789_SIM_REPLACEMENT,@PACCESSRIGHTS,@PCREATEDBY)", connection = new NpgsqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
                // using (connection = new OracleConnection("data source=10.0.1.192:1521/demoaksa; User ID=MBPU_STG; Password=mbpu321;"))

                using (con = new OracleConnection(_connectionString))
                {
                    using (cmd = new OracleCommand("PKG_MAPPING_BULK.mpng_tmpp_file", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pFILE_NME", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = File_Name });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pFILE_REC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = File_rec });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pCreatedBy", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = CreatedBy });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pfle", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                        //{
                        V_Pout = cmd.Parameters["pfle"].Value.ToString();
                        // obj.msg = cmd.Parameters["PDESC"].Value.ToString();
                        //}
                        // else
                        //  {
                        //     obj.msg = cmd.Parameters["PDESC"].Value.ToString();
                        //  }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (con != null && con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
                catch { }
            }
          //  return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }
        private string Call_Insert_LBC_Offer(string PFRANCHISE_ID, string POFFER_PRICE, string POFFER_TYPE, string POFFER_VOLUME, string POFFER_DESC, DateTime? PSTART_DATE, DateTime? PEND_DATE, string PLBC_PRICE, string PFILE_ID, string pCreatedBy)
        {
            BaseResponse response = new BaseResponse();

            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection con = null;
            OracleCommand cmd = null;
            try
            {

                //    using (cmd = new NpgsqlCommand("CALL  PRC_RETAILER_LOGIN_INFO_SAVE(@PRETAILERCNIC,@PRETAILERID,@PRETAILERREGION,@PFSRETAILERID,@PFUNDAMOPOS,@PEAgreementState,@PBVMT_Agent_Sent,@PBVMT_Agent_Receive,@PWIFI_FALLBACK,@PSMS_FALLBACK,@PUPSELL,@pDAP,@PIRCASHWITHDRAWL,@PIRBVSVERIFICATION,@PIRPULLFUND,@P4GUPSELLCHECK,@PDAPACCUPSELL,@PRETAILER_CREATE,@PRSO_TYPE,@PRETAILER_CREATE_TYPE,@PREPEAT_RECHARGE,@PEASY_BAZAR,@PRETAILER_REGISTRATION,@POTP,@PCOMPLIANCE_PASSWORD,@P789_SIM_REPLACEMENT,@PACCESSRIGHTS,@PCREATEDBY)", connection = new NpgsqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
                // using (connection = new OracleConnection("data source=10.0.1.192:1521/demoaksa; User ID=MBPU_STG; Password=mbpu321;"))

                using (con = new OracleConnection(_connectionString))
                {
                    using (cmd = new OracleCommand("PKG_LBC_OFFERS.PRC_LBC_OFFER_INSERT", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PFRANCHISE_ID", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = PFRANCHISE_ID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "POFFER_PRICE", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = POFFER_PRICE });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "POFFER_TYPE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = POFFER_TYPE });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "POFFER_VOLUME", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = POFFER_VOLUME });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "POFFER_DESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = POFFER_DESC });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PSTART_DATE", OracleDbType = OracleDbType.TimeStamp, Direction = ParameterDirection.Input, Value = PSTART_DATE });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PEND_DATE", OracleDbType = OracleDbType.TimeStamp, Direction = ParameterDirection.Input, Value = PEND_DATE });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PLBC_PRICE", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = PLBC_PRICE });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PFILE_ID", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = PFILE_ID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCREATED_BY", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = pCreatedBy.ToLower() });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PMSG"].Value) == "Y")
                        {
                            response.Message = "Operation Performed Successfully";
                        }
                        else
                        {
                            response.Message = cmd.Parameters["PDESC"].Value.ToString();
                        }
                        con.Dispose();
                        con = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (con != null && con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
                catch { }
            }
              return response.Message;
        }

        public BaseResponse DeleteLBCOffer(string pOfferID, string PENDED_BY)
        {
            BaseResponse resp = new BaseResponse();
           // var serializer = new JavaScriptSerializer();
            dynamic result = new ContentResult();
           // serializer.MaxJsonLength = int.MaxValue;
            result.ContentType = "application/json";
            try
            {

                resp.Message = DeleteProcess(pOfferID, PENDED_BY);
                result.Content = JsonConvert.SerializeObject(resp);
                return result;
            }
            catch (Exception ex)
            {

               resp.Message = ex.Message.ToString();
                //serializer.MaxJsonLength = int.MaxValue;
                result.Content = JsonConvert.SerializeObject(resp);
                
                result.ContentType = "application/json";
               // Log.LogError(ex, "LBCOffers/DeleteLBCOffer", "2035", " OfferID : " + pOfferID);
                return result;
            }
        }

        private string DeleteProcess(string pOfferID, string PENDED_BY)
        {
            BaseResponse response = new BaseResponse();

            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection con = null;
            OracleCommand cmd = null;
            try
            {

                //    using (cmd = new NpgsqlCommand("CALL  PRC_RETAILER_LOGIN_INFO_SAVE(@PRETAILERCNIC,@PRETAILERID,@PRETAILERREGION,@PFSRETAILERID,@PFUNDAMOPOS,@PEAgreementState,@PBVMT_Agent_Sent,@PBVMT_Agent_Receive,@PWIFI_FALLBACK,@PSMS_FALLBACK,@PUPSELL,@pDAP,@PIRCASHWITHDRAWL,@PIRBVSVERIFICATION,@PIRPULLFUND,@P4GUPSELLCHECK,@PDAPACCUPSELL,@PRETAILER_CREATE,@PRSO_TYPE,@PRETAILER_CREATE_TYPE,@PREPEAT_RECHARGE,@PEASY_BAZAR,@PRETAILER_REGISTRATION,@POTP,@PCOMPLIANCE_PASSWORD,@P789_SIM_REPLACEMENT,@PACCESSRIGHTS,@PCREATEDBY)", connection = new NpgsqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
                // using (connection = new OracleConnection("data source=10.0.1.192:1521/demoaksa; User ID=MBPU_STG; Password=mbpu321;"))

                using (con = new OracleConnection(_connectionString))
                {
                    using (cmd = new OracleCommand("PKG_LBC_OFFERS.PRC_LBC_OFFER_DELETE", con))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PID", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = pOfferID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PENDED_BY", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = PENDED_BY.ToLower() });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", Value = "", OracleDbType = OracleDbType.Varchar2, Direction = System.Data.ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", Value = "", OracleDbType = OracleDbType.Varchar2, Direction = System.Data.ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", Value = "", OracleDbType = OracleDbType.Varchar2, Direction = System.Data.ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PMSG"].Value) == "Y")
                        {
                            response.Message = "Operation Performed Successfully";
                        }
                        else
                        {
                            response.Message = cmd.Parameters["PDESC"].Value.ToString();
                        }
                        con.Dispose();
                        con = null;
                    }
                    return response.Message;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (con != null && con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
                catch { }
            }
            //  return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }

        public BaseResponse EditLBCOffer(LBCOfferRequest inventoryRequest)
        {
            BaseResponse resp = new BaseResponse();
            //var serializer = new JavaScriptSerializer();
            dynamic result = new ContentResult();
            //serializer.MaxJsonLength = int.MaxValue;
            result.ContentType = "application/json";
            try
            {
                DateTime StartDate = Convert.ToDateTime(inventoryRequest.DTPStartDate);
                DateTime EndDate = Convert.ToDateTime(inventoryRequest.DTPEndDate);

                DateTime now = DateTime.Now;
                if (now > StartDate)
                {
                    resp.Message = "Start date time cannot be less than today’s Datetime";
                    result.Content = JsonConvert.SerializeObject(resp);
                    return result;
                }
                if (StartDate > EndDate)
                {
                    resp.Message = "End Date Time cannot be less than Start Datetime";
                    result.Content = JsonConvert.SerializeObject(resp);
                    return result;
                }

                if (inventoryRequest.pFranchise_ID != "" && inventoryRequest.pOfferDescription != "" && inventoryRequest.pOfferType != "" && inventoryRequest.pOfferPrice != "" && inventoryRequest.pOfferVolume != "" && inventoryRequest.pLBC_OfferPrice != "")
                {
                    resp.Message = Call_Update_LBC_Offer(inventoryRequest.pFranchise_ID, inventoryRequest.pOfferPrice, inventoryRequest.pOfferType, inventoryRequest.pOfferVolume, inventoryRequest.pOfferDescription, StartDate, EndDate, inventoryRequest.pLBC_OfferPrice, inventoryRequest.pFileId, inventoryRequest.pCreatedBy);
                }
                else
                {
                    resp.Message = "Invalid Inputs";
                    result.Content = JsonConvert.SerializeObject(resp);
                    return result;
                }
                result.Content = JsonConvert.SerializeObject(resp);
                return result;
            }
            catch (Exception ex)
            {

                resp.Message = ex.Message.ToString();
                //serializer.MaxJsonLength = int.MaxValue;
                result.Content = JsonConvert.SerializeObject(resp);

                //  result.ContentType = "application/json";
                return result;
                //Log.LogError(ex, "LBCOffers/EditLBCOffer", "2035", " Franchise_ID : " + pFranchise_ID + " OfferPrice :" + pOfferPrice + " OfferType : " + pOfferType + " OfferType : " + pOfferType + " OfferVolume : " + pOfferVolume);
            }
        }

        private string Call_Update_LBC_Offer(string pFranchise_ID, string pOfferPrice, string pOfferType, string pOfferVolume, string pOfferDescription, DateTime startDate, DateTime endDate, string pLBC_OfferPrice, string pFileId , string pCreatedBy)
        {
            BaseResponse response = new BaseResponse();

            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection con = null;
            OracleCommand cmd = null;
            try
            {

                //    using (cmd = new NpgsqlCommand("CALL  PRC_RETAILER_LOGIN_INFO_SAVE(@PRETAILERCNIC,@PRETAILERID,@PRETAILERREGION,@PFSRETAILERID,@PFUNDAMOPOS,@PEAgreementState,@PBVMT_Agent_Sent,@PBVMT_Agent_Receive,@PWIFI_FALLBACK,@PSMS_FALLBACK,@PUPSELL,@pDAP,@PIRCASHWITHDRAWL,@PIRBVSVERIFICATION,@PIRPULLFUND,@P4GUPSELLCHECK,@PDAPACCUPSELL,@PRETAILER_CREATE,@PRSO_TYPE,@PRETAILER_CREATE_TYPE,@PREPEAT_RECHARGE,@PEASY_BAZAR,@PRETAILER_REGISTRATION,@POTP,@PCOMPLIANCE_PASSWORD,@P789_SIM_REPLACEMENT,@PACCESSRIGHTS,@PCREATEDBY)", connection = new NpgsqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
                // using (connection = new OracleConnection("data source=10.0.1.192:1521/demoaksa; User ID=MBPU_STG; Password=mbpu321;"))

                using (con = new OracleConnection(_connectionString))
                {
                    using (cmd = new OracleCommand("PKG_LBC_OFFERS.PRC_LBC_OFFER_UPDATE", con))
                    {
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PFRANCHISE_ID", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = pFranchise_ID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "POFFER_PRICE", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = pOfferPrice });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "POFFER_TYPE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = pOfferType });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "POFFER_VOLUME", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = pOfferVolume });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "POFFER_DESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = pOfferDescription });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PSTART_DATE", OracleDbType = OracleDbType.TimeStamp, Direction = ParameterDirection.Input, Value = startDate });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PEND_DATE", OracleDbType = OracleDbType.TimeStamp, Direction = ParameterDirection.Input, Value = endDate });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PLBC_PRICE", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = pLBC_OfferPrice });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PFILE_ID", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = pFileId });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCREATED_BY", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = pCreatedBy.ToLower() });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PMSG"].Value) == "Y")
                        {
                            response.Message = "Operation Performed Successfully";
                        }
                        else
                        {
                            response.Message = cmd.Parameters["PDESC"].Value.ToString();
                        }
                        con.Dispose();
                        con = null;
                    }
                }
                return response.Message;
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (con != null && con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
                catch { }
            }
            //  return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }

        public BaseResponse GetLBCOffers(string File_ID)
        {
            BaseResponse response = new BaseResponse();
            var resultt = "";
            dynamic dtt = null;
            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection con = null;
            OracleCommand cmd = null;
            try
            {

                //    using (cmd = new NpgsqlCommand("CALL  PRC_RETAILER_LOGIN_INFO_SAVE(@PRETAILERCNIC,@PRETAILERID,@PRETAILERREGION,@PFSRETAILERID,@PFUNDAMOPOS,@PEAgreementState,@PBVMT_Agent_Sent,@PBVMT_Agent_Receive,@PWIFI_FALLBACK,@PSMS_FALLBACK,@PUPSELL,@pDAP,@PIRCASHWITHDRAWL,@PIRBVSVERIFICATION,@PIRPULLFUND,@P4GUPSELLCHECK,@PDAPACCUPSELL,@PRETAILER_CREATE,@PRSO_TYPE,@PRETAILER_CREATE_TYPE,@PREPEAT_RECHARGE,@PEASY_BAZAR,@PRETAILER_REGISTRATION,@POTP,@PCOMPLIANCE_PASSWORD,@P789_SIM_REPLACEMENT,@PACCESSRIGHTS,@PCREATEDBY)", connection = new NpgsqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
                // using (connection = new OracleConnection("data source=10.0.1.192:1521/demoaksa; User ID=MBPU_STG; Password=mbpu321;"))

                using (con = new OracleConnection(_connectionString))
                {

                    using (cmd = new OracleCommand("PKG_LBC_OFFERS.PRC_GET_LBC_OFFERS", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pFILEID", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = File_ID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pCUR_LBC_OFFERS", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });

                        OracleDataAdapter oAdapdter = new OracleDataAdapter(cmd);
                        dtt =  oAdapdter.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                        }
                    }
                }
                con.Dispose();
                con = null;

                return dtt;
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (con != null && con.State != ConnectionState.Closed)
                    {
                        con.Close();
                        con.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
                catch { }
            }
            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message, Data = dtt };
        }
    }
}
