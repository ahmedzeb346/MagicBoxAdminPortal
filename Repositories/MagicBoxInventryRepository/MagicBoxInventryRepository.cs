using MagicBoxAdminPortal.Entities.MgtInventoryRequest;
using MagicBoxAdminPortal.Interfaces.MagicBoxInventry;
using MagicBoxAdminPortal.Repositories.RetailerMgtRepository;
using MagicBoxSupportApi.Helpers;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Repositories.MagicBoxInventryRepository
{
    public class MagicBoxInventryRepository : IMagicBoxInventryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        string Filter_col = "";
        DataTable dt = new DataTable();
        public MagicBoxInventryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public BaseResponse AddMagicBoxInventory(InventoryRequest inventoryRequest)
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
                    using (cmd = new OracleCommand("PKG_MAGICBOX_INVENTORY.PRC_MAGICBOX_INVENTORY_SAVE", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pMbid", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pMb_ID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pMbmsisdnno", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pMbmsisdnno });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pIccid", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pIccid });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pIsissued", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pIsissued });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pUnitid", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pUnitid });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pBidnings", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pBidnings });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pCreatedBy", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.UserNameCreatedBy });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                        {
                            response.Message = cmd.Parameters["PDESC"].Value.ToString();
                        }
                        else
                        {
                            response.Message = cmd.Parameters["PDESC"].Value.ToString();
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
            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }

        public BaseResponse UpdateMagicBoxInventory(InventoryRequest inventoryRequest)
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
                    using (cmd = new OracleCommand("PKG_MAGICBOX_INVENTORY.PRC_MAGICBOX_INVENTORY_UPDATE", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pMbid", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pMb_ID });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pMbmsisdnno", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pMbmsisdnno });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pIccid", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pIccid });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pIsissued", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pIsissued });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pUnitid", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pUnitid });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pBidnings", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pBidnings });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pCreatedBy", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = inventoryRequest.pCreatedBy });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                        {
                            response.Message = cmd.Parameters["PDESC"].Value.ToString();
                        }
                        else
                        {
                            response.Message = cmd.Parameters["PDESC"].Value.ToString();
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
            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }

        public BaseResponse GetMagicBoxInventory(DataSourceRequest request)
        {  
            BaseResponse response = new BaseResponse();
            var pageNumber = request.Page;
            int PageSize = request.PageSize;          
            var resultt = "";
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

                    using (cmd = new OracleCommand("PKG_MAGICBOX_INVENTORY.PRC_GET_MAGICBOX_INVENTORY", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pREGION", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = request.UserRegion.ToUpper() });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pFILTER", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = request.Filter_col == "" ? null : request.Filter_col });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pPAGE_NO", Value = pageNumber, OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pPAGE_SIZE", Value = PageSize, OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pCount", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Output, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pCUR_MAGICBOX_INVENTORY", Value = "", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", Value = "", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", Value = "", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", Value = "", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                        cmd.Connection = con;
                        dataAdapter.Fill(dt);
                        int RowCount = Convert.ToInt32(cmd.Parameters["pCount"].Value.ToString());
                        request.Page = 1;
                        //resultt = dt.ToDataSourceResult(request);
                        //resultt.Total = RowCount;
                        con.Close();
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
            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = message };
        }
    }
}
