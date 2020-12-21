
using MagicBoxAdminPortal.Entities.RetailerMgt;
using MagicBoxAdminPortal.generalrepositoryinterface.CreateHashByKey;
using MagicBoxAdminPortal.Repositories.RetailerMgtRepository;
using MagicBoxSupport.Repository.Interfaces.RetailerMgt;
using MagicBoxSupportApi.Helpers;
using MagicBoxSupportApi.Models.RetailerMgt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MagicBoxSupport.Repository.Repositories.RetailerMgtRepository
{
    public class RetailerMgtRepository : IRetailerMgtRepository
    {
        string Filter_col = "";
        DataTable dt = new DataTable();
        private readonly ICreateHashByKey _createHashByKey;
        IConfiguration _configuration;
        private readonly string _connectionString;
        public RetailerMgtRepository(IConfiguration configuration, ICreateHashByKey createHashByKey)
        {
            _createHashByKey = createHashByKey;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public BaseResponse AddRetailer(RetailerMgtRequest retailerMgtRequest)
        {
            retailerMgtRequest.pRetailerCNIC = retailerMgtRequest.pRetailerCNIC.Insert(5, "-").Insert(13, "-");




            ///////
            BaseResponse response = new BaseResponse();

            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection connection = null;
            OracleCommand cmd = null;
            try
            {

                //    using (cmd = new NpgsqlCommand("CALL  PRC_RETAILER_LOGIN_INFO_SAVE(@PRETAILERCNIC,@PRETAILERID,@PRETAILERREGION,@PFSRETAILERID,@PFUNDAMOPOS,@PEAgreementState,@PBVMT_Agent_Sent,@PBVMT_Agent_Receive,@PWIFI_FALLBACK,@PSMS_FALLBACK,@PUPSELL,@pDAP,@PIRCASHWITHDRAWL,@PIRBVSVERIFICATION,@PIRPULLFUND,@P4GUPSELLCHECK,@PDAPACCUPSELL,@PRETAILER_CREATE,@PRSO_TYPE,@PRETAILER_CREATE_TYPE,@PREPEAT_RECHARGE,@PEASY_BAZAR,@PRETAILER_REGISTRATION,@POTP,@PCOMPLIANCE_PASSWORD,@P789_SIM_REPLACEMENT,@PACCESSRIGHTS,@PCREATEDBY)", connection = new NpgsqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
                // using (connection = new OracleConnection("data source=10.0.1.192:1521/demoaksa; User ID=MBPU_STG; Password=mbpu321;"))

                using (connection = new OracleConnection(_connectionString))
                {

                    using (cmd = new OracleCommand("PKG_RETAILER_MANAGMENT.PRC_RETAILER_LOGIN_INFO_SAVE", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILERCNIC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRetailerCNIC });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILERID", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRetailerID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILERREGION", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRetailerRegion });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PFSRETAILERID", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pFSRetailerID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PFUNDAMOPOS", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pFundamoPos });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PEAgreementState", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pEAgreementState });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PBVMT_Agent_Sent", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pBVMT_Agent_Sent });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PBVMT_Agent_Receive", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pBVMT_Agent_Receive });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PWIFI_FALLBACK", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pWIFI_FALLBACK == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PSMS_FALLBACK", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pSMS_FALLBACK == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PUPSELL", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pUPSELL == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDAP", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pDAP == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIRCASHWITHDRAWL", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pIR_Cash_Withdrawal_Enabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIRBVSVERIFICATION", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pIR_BVS_Verfication_Enabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIRPULLFUND", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pIR_Pull_FUND_Enabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P4GUPSELLCHECK", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.PFourGUPSELLCHECK_Enabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDAPACCUPSELL", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pDAPACCUPSEL_Enabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILER_CREATE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRETAILER_CREATE == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRSO_TYPE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRSO_TYPE == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILER_CREATE_TYPE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRETAILER_CREATE_TYPE });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PREPEAT_RECHARGE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pREPEAT_RECHARGE == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PEASY_BAZAR", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pEASY_BAZAR == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILER_REGISTRATION", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRetailerRegistrationEnabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "POTP", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pOTP_ENABLED == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCOMPLIANCE_PASSWORD", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pCompliancePasswordEnabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P789_SIM_REPLACEMENT", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.p789SimReplacementEnabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PACCESSRIGHTS", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pAccessRights });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCREATEDBY", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.PCREATEDBY });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                        {

                            cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = System.Data.ParameterDirection.Output;

                        }
                        else
                        {
                            cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = System.Data.ParameterDirection.Output;
                        }

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
                    if (connection != null && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
                catch { }
            }
            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }

        public BaseResponse EditRetailer(RetailerMgtRequest retailerMgtRequest)
        {
            // retailerMgtRequest.pRetailerCNIC = retailerMgtRequest.pRetailerCNIC.Insert(5, "-").Insert(13, "-");

            if (retailerMgtRequest.pRetailerCNIC.Length.Equals(13))
            {
                retailerMgtRequest.pRetailerCNIC = retailerMgtRequest.pRetailerCNIC.Insert(5, "-");
                retailerMgtRequest.pRetailerCNIC = retailerMgtRequest.pRetailerCNIC.Insert(13, "-");
            }


            ///////
            BaseResponse response = new BaseResponse();

            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection connection = null;
            OracleCommand cmd = null;
            try
            {

                //    using (cmd = new NpgsqlCommand("CALL  PRC_RETAILER_LOGIN_INFO_SAVE(@PRETAILERCNIC,@PRETAILERID,@PRETAILERREGION,@PFSRETAILERID,@PFUNDAMOPOS,@PEAgreementState,@PBVMT_Agent_Sent,@PBVMT_Agent_Receive,@PWIFI_FALLBACK,@PSMS_FALLBACK,@PUPSELL,@pDAP,@PIRCASHWITHDRAWL,@PIRBVSVERIFICATION,@PIRPULLFUND,@P4GUPSELLCHECK,@PDAPACCUPSELL,@PRETAILER_CREATE,@PRSO_TYPE,@PRETAILER_CREATE_TYPE,@PREPEAT_RECHARGE,@PEASY_BAZAR,@PRETAILER_REGISTRATION,@POTP,@PCOMPLIANCE_PASSWORD,@P789_SIM_REPLACEMENT,@PACCESSRIGHTS,@PCREATEDBY)", connection = new NpgsqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
                // using (connection = new OracleConnection("data source=10.0.1.192:1521/demoaksa; User ID=MBPU_STG; Password=mbpu321;"))

                using (connection = new OracleConnection(_connectionString))
                {

                    using (cmd = new OracleCommand("PKG_RETAILER_MANAGMENT.PRC_RETAILER_LOGIN_INFO_UPDATE", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pRetailercnic", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRetailerCNIC });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pRetailerid", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRetailerID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pRid", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pR_ID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILERREGION", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRetailerRegion });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PFSRETAILERID", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pFSRetailerID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PFUNDAMOPOS", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pFundamoPos });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PEAgreementState", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pEAgreementState });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PBVMT_Agent_Sent", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pBVMT_Agent_Sent });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PBVMT_Agent_Receive", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pBVMT_Agent_Receive });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PWIFI_FALLBACK", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pWIFI_FALLBACK == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PSMS_FALLBACK", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pSMS_FALLBACK == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PUPSELL", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pUPSELL == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDAP", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pDAP == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIRCASHWITHDRAWL", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pIR_Cash_Withdrawal_Enabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIRBVSVERIFICATION", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pIR_BVS_Verfication_Enabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIRPULLFUND", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pIR_Pull_FUND_Enabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P4GUPSELLCHECK", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.PFourGUPSELLCHECK_Enabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDAPACCUPSELL", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pDAPACCUPSEL_Enabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILER_CREATE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRETAILER_CREATE == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRSO_TYPE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRSO_TYPE == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILER_CREATE_TYPE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRETAILER_CREATE_TYPE });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PREPEAT_RECHARGE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pREPEAT_RECHARGE == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PEASY_BAZAR", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pEASY_BAZAR == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILER_REGISTRATION", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pRetailerRegistrationEnabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "POTP", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pOTP_ENABLED == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCOMPLIANCE_PASSWORD", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pCompliancePasswordEnabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "P789_SIM_REPLACEMENT", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.p789SimReplacementEnabled == "true" ? 1 : 0 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pAccessRights", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pAccessRights });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pUpdatedBy", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerMgtRequest.pUpdatedBy });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                        {

                            cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = System.Data.ParameterDirection.Output;

                        }
                        else
                        {
                            cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = System.Data.ParameterDirection.Output;
                        }

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
                    if (connection != null && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
                catch { }
            }
            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }

        public BaseResponse DeleteRetailer(string pR_ID, string pUpdatedBy)
        {

            BaseResponse response = new BaseResponse();

            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection connection = null;
            OracleCommand cmd = null;
            try
            {

                //    using (cmd = new NpgsqlCommand("CALL  PRC_RETAILER_LOGIN_INFO_SAVE(@PRETAILERCNIC,@PRETAILERID,@PRETAILERREGION,@PFSRETAILERID,@PFUNDAMOPOS,@PEAgreementState,@PBVMT_Agent_Sent,@PBVMT_Agent_Receive,@PWIFI_FALLBACK,@PSMS_FALLBACK,@PUPSELL,@pDAP,@PIRCASHWITHDRAWL,@PIRBVSVERIFICATION,@PIRPULLFUND,@P4GUPSELLCHECK,@PDAPACCUPSELL,@PRETAILER_CREATE,@PRSO_TYPE,@PRETAILER_CREATE_TYPE,@PREPEAT_RECHARGE,@PEASY_BAZAR,@PRETAILER_REGISTRATION,@POTP,@PCOMPLIANCE_PASSWORD,@P789_SIM_REPLACEMENT,@PACCESSRIGHTS,@PCREATEDBY)", connection = new NpgsqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
                // using (connection = new OracleConnection("data source=10.0.1.192:1521/demoaksa; User ID=MBPU_STG; Password=mbpu321;"))

                using (connection = new OracleConnection(_connectionString))
                {

                    using (cmd = new OracleCommand("PKG_RETAILER_MANAGMENT.PRC_RETAILER_LOGIN_INFO_DELETE", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRID", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = pR_ID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PENDEDBY", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = pUpdatedBy });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                        {

                            cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = System.Data.ParameterDirection.Output;

                        }
                        else
                        {
                            cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = System.Data.ParameterDirection.Output;
                        }

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
                    if (connection != null && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
                catch { }
            }
            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }

        public BaseResponse RetailerPasswardReset(RetailerPasswardReset retailerPasswardReset)
        {

            BaseResponse response = new BaseResponse();

            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection connection = null;
            OracleCommand cmd = null;
            try
            {
                retailerPasswardReset.pRetailerpasswordHash = _createHashByKey.CreateByKeyHash(retailerPasswardReset.pRetailerPassward, "TP1@#AKSA&*$");

                using (connection = new OracleConnection(_connectionString))
                {

                    using (cmd = new OracleCommand("PKG_RETAILER_MANAGMENT.PRC_RETAILER_RESET_PASSWORD", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRID", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = retailerPasswardReset.pRID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pRETAILER_CNIC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerPasswardReset.pRetailerCNIC });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pRETAILER_PASSWARD", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerPasswardReset.pRetailerPassward });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pRetailerpasswordHash", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerPasswardReset.pRetailerpasswordHash });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pUPDATED_BY", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = retailerPasswardReset.pUpdatedBy });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                        {

                            cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = System.Data.ParameterDirection.Output;

                        }
                        else
                        {
                            cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = System.Data.ParameterDirection.Output;
                        }

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
                    if (connection != null && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
                catch { }
            }
            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }

        public BaseResponse GetBindedRetailers(string mbid_No, string MSISDN_NO)
        {

            BaseResponse response = new BaseResponse();

            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection connection = null;
            try
            {

                using (connection = new OracleConnection(_connectionString))
                {


                    OracleCommand cmd = new OracleCommand("SELECT retailer_cnic FROM retailer_magicbox_mapping WHERE ended IS NULL and mbid ='" + MSISDN_NO + "'  and IMEI = '" + mbid_No + "'");
                    OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                    cmd.Connection = connection;

                    dataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        response.Message = "Y";
                    }
                    else
                    {
                        response.Message = "N";
                    }


                    connection.Dispose();
                    //connection = null;
           
                
                }

            }
            catch (Exception ex) { 
            
            }
            finally
            {
                try
                {
                    if (connection != null && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    if (connection != null)
                    {
                        connection.Dispose();
                    }
                }
                catch { }
            }

            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message, Data = null };

        }

        public BaseResponse GetRetailers(DataSourceRequest request)
        {
            BaseResponse response = new BaseResponse();
            var resultt = "";
            var pageNumber = request.Page;
            int PageSize = request.PageSize;
            bool isSuccess = true;
            string code = "00";
            string message = string.Empty;
            OracleConnection connection = null;
            OracleCommand cmd = null;
            try
            {

                //    using (cmd = new NpgsqlCommand("CALL  PRC_RETAILER_LOGIN_INFO_SAVE(@PRETAILERCNIC,@PRETAILERID,@PRETAILERREGION,@PFSRETAILERID,@PFUNDAMOPOS,@PEAgreementState,@PBVMT_Agent_Sent,@PBVMT_Agent_Receive,@PWIFI_FALLBACK,@PSMS_FALLBACK,@PUPSELL,@pDAP,@PIRCASHWITHDRAWL,@PIRBVSVERIFICATION,@PIRPULLFUND,@P4GUPSELLCHECK,@PDAPACCUPSELL,@PRETAILER_CREATE,@PRSO_TYPE,@PRETAILER_CREATE_TYPE,@PREPEAT_RECHARGE,@PEASY_BAZAR,@PRETAILER_REGISTRATION,@POTP,@PCOMPLIANCE_PASSWORD,@P789_SIM_REPLACEMENT,@PACCESSRIGHTS,@PCREATEDBY)", connection = new NpgsqlConnection(_Configuration.GetConnectionString("DefaultConnection")))
                // using (connection = new OracleConnection("data source=10.0.1.192:1521/demoaksa; User ID=MBPU_STG; Password=mbpu321;"))

                using (connection = new OracleConnection(_connectionString))
                {

                    using (cmd = new OracleCommand("PKG_RETAILER_MANAGMENT.PRC_GET_RETAILERS", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pREGION", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = request.UserRegion.ToUpper() });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pFILTER", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = request.Filter_col == "" ? null : request.Filter_col });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pPAGE_NO", Value = pageNumber, OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pPAGE_SIZE", Value = PageSize, OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pCount", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Output, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pCUR_RETAILERS", Value = "", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", Value = "", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", Value = "", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", Value = "", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        connection.Open();
                        OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                        cmd.Connection = connection;
                        dataAdapter.Fill(dt);
                        int RowCount = Convert.ToInt32(cmd.Parameters["pCount"].Value.ToString());
                        request.Page = 1;
                        //resultt = dt.ToDataSourceResult(request);
                        //resultt.Total = RowCount;
                        connection.Close();

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
                    if (connection != null && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
                catch { }
            }
            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }

        // private void ModifyFilters(IEnumerable<IFilterDescriptor> filters)
        //{
        //    try
        //    {


        //    Filter_col = "";
        //    if (filters.Any())
        //    {
        //        foreach (var filter in filters)
        //        {
        //            var descriptor = filter as FilterDescriptor;
        //            if (descriptor != null && descriptor.Member == "RID")// || descriptor.Member == "RETAILER_CNIC" || descriptor.Member == "RETAILER_ID" || descriptor.Member == "RETAILER_REGION" || descriptor.Member == "FS_RETAILER_ID" )
        //            {
        //                if (descriptor.Operator.ToString() == "IsEqualTo")
        //                {
        //                    Filter_col = "AND " + descriptor.Member.ToString() + " = " + "^" + descriptor.Value.ToString().Trim() + "^";
        //                }
        //                else if (descriptor.Operator.ToString() == "IsNotEqualTo")
        //                {
        //                    Filter_col = "AND " + descriptor.Member.ToString() + " != " + "^" + descriptor.Value.ToString().Trim() + "^";
        //                }
        //                else if (descriptor.Operator.ToString() == "Contains")
        //                {
        //                    Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^%" + descriptor.Value.ToString().Trim() + "%^";
        //                }
        //                else if (descriptor.Operator.ToString() == "StartsWith")
        //                {
        //                    Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^" + descriptor.Value.ToString().Trim() + "%^";
        //                }
        //                else if (descriptor.Operator.ToString() == "EndsWith")
        //                {
        //                    Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^%" + descriptor.Value.ToString().Trim() + "^";
        //                }
        //            }
        //            else if (descriptor != null && descriptor.Member == "RETAILER_CNIC")
        //                {
        //                    if (descriptor.Operator.ToString() == "IsEqualTo")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " = " + "^" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "IsNotEqualTo")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " != " + "^" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "Contains")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^%" + descriptor.Value.ToString().Trim() + "%^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "StartsWith")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^" + descriptor.Value.ToString().Trim() + "%^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "EndsWith")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^%" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                }
        //                else if (descriptor != null && descriptor.Member == "FS_RETAILER_ID")
        //                {
        //                    if (descriptor.Operator.ToString() == "IsEqualTo")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " = " + "^" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "IsNotEqualTo")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " != " + "^" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "Contains")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^%" + descriptor.Value.ToString().Trim() + "%^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "StartsWith")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^" + descriptor.Value.ToString().Trim() + "%^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "EndsWith")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^%" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                }
        //                else if (descriptor != null && descriptor.Member == "RETAILER_ID")
        //                {
        //                    if (descriptor.Operator.ToString() == "IsEqualTo")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " = " + "^" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "IsNotEqualTo")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " != " + "^" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "Contains")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^%" + descriptor.Value.ToString().Trim() + "%^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "StartsWith")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^" + descriptor.Value.ToString().Trim() + "%^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "EndsWith")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^%" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                }
        //                else if (descriptor != null && descriptor.Member == "RETAILER_REGION")
        //                {
        //                    if (descriptor.Operator.ToString() == "IsEqualTo")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " = " + "^" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "IsNotEqualTo")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " != " + "^" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "Contains")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^%" + descriptor.Value.ToString().Trim() + "%^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "StartsWith")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^" + descriptor.Value.ToString().Trim() + "%^";
        //                    }
        //                    else if (descriptor.Operator.ToString() == "EndsWith")
        //                    {
        //                        Filter_col = "AND " + descriptor.Member.ToString() + " LIKE  " + "^%" + descriptor.Value.ToString().Trim() + "^";
        //                    }
        //                }

        //                if (descriptor != null && descriptor.Member == "SomeField")
        //            {
        //                descriptor.Member = "SomeOtherField";
        //            }
        //            else if (filter is CompositeFilterDescriptor)
        //            {
        //                ModifyFilters(((CompositeFilterDescriptor)filter).FilterDescriptors);
        //            }
        //        }
        //    }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


    } 
}
            
    


