using LinqToExcel;
using MagicBoxAdminPortal.Entities.GETTRANDETAILS;
using MagicBoxAdminPortal.Entities.ReadExcelMNP;
using MagicBoxAdminPortal.Interfaces.Support;
using MagicBoxSupportApi.Helpers;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Repositories.Support
{
    public class SupportRepository : ISupportRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        string Filter_col = "";
        DataTable dt = new DataTable();
        public SupportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public BaseResponse GetMappedData()
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
                    using (cmd = new OracleCommand("PKG_MAGICBOX_INVENTORY.PRC_GET_MAPPING_DATA", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "poCURSOR_MAPPING_DATA", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        OracleDataAdapter oAdapdter = new OracleDataAdapter(cmd);
                        oAdapdter.Fill(dt);
                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                        {
                            response.Message = cmd.Parameters["PDESC"].Value.ToString();
                        }
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
            return new BaseResponse { Code = code, IsSuccess = isSuccess, Message = response.Message };
        }

        public bool MNP_Data_Sync(string Desc)
        {
            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
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
                    using (cmd = new OracleCommand("PRC_BIZ_PARTNER_DATA_SYNC", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "pMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        cmd.ExecuteNonQuery();
                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                        {
                            Desc = cmd.Parameters["PDESC"].Value.ToString();
                            con.Close();
                        }
                        else
                        {
                            Desc = cmd.Parameters["PDESC"].Value.ToString();
                            con.Close();
                            return isSuccess = false;
                        }


                    }
                }
                return isSuccess = true;

            }
            catch (Exception ex)
            {
                Desc = ex.Message.ToString();
                return isSuccess = false;
            }
        }

        public DataTable Read_MNP_File(string filepath)
        {
            DataTable DT = new DataTable();
            DataRow dROW = null;

            DT.Columns.Add(new DataColumn("BIZ_PARTNER_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PARTNER_NAME", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PARTNER_DESC", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PART_ERP_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("END_DATE", typeof(string)));
            DT.Columns.Add(new DataColumn("REGION_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("CITY_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("CITY_NAME", typeof(string)));
            DT.Columns.Add(new DataColumn("ZONE_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("ZONE_NAME", typeof(string)));
            DT.Columns.Add(new DataColumn("AREA_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("AREA_NAME", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PARTNER_TYPE_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("PARENT_BP_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("PARENT_BP_CODE", typeof(string)));
            DT.Columns.Add(new DataColumn("AUTHORIZED_RETAILER", typeof(string)));
            DT.Columns.Add(new DataColumn("E_LOAD_MSISDN", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PARTNER_POSTAL_ADDRESS", typeof(string)));
            DT.Columns.Add(new DataColumn("CONTACT_PERSON", typeof(string)));
            DT.Columns.Add(new DataColumn("CONTACT_NUMBER", typeof(string)));
            DT.Columns.Add(new DataColumn("NTN_NO", typeof(string)));
            DT.Columns.Add(new DataColumn("NIC", typeof(string)));
            DT.Columns.Add(new DataColumn("PARENT_BIZ_PARTNER_TYPE_ID", typeof(string)));


            var excel = new ExcelQueryFactory(filepath);
            excel.ReadOnly = true;

            excel.AddMapping<ReadExcel_MNP>(x => x.BIZ_PARTNER_ID, "BIZ PARTNER_ID");
            excel.AddMapping("BIZ_PARTNER_ID", "BIZ_PARTNER_ID");
            excel.AddMapping("BIZ_PARTNER_DESC", "BIZ_PARTNER_DESC");
            excel.AddMapping("BIZ_PART_ERP_ID", "BIZ_PART_ERP_ID");
            excel.AddMapping("ERP_CODE", "ERP_CODE");
            excel.AddMapping("END_DATE", "END_DATE");
            excel.AddMapping("REGION_ID", "REGION_ID");
            excel.AddMapping("REGION_NAME", "REGION_NAME");
            excel.AddMapping("CITY_ID", "CITY_ID");
            excel.AddMapping("CITY_NAME", "CITY_NAME");
            excel.AddMapping("ZONE_ID", "ZONE_ID");
            excel.AddMapping("ZONE_NAME", "ZONE_NAME");
            excel.AddMapping("AREA_ID", "AREA_ID");
            excel.AddMapping("AREA_NAME", "AREA_NAME");
            excel.AddMapping("BIZ_PARTNER_TYPE_ID", "BIZ_PARTNER_TYPE_ID");
            excel.AddMapping("BIZ_PARTNER_CODE", "BIZ_PARTNER_CODE");
            excel.AddMapping("PARENT_BP_ID", "PARENT_BP_ID");
            excel.AddMapping("PARENT_BP_CODE", "PARENT_BP_CODE");
            excel.AddMapping("AUTHORIZED_RETAILER", "AUTHORIZED_RETAILER");
            excel.AddMapping("E_LOAD_MSISDN", "E_LOAD_MSISDN");
            excel.AddMapping("BIZ_PARTNER_POSTAL_ADDRESS", "BIZ_PARTNER_POSTAL_ADDRESS");
            excel.AddMapping("CONTACT_PERSON", "CONTACT_PERSON");
            excel.AddMapping("CONTACT_NUMBER", "CONTACT_NUMBER");
            excel.AddMapping("NTN_NO", "NTN_NO");
            excel.AddMapping("NIC", "NIC");
            excel.AddMapping("PARENT_BIZ_PARTNER_TYPE_ID", "PARENT_BIZ_PARTNER_TYPE_ID");


            var BVS_DATA = from c in excel.Worksheet<ReadExcel_MNP>(0)
                           select c;

            //var indianaCompanies = from c in excel.WorksheetNoHeader() select c;
            foreach (var data in BVS_DATA)
            {
                if (data.BIZ_PARTNER_ID == null && data.BIZ_PARTNER_NAME == null && data.BIZ_PARTNER_DESC == null && data.BIZ_PART_ERP_ID == null && data.END_DATE == null && data.REGION_ID == null && data.CITY_ID == null && data.CITY_NAME == null && data.ZONE_ID == null && data.ZONE_NAME == null && data.AREA_ID == null && data.AREA_NAME == null && data.BIZ_PARTNER_TYPE_ID == null && data.PARENT_BP_ID == null && data.PARENT_BP_CODE == null && data.AUTHORIZED_RETAILER == null && data.E_LOAD_MSISDN == null && data.BIZ_PARTNER_POSTAL_ADDRESS == null && data.CONTACT_PERSON == null && data.CONTACT_NUMBER == null && data.NTN_NO == null && data.NIC == null && data.PARENT_BIZ_PARTNER_TYPE_ID == null)
                {
                }
                else
                {
                    dROW = DT.NewRow();
                    if (data.BIZ_PARTNER_ID == null)
                    {
                        dROW["BIZ_PARTNER_ID"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_ID"] = data.BIZ_PARTNER_ID.ToString();
                    }
                    if (data.BIZ_PARTNER_NAME == null)
                    {
                        dROW["BIZ_PARTNER_NAME"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_NAME"] = data.BIZ_PARTNER_NAME.ToString();
                    }
                    if (data.BIZ_PARTNER_DESC == null)
                    {
                        dROW["BIZ_PARTNER_DESC"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_DESC"] = data.BIZ_PARTNER_DESC.ToString();
                    }

                    if (data.BIZ_PART_ERP_ID == null)
                    {
                        dROW["BIZ_PART_ERP_ID"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PART_ERP_ID"] = data.BIZ_PART_ERP_ID.ToString();
                    }

                    if (data.END_DATE == null)
                    {
                        dROW["END_DATE"] = "";
                    }
                    else
                    {
                        dROW["END_DATE"] = data.END_DATE.ToString();
                    }

                    if (data.REGION_ID == null)
                    {
                        dROW["REGION_ID"] = "";
                    }
                    else
                    {
                        dROW["REGION_ID"] = data.REGION_ID.ToString();
                    }

                    if (data.CITY_ID == null)
                    {
                        dROW["CITY_ID"] = "";
                    }
                    else
                    {
                        dROW["CITY_ID"] = data.CITY_ID.ToString();
                    }

                    if (data.CITY_NAME == null)
                    {
                        dROW["CITY_NAME"] = "";
                    }
                    else
                    {
                        dROW["CITY_NAME"] = data.CITY_NAME.ToString();
                    }

                    if (data.ZONE_ID == null)
                    {
                        dROW["ZONE_ID"] = "";
                    }
                    else
                    {
                        dROW["ZONE_ID"] = data.ZONE_ID.ToString();
                    }


                    if (data.ZONE_NAME == null)
                    {
                        dROW["ZONE_NAME"] = "";
                    }
                    else
                    {
                        dROW["ZONE_NAME"] = data.ZONE_NAME.ToString();
                    }

                    if (data.AREA_ID == null)
                    {
                        dROW["AREA_ID"] = "";
                    }
                    else
                    {
                        dROW["AREA_ID"] = data.AREA_ID.ToString();
                    }

                    if (data.AREA_NAME == null)
                    {
                        dROW["AREA_NAME"] = "";
                    }
                    else
                    {
                        dROW["AREA_NAME"] = data.AREA_NAME.ToString();
                    }


                    if (data.BIZ_PARTNER_TYPE_ID == null)
                    {
                        dROW["BIZ_PARTNER_TYPE_ID"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_TYPE_ID"] = data.BIZ_PARTNER_TYPE_ID.ToString();
                    }
                    if (data.PARENT_BP_ID == null)
                    {
                        dROW["PARENT_BP_ID"] = "";
                    }
                    else
                    {
                        dROW["PARENT_BP_ID"] = data.PARENT_BP_ID.ToString();
                    }

                    if (data.PARENT_BP_CODE == null)
                    {
                        dROW["PARENT_BP_CODE"] = "";
                    }
                    else
                    {
                        dROW["PARENT_BP_CODE"] = data.PARENT_BP_CODE.ToString();
                    }

                    if (data.AUTHORIZED_RETAILER == null)
                    {
                        dROW["AUTHORIZED_RETAILER"] = "";
                    }
                    else
                    {
                        dROW["AUTHORIZED_RETAILER"] = data.AUTHORIZED_RETAILER.ToString();
                    }

                    if (data.E_LOAD_MSISDN == null)
                    {
                        dROW["E_LOAD_MSISDN"] = "";
                    }
                    else
                    {
                        dROW["E_LOAD_MSISDN"] = data.E_LOAD_MSISDN.ToString();
                    }

                    if (data.BIZ_PARTNER_POSTAL_ADDRESS == null)
                    {
                        dROW["BIZ_PARTNER_POSTAL_ADDRESS"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_POSTAL_ADDRESS"] = data.BIZ_PARTNER_POSTAL_ADDRESS.ToString();
                    }

                    if (data.CONTACT_PERSON == null)
                    {
                        dROW["CONTACT_PERSON"] = "";
                    }
                    else
                    {
                        dROW["CONTACT_PERSON"] = data.CONTACT_PERSON.ToString();
                    }

                    if (data.CONTACT_NUMBER == null)
                    {
                        dROW["CONTACT_NUMBER"] = "";
                    }
                    else
                    {
                        dROW["CONTACT_NUMBER"] = data.CONTACT_NUMBER.ToString();
                    }


                    if (data.NTN_NO == null)
                    {
                        dROW["NTN_NO"] = "";
                    }
                    else
                    {
                        dROW["NTN_NO"] = data.NTN_NO.ToString();
                    }

                    if (data.NIC == null)
                    {
                        dROW["NIC"] = "";
                    }
                    else
                    {
                        dROW["NIC"] = data.NIC.ToString();
                    }

                    if (data.PARENT_BIZ_PARTNER_TYPE_ID == null)
                    {
                        dROW["PARENT_BIZ_PARTNER_TYPE_ID"] = "";
                    }
                    else
                    {
                        dROW["PARENT_BIZ_PARTNER_TYPE_ID"] = data.PARENT_BIZ_PARTNER_TYPE_ID.ToString();
                    }



                    DT.Rows.Add(dROW);
                }
            }

            //  var indianaCompanies = from c in excel.Worksheet<Company>() where c.State == "IN" select c; 
            return DT;

        }


        public bool Check_MNP_file(DataTable dt, ref string msg, ref DataTable dtError)
        {
            bool result = false;
            dtError = new DataTable();
            string count_Columns = "26";

            if (count_Columns == dt.Columns.Count.ToString())
            {

                // DataTable dtError = new DataTable();
                DataRow dROW = null;

                dtError.Columns.Add(new DataColumn("BIZ_PARTNER_ID", typeof(string)));
                dtError.Columns.Add(new DataColumn("BIZ_PARTNER_CODE", typeof(string)));
                dtError.Columns.Add(new DataColumn("E_LOAD_MSISDN", typeof(string)));
                dtError.Columns.Add(new DataColumn("AUTHORIZED_RETAILER", typeof(string)));//for TextBox value   
                dtError.Columns.Add(new DataColumn("PARENT_BP_ID", typeof(string)));
                dtError.Columns.Add(new DataColumn("NIC", typeof(string)));
                dtError.Columns.Add(new DataColumn("REGION_NAME", typeof(string)));
                dtError.Columns.Add(new DataColumn("PARENT_BP_CODE", typeof(string)));

                //dtError.Columns.Add(new DataColumn("REGION_ID", typeof(string)));
                //dtError.Columns.Add(new DataColumn("CITY_ID", typeof(string)));
                //dtError.Columns.Add(new DataColumn("ZONE_ID", typeof(string)));
                //dtError.Columns.Add(new DataColumn("AREA_ID", typeof(string)));
                //dtError.Columns.Add(new DataColumn("BIZ_PARTNER_TYPE_ID", typeof(string)));

                //dtError.Columns.Add(new DataColumn("PARENT_BIZ_PARTNER_TYPE_ID", typeof(string)));

                //dtError.Columns.Add(new DataColumn("BIZ_PARTNER_NAME", typeof(string)));
                //dtError.Columns.Add(new DataColumn("BIZ_PARTNER_DESC", typeof(string)));
                //dtError.Columns.Add(new DataColumn("ERP_CODE", typeof(string)));
                //dtError.Columns.Add(new DataColumn("END_DATE", typeof(string)));
                //dtError.Columns.Add(new DataColumn("REGION_NAME", typeof(string)));
                //dtError.Columns.Add(new DataColumn("CITY_NAME", typeof(string)));
                //dtError.Columns.Add(new DataColumn("ZONE_NAME", typeof(string)));
                //dtError.Columns.Add(new DataColumn("AREA_NAME", typeof(string)));
                //dtError.Columns.Add(new DataColumn("BIZ_PARTNER_POSTAL_ADDRESS", typeof(string)));
                //dtError.Columns.Add(new DataColumn("CONTACT_NUMBER", typeof(string)));
                //dtError.Columns.Add(new DataColumn("NTN_NO", typeof(string)));
                //dtError.Columns.Add(new DataColumn("NIC", typeof(string)));
                //dtError.Columns.Add(new DataColumn("BIZ_PARTNER_ID", typeof(string)));
                //dtError.Columns.Add(new DataColumn("BIZ_PARTNER_ID", typeof(string)));
                dtError.Columns.Add(new DataColumn("Reason", typeof(string)));




                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string BIZ_PARTNER_ID = row["BIZ_PARTNER_ID"].ToString();
                        string BIZ_PARTNER_CODE = row["BIZ_PARTNER_CODE"].ToString();
                        string E_LOAD_MSISDN = row["E_LOAD_MSISDN"].ToString();
                        string AUTHORIZED_RETAILER = row["AUTHORIZED_RETAILER"].ToString();
                        string PARENT_BP_ID = row["PARENT_BP_ID"].ToString();
                        string PARENT_BP_CODE = row["PARENT_BP_CODE"].ToString();
                        string NIC = row["NIC"].ToString();
                        string REGION_NAME = row["REGION_NAME"].ToString();

                        if (string.IsNullOrEmpty(BIZ_PARTNER_ID))
                        {
                            msg = msg + "," + "BIZ_PARTNER_ID";
                        }

                        if (string.IsNullOrEmpty(BIZ_PARTNER_CODE))
                        {
                            msg = msg + "," + "BIZ_PARTNER_CODE";
                        }

                        if (string.IsNullOrEmpty(E_LOAD_MSISDN))
                        {
                            msg = msg + "," + "E_LOAD_MSISDN";
                        }

                        if (string.IsNullOrEmpty(PARENT_BP_ID))
                        {
                            msg = msg + "," + "PARENT_BP_ID";
                        }

                        if (string.IsNullOrEmpty(PARENT_BP_CODE))
                        {
                            msg = msg + "," + "PARENT_BP_CODE";
                        }

                        if (string.IsNullOrEmpty(NIC))
                        {
                            msg = msg + "," + "NIC";
                        }
                        if (string.IsNullOrEmpty(REGION_NAME))
                        {
                            msg = msg + "," + "REGION_NAME";
                        }


                        if (msg != "")
                        {
                            msg = msg + " incorrect";

                            dROW = dtError.NewRow();
                            dROW["BIZ_PARTNER_ID"] = row["BIZ_PARTNER_ID"].ToString();
                            dROW["BIZ_PARTNER_CODE"] = row["BIZ_PARTNER_CODE"].ToString();
                            dROW["E_LOAD_MSISDN"] = row["E_LOAD_MSISDN"].ToString();
                            dROW["AUTHORIZED_RETAILER"] = row["AUTHORIZED_RETAILER"].ToString();
                            dROW["PARENT_BP_ID"] = row["PARENT_BP_ID"].ToString();
                            dROW["PARENT_BP_CODE"] = row["PARENT_BP_CODE"].ToString();
                            dROW["NIC"] = row["NIC"].ToString();
                            dROW["REGION_NAME"] = row["REGION_NAME"].ToString();
                            dROW["Reason"] = msg.ToString();
                            dtError.Rows.Add(dROW);

                            msg = "";
                        }

                    }
                }
                else
                {
                    msg = "File is Empty or Incorrect.";
                    result = false;

                    return result;
                }
            }
            else
            {
                msg = "File is Empty or Incorrect.";
                result = false;

                return result;
            }


            if (dtError.Rows.Count == 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;

        }


        //protected void rgUploaded_ItemCommand(object sender, GridCommandEventArgs e)
        //{
        //    if (e.CommandName = RadGrid.ExportToExcelCommandName)
        //    {

        //        e.Canceled = true;
        //        rgUploaded.ExportSettings.IgnorePaging = true;
        //        rgUploaded.ExportSettings.OpenInNewWindow = true;
        //        rgUploaded.MasterTableView.HierarchyDefaultExpanded = true;

        //        rgUploaded.MasterTableView.ExportToExcel();


        //    }
        //    else if (e.CommandName == RadGrid.ExportToCsvCommandName)
        //    {

        //        e.Canceled = true;
        //        rgUploaded.ExportSettings.IgnorePaging = true;
        //        rgUploaded.ExportSettings.OpenInNewWindow = true;
        //        rgUploaded.MasterTableView.HierarchyDefaultExpanded = true;

        //        rgUploaded.MasterTableView.ExportToCSV();

        //    }
        //}


        public DataTable ReadExcelFile(string filepath)
        {
            DataTable DT = new DataTable();
            DataRow dROW = null;

            DT.Columns.Add(new DataColumn("BIZ_PARTNER_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PARTNER_CODE", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PARTNER_NAME", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PARTNER_DESC", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PART_ERP_ID", typeof(string)));//for TextBox value   
            DT.Columns.Add(new DataColumn("ERP_CODE", typeof(string)));
            DT.Columns.Add(new DataColumn("END_DATE", typeof(string)));
            DT.Columns.Add(new DataColumn("REGION_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("REGION_NAME", typeof(string)));
            DT.Columns.Add(new DataColumn("CITY_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("CITY_NAME", typeof(string)));
            DT.Columns.Add(new DataColumn("ZONE_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("ZONE_NAME", typeof(string)));
            DT.Columns.Add(new DataColumn("AREA_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("AREA_NAME", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PARTNER_TYPE_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("PARENT_BP_ID", typeof(string)));
            DT.Columns.Add(new DataColumn("PARENT_BP_CODE", typeof(string)));
            DT.Columns.Add(new DataColumn("AUTHORIZED_RETAILER", typeof(string)));
            DT.Columns.Add(new DataColumn("E_LOAD_MSISDN", typeof(string)));
            DT.Columns.Add(new DataColumn("BIZ_PARTNER_POSTAL_ADDRESS", typeof(string)));
            DT.Columns.Add(new DataColumn("CONTACT_PERSON", typeof(string)));
            DT.Columns.Add(new DataColumn("CONTACT_NUMBER", typeof(string)));
            DT.Columns.Add(new DataColumn("NTN_NO", typeof(string)));
            DT.Columns.Add(new DataColumn("NIC", typeof(string)));
            DT.Columns.Add(new DataColumn("PARENT_BIZ_PARTNER_TYPE_ID", typeof(string)));

            var excel = new ExcelQueryFactory(filepath);
            excel.ReadOnly = true;

            excel.AddMapping<ReadExcel_MNP>(x => x.BIZ_PARTNER_ID, "BIZ_PARTNER_ID"); //maps the "State" property to the "Providence" column
            excel.AddMapping("BIZ_PARTNER_CODE", "BIZ_PARTNER_CODE");
            excel.AddMapping("BIZ_PARTNER_NAME", "BIZ_PARTNER_NAME");
            excel.AddMapping("BIZ_PARTNER_DESC", "BIZ_PARTNER_DESC");
            excel.AddMapping("BIZ_PART_ERP_ID", "BIZ_PART_ERP_ID");
            excel.AddMapping("ERP_CODE", "ERP_CODE");
            excel.AddMapping("END_DATE", "END_DATE");
            excel.AddMapping("REGION_ID", "REGION_ID");
            excel.AddMapping("REGION_NAME", "REGION_NAME");
            excel.AddMapping("CITY_ID", "CITY_ID");
            excel.AddMapping("CITY_NAME", "CITY_NAME");
            excel.AddMapping("ZONE_ID", "ZONE_ID");
            excel.AddMapping("ZONE_NAME", "ZONE_NAME");
            excel.AddMapping("AREA_ID", "AREA_ID");
            excel.AddMapping("AREA_NAME", "AREA_NAME");
            excel.AddMapping("BIZ_PARTNER_TYPE_ID", "BIZ_PARTNER_TYPE_ID");
            excel.AddMapping("PARENT_BP_ID", "PARENT_BP_ID");
            excel.AddMapping("PARENT_BP_CODE", "PARENT_BP_CODE");
            excel.AddMapping("AUTHORIZED_RETAILER", "AUTHORIZED_RETAILER");
            excel.AddMapping("E_LOAD_MSISDN", "E_LOAD_MSISDN");
            excel.AddMapping("BIZ_PARTNER_POSTAL_ADDRESS", "BIZ_PARTNER_POSTAL_ADDRESS");
            excel.AddMapping("CONTACT_PERSON", "CONTACT_PERSON");
            excel.AddMapping("CONTACT_NUMBER", "CONTACT_NUMBER");
            excel.AddMapping("NTN_NO", "NTN_NO");
            excel.AddMapping("NIC", "NIC");
            excel.AddMapping("PARENT_BIZ_PARTNER_TYPE_ID", "PARENT_BIZ_PARTNER_TYPE_ID");
            //***********************************************/////

            var BVS_DATA = from c in excel.Worksheet<ReadExcel_MNP>(0)
                           select c;

            //var indianaCompanies = from c in excel.WorksheetNoHeader() select c;
            foreach (var data in BVS_DATA)
            {
                if (data.BIZ_PARTNER_ID == null && data.BIZ_PARTNER_CODE == null && data.BIZ_PARTNER_NAME == null && data.BIZ_PARTNER_DESC == null && data.BIZ_PART_ERP_ID == null && data.ERP_CODE == null && data.END_DATE == null && data.REGION_ID == null && data.REGION_NAME == null && data.CITY_ID == null && data.CITY_NAME == null && data.ZONE_ID == null && data.ZONE_NAME == null && data.AREA_ID == null && data.AREA_NAME == null && data.BIZ_PARTNER_TYPE_ID == null && data.BIZ_PARTNER_CODE == null && data.PARENT_BP_ID == null && data.PARENT_BP_CODE == null && data.AUTHORIZED_RETAILER == null && data.E_LOAD_MSISDN == null && data.BIZ_PARTNER_POSTAL_ADDRESS == null && data.CONTACT_PERSON == null && data.CONTACT_NUMBER == null && data.NTN_NO == null && data.NIC == null && data.PARENT_BIZ_PARTNER_TYPE_ID == null)
                {
                }
                else
                {

                    //////BIZ_PARTNER_ID
                    dROW = DT.NewRow();
                    if (data.BIZ_PARTNER_ID == null)
                    {
                        dROW["BIZ_PARTNER_ID"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_ID"] = data.BIZ_PARTNER_ID.ToString();
                    }

                    //////BIZ_PARTNER_CODE
                    if (data.BIZ_PARTNER_CODE == null)
                    {
                        dROW["BIZ_PARTNER_CODE"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_CODE"] = data.BIZ_PARTNER_CODE.ToString();
                    }

                    //////BIZ_PARTNER_NAME
                    if (data.BIZ_PARTNER_NAME == null)
                    {
                        dROW["BIZ_PARTNER_NAME"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_NAME"] = data.BIZ_PARTNER_NAME.ToString();
                    }


                    //////BIZ_PARTNER_DESC
                    if (data.BIZ_PARTNER_DESC == null)
                    {
                        dROW["BIZ_PARTNER_DESC"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_DESC"] = data.BIZ_PARTNER_DESC.ToString();
                    }


                    //////BIZ_PART_ERP_ID
                    if (data.BIZ_PART_ERP_ID == null)
                    {
                        dROW["BIZ_PART_ERP_ID"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PART_ERP_ID"] = data.BIZ_PART_ERP_ID.ToString();
                    }


                    //////ERP_CODE
                    if (data.ERP_CODE == null)
                    {
                        dROW["ERP_CODE"] = "";
                    }
                    else
                    {
                        dROW["ERP_CODE"] = data.ERP_CODE.ToString();
                    }


                    //////END_DATE
                    if (data.END_DATE == null)
                    {
                        dROW["END_DATE"] = "";
                    }
                    else
                    {
                        dROW["END_DATE"] = data.END_DATE.ToString();
                    }

                    //////REGION_ID
                    if (data.REGION_ID == null)
                    {
                        dROW["REGION_ID"] = "";
                    }
                    else
                    {
                        dROW["REGION_ID"] = data.REGION_ID.ToString();
                    }

                    //////REGION_NAME
                    if (data.REGION_NAME == null)
                    {
                        dROW["REGION_NAME"] = "";
                    }
                    else
                    {
                        dROW["REGION_NAME"] = data.REGION_NAME.ToString();
                    }

                    //////CITY_ID
                    if (data.CITY_ID == null)
                    {
                        dROW["CITY_ID"] = "";
                    }
                    else
                    {
                        dROW["CITY_ID"] = data.CITY_ID.ToString();
                    }

                    //////CITY_NAME
                    if (data.CITY_NAME == null)
                    {
                        dROW["CITY_NAME"] = "";
                    }
                    else
                    {
                        dROW["CITY_NAME"] = data.CITY_NAME.ToString();
                    }

                    //////ZONE_ID
                    if (data.ZONE_ID == null)
                    {
                        dROW["ZONE_ID"] = "";
                    }
                    else
                    {
                        dROW["ZONE_ID"] = data.ZONE_ID.ToString();
                    }

                    //////ZONE_NAME
                    if (data.ZONE_NAME == null)
                    {
                        dROW["ZONE_NAME"] = "";
                    }
                    else
                    {
                        dROW["ZONE_NAME"] = data.ZONE_NAME.ToString();
                    }

                    //////AREA_ID
                    if (data.AREA_ID == null)
                    {
                        dROW["AREA_ID"] = "";
                    }
                    else
                    {
                        dROW["AREA_ID"] = data.AREA_ID.ToString();
                    }

                    //////AREA_NAME
                    if (data.AREA_NAME == null)
                    {
                        dROW["AREA_NAME"] = "";
                    }
                    else
                    {
                        dROW["AREA_NAME"] = data.AREA_NAME.ToString();
                    }

                    //////BIZ_PARTNER_TYPE_ID
                    if (data.BIZ_PARTNER_TYPE_ID == null)
                    {
                        dROW["BIZ_PARTNER_TYPE_ID"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_TYPE_ID"] = data.BIZ_PARTNER_TYPE_ID.ToString();
                    }
                    //////PARENT_BP_ID
                    if (data.PARENT_BP_ID == null)
                    {
                        dROW["PARENT_BP_ID"] = "";
                    }
                    else
                    {
                        dROW["PARENT_BP_ID"] = data.PARENT_BP_ID.ToString();
                    }

                    //////PARENT_BP_CODE
                    if (data.PARENT_BP_CODE == null)
                    {
                        dROW["PARENT_BP_CODE"] = "";
                    }
                    else
                    {
                        dROW["PARENT_BP_CODE"] = data.PARENT_BP_CODE.ToString();
                    }
                    //////AUTHORIZED_RETAILER
                    if (data.AUTHORIZED_RETAILER == null)
                    {
                        dROW["AUTHORIZED_RETAILER"] = "";
                    }
                    else
                    {
                        dROW["AUTHORIZED_RETAILER"] = data.AUTHORIZED_RETAILER.ToString();
                    }

                    //////E_LOAD_MSISDN
                    if (data.E_LOAD_MSISDN == null)
                    {
                        dROW["E_LOAD_MSISDN"] = "";
                    }
                    else
                    {
                        dROW["E_LOAD_MSISDN"] = data.E_LOAD_MSISDN.ToString();
                    }
                    //////BIZ_PARTNER_POSTAL_ADDRESS
                    if (data.BIZ_PARTNER_POSTAL_ADDRESS == null)
                    {
                        dROW["BIZ_PARTNER_POSTAL_ADDRESS"] = "";
                    }
                    else
                    {
                        dROW["BIZ_PARTNER_POSTAL_ADDRESS"] = data.BIZ_PARTNER_POSTAL_ADDRESS.ToString();
                    }

                    //////CONTACT_PERSON
                    if (data.CONTACT_PERSON == null)
                    {
                        dROW["CONTACT_PERSON"] = "";
                    }
                    else
                    {
                        dROW["CONTACT_PERSON"] = data.CONTACT_PERSON.ToString();
                    }

                    //////CONTACT_NUMBER
                    if (data.CONTACT_NUMBER == null)
                    {
                        dROW["CONTACT_NUMBER"] = "";
                    }
                    else
                    {
                        dROW["CONTACT_NUMBER"] = data.CONTACT_NUMBER.ToString();
                    }

                    //////NTN_NO
                    if (data.NTN_NO == null)
                    {
                        dROW["NTN_NO"] = "";
                    }
                    else
                    {
                        dROW["NTN_NO"] = data.NTN_NO.ToString();
                    }

                    //////NIC
                    if (data.NIC == null)
                    {
                        dROW["NIC"] = "";
                    }
                    else
                    {
                        dROW["NIC"] = data.NIC.ToString();
                    }
                    //////PARENT_BIZ_PARTNER_TYPE_ID
                    if (data.PARENT_BIZ_PARTNER_TYPE_ID == null)
                    {
                        dROW["PARENT_BIZ_PARTNER_TYPE_ID"] = "";
                    }
                    else
                    {
                        dROW["PARENT_BIZ_PARTNER_TYPE_ID"] = data.PARENT_BIZ_PARTNER_TYPE_ID.ToString();
                    }



                    DT.Rows.Add(dROW);
                }
            }


            return DT;

        }

        ////////CLSDB/////////////

        public BaseResponse GetAuthentication(string username, string pwd, string uSERID, string pAGES)
        {
            //   string METHOD = "AKSA/GetPendingSaf";
            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string message = string.Empty;
            OracleConnection conn = null;
            OracleCommand cmd = null;
            string PKG_PRC = "PKG_WEB.PRC_USER_AUTHENTICATE";
            DataTable dt = new DataTable();
            //   string uSERID;

            try
            {

                using (conn = new OracleConnection(_connectionString))
                {
                    using (cmd = new OracleCommand(PKG_PRC, conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandTimeout = 99999999;


                        cmd.Parameters.Add("PUSER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = username;
                        cmd.Parameters.Add("PPASSWORD", OracleDbType.Varchar2, ParameterDirection.Input).Value = pwd;

                        cmd.Parameters.Add("PUSERID", OracleDbType.Int32, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PPAGE", OracleDbType.Int32, 1000).Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("PCODE", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PMSG", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                        dt = new DataTable();

                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {

                            da.Fill(dt);
                            pAGES = cmd.Parameters["PPAGE"].Value != null ? cmd.Parameters["PPAGE"].Value.ToString() : "";
                            uSERID = cmd.Parameters["PUSERID"].Value != null ? cmd.Parameters["PUSERID"].Value.ToString() : "";
                            string sCODE = cmd.Parameters["PCODE"].Value != null ? cmd.Parameters["PCODE"].Value.ToString() : "";
                            string sDESC = cmd.Parameters["PDESC"].Value != null ? cmd.Parameters["PDESC"].Value.ToString() : "";
                            string sMSG = cmd.Parameters["PMSG"].Value != null ? cmd.Parameters["PMSG"].Value.ToString() : "";

                            if (sMSG != "Y")
                            {
                            }
                            else
                            {

                                if (sCODE != "00")
                                {
                                    //LOG ERROR

                                }
                            }
                            // return uSERID;
                        }


                    }

                }

            }
            catch (Exception ex)
            {
                //LOG ERROR
               // Log.LogError(ex.Message, PKG_PRC);
                // clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf ", ex.ToString());
            }

            return null;
        }

        public void GetErrorCode(string API_NAME, ref DataTable dt)
        {
            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string message = string.Empty;
            OracleConnection conn = null;
            OracleCommand cmd = null;
            string PKG_PRC = "PKG_WEB.PRC_GET_ERROR_CODE";
            // DataTable dt = new DataTable();


            try
            {

                using (conn = new OracleConnection(_connectionString))
                {
                    using (cmd = new OracleCommand(PKG_PRC, conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandTimeout = 99999999;


                        cmd.Parameters.Add("PAPI_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = API_NAME;


                        cmd.Parameters.Add("PCUR_LIST", OracleDbType.RefCursor, 1000).Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("PCODE", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PMSG", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {

                            da.Fill(dt);

                            // uSERID = cmd.Parameters["PUSERID"].Value != null ? cmd.Parameters["PUSERID"].Value.ToString() : "";
                            string sCODE = cmd.Parameters["PCODE"].Value != null ? cmd.Parameters["PCODE"].Value.ToString() : "";
                            string sDESC = cmd.Parameters["PDESC"].Value != null ? cmd.Parameters["PDESC"].Value.ToString() : "";
                            string sMSG = cmd.Parameters["PMSG"].Value != null ? cmd.Parameters["PMSG"].Value.ToString() : "";

                            if (sMSG != "Y")
                            {
                                // clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf " + PKG_PRC, sDESC);


                            }
                            else
                            {

                                if (sCODE != "00")
                                {
                                    //LOG ERROR
                                    //  clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf " + PKG_PRC, sCODE);
                                }
                            }
                            //return uSERID;
                        }


                    }

                }


            }
            catch (Exception ex)
            {
                //LOG ERROR
                //Log.LogError(ex.Message, PKG_PRC);
                // clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf ", ex.ToString());
            }
        }

        public void GetXMPResponse(string tran_id, ref string resp)
        {
            //   string METHOD = "AKSA/GetPendingSaf";
            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string message = string.Empty;
            OracleConnection conn = null;
            OracleCommand cmd = null;
            string PKG_PRC = "PKG_WEB.PRC_GET_XML_RESP";
            DataTable dt = new DataTable();


            try
            {

                using (conn = new OracleConnection(_connectionString))
                {
                    using (cmd = new OracleCommand(PKG_PRC, conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandTimeout = 99999999;


                        cmd.Parameters.Add("PTRANID", OracleDbType.Varchar2, ParameterDirection.Input).Value = tran_id;


                        cmd.Parameters.Add("PXML", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("PCODE", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PMSG", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                        conn.Open();
                        // cmd.Connection = conn;
                        cmd.ExecuteNonQuery();

                        resp = cmd.Parameters["PXML"].Value != null ? cmd.Parameters["PXML"].Value.ToString() : "";
                        string sCODE = cmd.Parameters["PCODE"].Value != null ? cmd.Parameters["PCODE"].Value.ToString() : "";
                        string sDESC = cmd.Parameters["PDESC"].Value != null ? cmd.Parameters["PDESC"].Value.ToString() : "";
                        string sMSG = cmd.Parameters["PMSG"].Value != null ? cmd.Parameters["PMSG"].Value.ToString() : "";

                        if (sMSG != "Y")
                        {
                            // clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf " + PKG_PRC, sDESC);


                        }
                        else
                        {

                            if (sCODE != "00")
                            {
                                //LOG ERROR
                                //  clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf " + PKG_PRC, sCODE);
                            }
                        }
                        //return uSERID;



                    }

                }


            }
            catch (Exception ex)
            {
                //LOG ERROR
               // Log.LogError(ex.Message, PKG_PRC);
                // clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf ", ex.ToString());
            }

            //return null;
        }

        public void PRC_GET_TRAN_DETAILS(GETTRANDETAILS gETTRANDETAILS, ref DataTable dt)
        {
            //   string METHOD = "AKSA/GetPendingSaf";
            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string message = string.Empty;
            OracleConnection conn = null;
            OracleCommand cmd = null;
            string PKG_PRC = "PKG_WEB.PRC_GET_TRAN_DETAILS";
            // DataTable dt = new DataTable();


            try
            {

                using (conn = new OracleConnection(_connectionString))
                {
                    using (cmd = new OracleCommand(PKG_PRC, conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandTimeout = 99999999;


                        cmd.Parameters.Add("PSTART_DATE", OracleDbType.TimeStamp, ParameterDirection.Input).Value = string.IsNullOrEmpty(Convert.ToString(gETTRANDETAILS.startDate)) ? (object)DBNull.Value : Convert.ToDateTime(gETTRANDETAILS.startDate);

                        cmd.Parameters.Add("PEND_DATE", OracleDbType.TimeStamp, ParameterDirection.Input).Value = string.IsNullOrEmpty(Convert.ToString(gETTRANDETAILS.endDate)) ? (object)DBNull.Value : Convert.ToDateTime(gETTRANDETAILS.endDate);

                        cmd.Parameters.Add("PCNIC", OracleDbType.Varchar2, ParameterDirection.Input).Value = string.IsNullOrEmpty(gETTRANDETAILS.cnic) ? (object)DBNull.Value : gETTRANDETAILS.cnic;

                        cmd.Parameters.Add("PAKSACODE", OracleDbType.Int32, ParameterDirection.Input).Value = string.IsNullOrEmpty(gETTRANDETAILS.aksacode) ? (object)DBNull.Value : Convert.ToInt32(gETTRANDETAILS.aksacode);

                        cmd.Parameters.Add("PNADRACODE", OracleDbType.Varchar2, ParameterDirection.Input).Value =

                        string.IsNullOrEmpty(gETTRANDETAILS.nadracode) ? (object)DBNull.Value : gETTRANDETAILS.nadracode;

                        cmd.Parameters.Add("PTRANID", OracleDbType.Int32, ParameterDirection.Input).Value =
                       string.IsNullOrEmpty(gETTRANDETAILS.tran_id) ? (object)DBNull.Value : Convert.ToInt32(gETTRANDETAILS.tran_id);

                        cmd.Parameters.Add("PCLIENTIP", OracleDbType.Varchar2, ParameterDirection.Input).Value =
                        string.IsNullOrEmpty(gETTRANDETAILS.client_Ip) ? (object)DBNull.Value : (gETTRANDETAILS.client_Ip);

                        cmd.Parameters.Add("PCUR_DATA", OracleDbType.RefCursor, 1000).Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("PCODE", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PMSG", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {

                            da.Fill(dt);

                            // uSERID = cmd.Parameters["PUSERID"].Value != null ? cmd.Parameters["PUSERID"].Value.ToString() : "";
                            string sCODE = cmd.Parameters["PCODE"].Value != null ? cmd.Parameters["PCODE"].Value.ToString() : "";
                            string sDESC = cmd.Parameters["PDESC"].Value != null ? cmd.Parameters["PDESC"].Value.ToString() : "";
                            string sMSG = cmd.Parameters["PMSG"].Value != null ? cmd.Parameters["PMSG"].Value.ToString() : "";

                            if (sMSG != "Y")
                            {
                                // clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf " + PKG_PRC, sDESC);


                            }
                            else
                            {

                                if (sCODE != "00")
                                {
                                    //LOG ERROR
                                    //  clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf " + PKG_PRC, sCODE);
                                }
                            }
                            //return uSERID;
                        }


                    }

                }


            }
            catch (Exception ex)
            {
                //LOG ERROR
              //  Log.LogError(ex.Message, PKG_PRC);
                // clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf ", ex.ToString());
            }

            //return null;
        }

        public void PRC_GET_DATE_TOTAL(string startDate, string endDate, ref DataTable dt)
        {
            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string message = string.Empty;
            OracleConnection conn = null;
            OracleCommand cmd = null;
            string PKG_PRC = "PKG_WEB.PRC_GET_DAY_COUNT";
            // DataTable dt = new DataTable();


            try
            {

                using (conn = new OracleConnection(_connectionString))
                {
                    using (cmd = new OracleCommand(PKG_PRC, conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandTimeout = 99999999;


                        cmd.Parameters.Add("PSTART_DATE", OracleDbType.TimeStamp, ParameterDirection.Input).Value = string.IsNullOrEmpty(Convert.ToString(startDate)) ? (object)DBNull.Value : Convert.ToDateTime(startDate);
                        cmd.Parameters.Add("PEND_DATE", OracleDbType.TimeStamp, ParameterDirection.Input).Value = string.IsNullOrEmpty(Convert.ToString(endDate)) ? (object)DBNull.Value : Convert.ToDateTime(endDate);

                        // cmd.Parameters.Add("PCLIENTIP", OracleDbType.TimeStamp, ParameterDirection.Input).Value = client_Ip;


                        cmd.Parameters.Add("PCUR_DATA", OracleDbType.RefCursor, 1000).Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("PCODE", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PDESC", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("PMSG", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                        using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                        {

                            da.Fill(dt);

                            // uSERID = cmd.Parameters["PUSERID"].Value != null ? cmd.Parameters["PUSERID"].Value.ToString() : "";
                            string sCODE = cmd.Parameters["PCODE"].Value != null ? cmd.Parameters["PCODE"].Value.ToString() : "";
                            string sDESC = cmd.Parameters["PDESC"].Value != null ? cmd.Parameters["PDESC"].Value.ToString() : "";
                            string sMSG = cmd.Parameters["PMSG"].Value != null ? cmd.Parameters["PMSG"].Value.ToString() : "";

                            if (sMSG != "Y")
                            {
                                // clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf " + PKG_PRC, sDESC);


                            }
                            else
                            {

                                if (sCODE != "00")
                                {
                                    //LOG ERROR
                                    //  clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf " + PKG_PRC, sCODE);
                                }
                            }
                            //return uSERID;
                        }


                    }

                }


            }
            catch (Exception ex)
            {
                //LOG ERROR
               // Log.LogError(ex.Message, PKG_PRC);
                // clsWriteLogFile.WriteLine_AKSA_clsA_ErrorLog("AKSA/GetPendingSaf ", ex.ToString());
            }

            //return null;
        }

        ////////clsGeneral/////////////

        public DataTable Get_Retailer_Info(string imei)
        {
            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string desc = "";
            string msg = "";
            string message = string.Empty;
            OracleConnection conn = null;
            OracleCommand cmd = null;
            try
            {

               // string _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringOra"].ToString();
                OracleConnection con = null;
                using (conn = new OracleConnection(_connectionString))
                {
                    /*  PKG_APK_PUSH   CREATE OR REPLACE PROCEDURE PRC_GET_RETAILER_INFO 

                            PIMEI IN VARCHAR2,
                            PCUR_INFO OUT SYS_REFCURSOR,
                            PCODE       OUT VARCHAR2,
                            PDESC       OUT VARCHAR2,
                            PMSG        OUT VARCHAR2 
                            */


                    using (cmd = new OracleCommand("PKG_APK_PUSH.PRC_GET_RETAILER_INFO", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIMEI", Value = imei.Trim(), OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCUR_INFO", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                        cmd.Connection = con;
                        dataAdapter.Fill(dt);
                        msg = cmd.Parameters["PMSG"].Value.ToString();
                        desc = cmd.Parameters["PDESC"].Value.ToString();
                        code = cmd.Parameters["PCODE"].Value.ToString();
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return dt;
        }

        public  string APK_Push_DB_Call(APKPushDBCall aPKPushDBCall)
        {
            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string desc = "";
            string msg = "";
            string message = string.Empty;
            OracleConnection conn = null;
            OracleCommand cmd = null;
          
            try
            {

                //string _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringOra"].ToString();
                //OracleConnection con = null;
                using (conn = new OracleConnection(_connectionString))
                {
                    /*  PKG_APK_PUSH  PROCEDURE PRC_APK_PUSH 
                                (
                                  PIMEI       IN VARCHAR2,
                                  PVERSION_NO       IN VARCHAR2,
                                  PAPK_PUSH       IN VARCHAR2,
                                  PCODE       OUT VARCHAR2,
                                  PDESC       OUT VARCHAR2,
                                  PMSG        OUT VARCHAR2 
                                );
                                                        */


                    using (cmd = new OracleCommand("PKG_APK_PUSH.PRC_APK_PUSH", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIMEI", Value = aPKPushDBCall.imei.Trim(), OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PVERSION_NO", Value = aPKPushDBCall.Version_no.Trim(), OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PAPK_PUSH", Value = aPKPushDBCall.APK_Push.Trim(), OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PUPDATED_BY", Value = aPKPushDBCall.Updated_BY, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        conn.Open();
                        //  OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                        cmd.Connection = conn;
                        //dataAdapter.Fill(dt);
                        cmd.ExecuteNonQuery();
                        msg = cmd.Parameters["PMSG"].Value.ToString();
                        desc = cmd.Parameters["PDESC"].Value.ToString();
                        code = cmd.Parameters["PCODE"].Value.ToString();
                    }
                }
                return desc;
            }
            catch (Exception ex)
            {
                desc = ex.Message.ToString();
                return desc;
            }

            return desc;
        }

        public string Enable_AGENT_BVMT_FLOWS(int pBIT)
        {

            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string desc = "";
            string msg = "";
            string message = string.Empty;
            OracleConnection con = null;
            OracleCommand cmd = null;
            try
            {

                //string _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringOra"].ToString();
               
                using (con = new OracleConnection(_connectionString))
                {
                    /*  PKG_APK_PUSH  PROCEDURE PRC_ENABLE_BVMT_AGENT_FLOWS 
                                                (
                                                  PBIT       IN NUMBER,
                                                  PCODE       OUT VARCHAR2,
                                                  PDESC       OUT VARCHAR2,
                                                  PMSG        OUT VARCHAR2 
                                                );
                                                        */


                    using (cmd = new OracleCommand("PKG_APK_PUSH.PRC_ENABLE_BVMT_AGENT_FLOWS", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PBIT", Value = pBIT, OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        msg = cmd.Parameters["PMSG"].Value.ToString();
                        desc = cmd.Parameters["PDESC"].Value.ToString();
                        code = cmd.Parameters["PCODE"].Value.ToString();
                    }
                }
                return desc;
            }
            catch (Exception ex)
            {
                desc = ex.Message.ToString();
                return desc;
            }

            return desc;
        }

        public string DISABLE_AGENT_BVMT_FLOWS(int pBIT)
        {

            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string desc = "";
            string msg = "";
            string message = string.Empty;
            OracleConnection con = null;
            OracleCommand cmd = null;
            try
            {

                //string _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringOra"].ToString();
                //OracleConnection con = null;
                using (con = new OracleConnection(_connectionString))
                {
                    /*  PKG_APK_PUSH  PROCEDURE PRC_DISABLE_BVMT_AGENT_FLOWS 
                                            (
                                              PBIT       IN NUMBER,
                                              PCODE       OUT VARCHAR2,
                                              PDESC       OUT VARCHAR2,
                                              PMSG        OUT VARCHAR2 
                                            );

                                                        */


                    using (cmd = new OracleCommand("PKG_APK_PUSH.PRC_DISABLE_BVMT_AGENT_FLOWS", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PBIT", Value = pBIT, OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        msg = cmd.Parameters["PMSG"].Value.ToString();
                        desc = cmd.Parameters["PDESC"].Value.ToString();
                        code = cmd.Parameters["PCODE"].Value.ToString();
                    }
                }
                return desc;
            }
            catch (Exception ex)
            {
                desc = ex.Message.ToString();
                return desc;
            }

            return desc;
        }

        public string Ufone_JV_Access_DB_Call(string imei, string Ufno_JV)
        {

            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string desc = "";
            string msg = "";
            string message = string.Empty;
            OracleConnection con = null;
            OracleCommand cmd = null;
            try
            {

                //string _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringOra"].ToString();
                //OracleConnection con = null;
                using (con = new OracleConnection(_connectionString))
                {
                    /*  PKG_APK_PUSH  PROCEDURE PRC_ACCESS_UFONE_JV 
                                                (
                                                  PIMEI       IN VARCHAR2,
                                                  PUFONE_JV       IN VARCHAR2,
                                                  PCODE       OUT VARCHAR2,
                                                  PDESC       OUT VARCHAR2,
                                                  PMSG        OUT VARCHAR2 
                                                );
                                                        */


                    using (cmd = new OracleCommand("PKG_APK_PUSH.PRC_ACCESS_UFONE_JV", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIMEI", Value = imei.Trim(), OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PUFONE_JV", Value = Ufno_JV.Trim(), OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        //  OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                        cmd.Connection = con;
                        //dataAdapter.Fill(dt);
                        cmd.ExecuteNonQuery();
                        msg = cmd.Parameters["PMSG"].Value.ToString();
                        desc = cmd.Parameters["PDESC"].Value.ToString();
                        code = cmd.Parameters["PCODE"].Value.ToString();
                    }
                }
                return desc;
            }
            catch (Exception ex)
            {
                desc = ex.Message.ToString();
                return desc;
            }

            return desc;
        }
        public string IR_FlOW_Access_DB_Call(IRFlOWAccessDBCall iRFlOWAccessDBCall)
        {

            BaseResponse response = new BaseResponse();
            bool isSuccess = false;
            string code = "00";
            string desc = "";
            string msg = "";
            string message = string.Empty;
            OracleConnection con = null;
            OracleCommand cmd = null;
            try
            {

                //string _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringOra"].ToString();
                //OracleConnection con = null;
                using (con = new OracleConnection(_connectionString))
                {
                    /*  PKG_APK_PUSH   PROCEDURE PRC_IR_ENABLE_DISABLE 
                    (
                      PRETAILER_CNIC       IN VARCHAR2,
                      PIR_CASHWITHDRAWAL   IN VARCHAR2,
                      PIR_BVS_VERFICATION  IN VARCHAR2,
                      PIR_PULL_FUND        IN VARCHAR2,
                      PCODE                OUT VARCHAR2,
                      PDESC                OUT VARCHAR2,
                      PMSG                 OUT VARCHAR2 
                    ) 
                                                                        */


                    using (cmd = new OracleCommand("PKG_APK_PUSH.PRC_IR_ENABLE_DISABLE", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PRETAILER_CNIC", Value = iRFlOWAccessDBCall.Retailer_CNIC.Trim(), OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIR_CASHWITHDRAWAL", Value = iRFlOWAccessDBCall.IR_Cashwithdrawal.Trim(), OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIR_BVS_VERFICATION", Value = iRFlOWAccessDBCall.IR_bvs_verification.Trim(), OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PIR_PULL_FUND", Value = iRFlOWAccessDBCall.R_Pull_Fund.Trim(), OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Size = 100 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        //  OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                        cmd.Connection = con;
                        //dataAdapter.Fill(dt);
                        cmd.ExecuteNonQuery();
                        msg = cmd.Parameters["PMSG"].Value.ToString();
                        desc = cmd.Parameters["PDESC"].Value.ToString();
                        code = cmd.Parameters["PCODE"].Value.ToString();
                    }
                }
                return desc;
            }
            catch (Exception ex)
            {
                desc = ex.Message.ToString();
                return desc;
            }

            return desc;
        }

        // login

        public bool Validate_LOGIN(ValidateLOGIN validateLOGIN)
        {

            try
            {

                OracleCommand cmd = new OracleCommand("PKG_MB_CONFIGURATIONS.PRC_FRONTEND_LOGIN");
                cmd.CommandType = CommandType.StoredProcedure;


                OracleParameter pram_IN_1 = new OracleParameter("pUser_Name", OracleDbType.Varchar2);
                pram_IN_1.Direction = ParameterDirection.Input;
                pram_IN_1.Value = validateLOGIN.UName;
                cmd.Parameters.Add(pram_IN_1);

                OracleParameter pram_IN_2 = new OracleParameter("PUSER_PWD", OracleDbType.Varchar2);
                pram_IN_2.Direction = ParameterDirection.Input;
                pram_IN_2.Value = validateLOGIN.Password;
                cmd.Parameters.Add(pram_IN_2);

                OracleParameter pram_IN_3 = new OracleParameter("pSession_ID", OracleDbType.Varchar2);
                pram_IN_3.Direction = ParameterDirection.Input;
                pram_IN_3.Value = validateLOGIN.SessionID;
                cmd.Parameters.Add(pram_IN_3);



                OracleParameter pram_OUT_12 = new OracleParameter("pAccess_Level_P", OracleDbType.Varchar2);
                pram_OUT_12.Size = 1000;
                pram_OUT_12.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_12);

                OracleParameter pram_OUT_13 = new OracleParameter("pAccess_Level_R", OracleDbType.Varchar2);
                pram_OUT_13.Size = 1000;
                pram_OUT_13.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_13);

                OracleParameter pram_OUT_14 = new OracleParameter("pUserRegion", OracleDbType.Varchar2);
                pram_OUT_14.Size = 1000;
                pram_OUT_14.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_14);

                OracleParameter pram_OUT_0 = new OracleParameter("PIS_AUTHENTICATED", OracleDbType.Varchar2);
                pram_OUT_0.Size = 1000;
                pram_OUT_0.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_0);

                OracleParameter pram_OUT_2 = new OracleParameter("pUSER_STATUS", OracleDbType.Varchar2);
                pram_OUT_2.Size = 1000;
                pram_OUT_2.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_2);

                OracleParameter pram_OUT_3 = new OracleParameter("pRetryCount", OracleDbType.Varchar2);
                pram_OUT_3.Size = 1000;
                pram_OUT_3.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_3);

                OracleParameter pram_OUT_4 = new OracleParameter("pPasswordChangeFlag", OracleDbType.Varchar2);
                pram_OUT_4.Size = 1000;
                pram_OUT_4.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_4);

                OracleParameter pram_OUT_5 = new OracleParameter("pPasswordChangeDecs", OracleDbType.Varchar2);
                pram_OUT_5.Size = 1000;
                pram_OUT_5.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_5);

                OracleParameter pram_OUT_6 = new OracleParameter("PSESSION_ID_OUT", OracleDbType.Varchar2);
                pram_OUT_6.Size = 1000;
                pram_OUT_6.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_6);

                OracleParameter pram_OUT_7 = new OracleParameter("PFLAG", OracleDbType.Varchar2);
                pram_OUT_7.Size = 1000;
                pram_OUT_7.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_7);

                OracleParameter pram_OUT_8 = new OracleParameter("PUSER_ID", OracleDbType.Varchar2);
                pram_OUT_8.Size = 1000;
                pram_OUT_8.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_8);

                OracleParameter PMSISDN = new OracleParameter("PMSISDN", OracleDbType.Varchar2);
                PMSISDN.Size = 1000;
                PMSISDN.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(PMSISDN);

                OracleParameter pram_OUT_9 = new OracleParameter("PCODE", OracleDbType.Varchar2);
                pram_OUT_9.Size = 1000;
                pram_OUT_9.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_9);

                OracleParameter pram_OUT_10 = new OracleParameter("pDESC", OracleDbType.Varchar2);
                pram_OUT_10.Size = 1000;
                pram_OUT_10.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_10);

                OracleParameter pram_OUT_11 = new OracleParameter("PMSG", OracleDbType.Varchar2);
                pram_OUT_11.Size = 1000;
                pram_OUT_11.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pram_OUT_11);

                ExecuteNonQuery(cmd);

                validateLOGIN.Is_Autheticated = cmd.Parameters["PIS_AUTHENTICATED"].Value.ToString();

                if (validateLOGIN.Is_Autheticated.Equals("TRUE"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public DataSet Call_Report_Procedure(CallReportProcedure callReportProcedure, ref string msgOut)
        {
            /*
                PACKAGE pkg_elmah$get_error_NEW
            PROCEDURE GET_ELMAH_ERROR_NEW
    (
                           PI_TRAN_ID      IN  VARCHAR2,
                           PI_APP_NAME     IN  VARCHAR2,
                           PI_STATUS_CODE  IN NUMBER,
                           PI_START_DATE   IN TIMESTAMP,
                           PI_END_DATE     IN TIMESTAMP,                            
                           PO_REF_CURSOR   OUT SYS_REFCURSOR,
                           PCODE           OUT VARCHAR2,
                           PDESC           OUT VARCHAR2,
                           PMSG            OUT VARCHAR2
    );

            */
            DataSet DS = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                //string Constr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringOra"].ToString();
                OracleConnection con = new OracleConnection(_connectionString);
                OracleCommand cmd = new OracleCommand("pkg_elmah$get_error_NEW.GET_ELMAH_ERROR_NEW", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                OracleParameter PI_TRAN_ID = new OracleParameter("PI_TRAN_ID", OracleDbType.Varchar2);
                PI_TRAN_ID.Direction = ParameterDirection.Input;

                if (callReportProcedure.TranID != "") PI_TRAN_ID.Value = callReportProcedure.TranID;
                else PI_TRAN_ID.Value = DBNull.Value;
                cmd.Parameters.Add(PI_TRAN_ID);

                OracleParameter PI_APP_NAME = new OracleParameter("PI_APP_NAME", OracleDbType.Varchar2);
                PI_APP_NAME.Direction = ParameterDirection.Input;

                if (callReportProcedure.ChannelType != "") PI_APP_NAME.Value = callReportProcedure.ChannelType;
                else PI_APP_NAME.Value = DBNull.Value;
                cmd.Parameters.Add(PI_APP_NAME);

                OracleParameter PI_STATUS_CODE = new OracleParameter("PI_STATUS_CODE", OracleDbType.Int16);
                PI_STATUS_CODE.Direction = ParameterDirection.Input;

                if (callReportProcedure.API_name != "") PI_STATUS_CODE.Value = callReportProcedure.API_name;
                else PI_STATUS_CODE.Value = DBNull.Value;
                cmd.Parameters.Add(PI_STATUS_CODE);

                OracleParameter PI_START_DATE = new OracleParameter("PI_START_DATE", OracleDbType.TimeStamp);
                PI_START_DATE.Direction = ParameterDirection.Input;

                if (callReportProcedure.StartDate.HasValue) PI_START_DATE.Value = callReportProcedure.StartDate;
                else PI_START_DATE.Value = DBNull.Value;
                cmd.Parameters.Add(PI_START_DATE);

                OracleParameter PI_END_DATE = new OracleParameter("PI_END_DATE", OracleDbType.TimeStamp);
                PI_END_DATE.Direction = ParameterDirection.Input;

                if (callReportProcedure.EndDate.HasValue) PI_END_DATE.Value = callReportProcedure.EndDate;
                else PI_END_DATE.Value = DBNull.Value;
                cmd.Parameters.Add(PI_END_DATE);


                cmd.Parameters.Add(new OracleParameter { ParameterName = "PO_REF_CURSOR", OracleDbType = OracleDbType.RefCursor, Direction = System.Data.ParameterDirection.Output });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "PCUR_STATUS_CODE", OracleDbType = OracleDbType.RefCursor, Direction = System.Data.ParameterDirection.Output });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = System.Data.ParameterDirection.Output, Size = 1000 });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = System.Data.ParameterDirection.Output, Size = 1000 });
                cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = System.Data.ParameterDirection.Output, Size = 1000 });

                // OracleDataAdapter oAdapdter = new OracleDataAdapter(cmd);
                // oAdapdter.Fill(dt);
                DS = FillDataSet(cmd);
                if (dt != null && dt.Rows.Count > 0)
                {

                }
                if (Convert.ToString(cmd.Parameters["PCODE"].Value) != "00")
                {
                    msgOut = Convert.ToString(cmd.Parameters["PCODE"].Value) + " DESC: " + Convert.ToString(cmd.Parameters["PDESC"].Value);
                }
            }
            catch (Exception ex)
            {
                //GlobalInfoEpmars.LogError(ex, "GetKisanDetails/KisanDatabaseHelper", "2035", Convert.ToString(tranId), Convert.ToString(tranId));
            }

            //return dt;
            return DS;
        }
        public int ExecuteNonQuery(OracleCommand cmd)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(_connectionString))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet FillDataSet(OracleCommand cmd)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(_connectionString))
                {
                    OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                    cmd.Connection = conn;
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetAPK_Data(string FileNo)
        {
            string desc = string.Empty;
            DataTable MappedDT = new DataTable();
            OracleConnection con = null;
            try
            {
               // string conn = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringOra"].ConnectionString;
                using (con = new OracleConnection(_connectionString))
                {

                    using (OracleCommand cmd = new OracleCommand("PKG_APK_PUSH.PRC_GET_TMP_APK", con))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PFILE_NO", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = Convert.ToInt32(FileNo) });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCUR_TMP_APK", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PCODE", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PDESC", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        cmd.Parameters.Add(new OracleParameter { ParameterName = "PMSG", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                        con.Open();
                        OracleDataAdapter oAdapdter = new OracleDataAdapter(cmd);
                        oAdapdter.Fill(MappedDT);
                        if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                        {
                            desc = cmd.Parameters["PDESC"].Value.ToString();
                        }
                        con.Close();

                    }
                }

            }
            catch (Exception ex)
            {
                desc = ex.Message.ToString();
            }
            finally
            {
                con.Close();


            }
            return MappedDT;
        }
        public void Insert_APK_Rec(string IMEI_No, string APK_VersionNo, string FileNo)
        {
         
            string sCode = "";
            string sDesc = "";
            string sMsg = "";


            OracleCommand cmd = new OracleCommand("PKG_APK_PUSH.PRC_INSERT_TMP_APK");
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter pmd_pstpaid_no = new OracleParameter("PIMEI", OracleDbType.Varchar2);
            pmd_pstpaid_no.Direction = ParameterDirection.Input;
            pmd_pstpaid_no.Value = IMEI_No;
            cmd.Parameters.Add(pmd_pstpaid_no);

            OracleParameter pretailr_cnic = new OracleParameter("PVERSION_NO", OracleDbType.Varchar2);
            pretailr_cnic.Direction = ParameterDirection.Input;
            pretailr_cnic.Value = APK_VersionNo;
            cmd.Parameters.Add(pretailr_cnic);

            OracleParameter pretaelr_id = new OracleParameter("PFILE_NO", OracleDbType.Varchar2);
            pretaelr_id.Direction = ParameterDirection.Input;
            pretaelr_id.Value = FileNo;
            cmd.Parameters.Add(pretaelr_id);

            OracleParameter pCODE = new OracleParameter("PCODE", OracleDbType.Varchar2);
            pCODE.Size = 1000;
            pCODE.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pCODE);

            OracleParameter pDesc = new OracleParameter("PDESC", OracleDbType.Varchar2);
            pDesc.Direction = ParameterDirection.Output;
            pDesc.Size = 1000;
            cmd.Parameters.Add(pDesc);

            OracleParameter pMSG = new OracleParameter("PMSG", OracleDbType.Varchar2);
            pMSG.Direction = ParameterDirection.Output;
            pMSG.Size = 1000;
            cmd.Parameters.Add(pMSG);


            try
            {
                ExecuteNonQuery(cmd);
                sMsg = cmd.Parameters["PMSG"].Value.ToString();
                sCode = cmd.Parameters["PCODE"].Value.ToString();
                sDesc = cmd.Parameters["PDESC"].Value.ToString();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                cmd.Dispose();
                cmd = null;
            }

        }
    }
}
