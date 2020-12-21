using LinqToExcel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.Entities.ReadExcelMNP
{
    public class ReadExcel_MNP
    {
        [ExcelColumn("BIZ_PARTNER_ID")]
        public string BIZ_PARTNER_ID { get; set; }

        [ExcelColumn("BIZ_PARTNER_NAME")]
        public string BIZ_PARTNER_NAME { get; set; }

        [ExcelColumn("BIZ_PARTNER_DESC")]
        public string BIZ_PARTNER_DESC { get; set; }

        [ExcelColumn("BIZ_PART_ERP_ID")]
        public string BIZ_PART_ERP_ID { get; set; }
        [ExcelColumn("ERP_CODE")]
        public string ERP_CODE { get; set; }

        [ExcelColumn("END_DATE")]
        public string END_DATE { get; set; }

        [ExcelColumn("REGION_ID")]
        public string REGION_ID { get; set; }

        [ExcelColumn("REGION_NAME")]
        public string REGION_NAME { get; set; }
        [ExcelColumn("CITY_ID")]
        public string CITY_ID { get; set; }

        [ExcelColumn("CITY_NAME")]
        public string CITY_NAME { get; set; }

        [ExcelColumn("ZONE_ID")]
        public string ZONE_ID { get; set; }

        [ExcelColumn("ZONE_NAME")]
        public string ZONE_NAME { get; set; }
        [ExcelColumn("AREA_ID")]//maps the "Name" property to the "Company Title" column
        public string AREA_ID { get; set; }

        [ExcelColumn("AREA_NAME")]
        public string AREA_NAME { get; set; }

        [ExcelColumn("BIZ_PARTNER_TYPE_ID")]
        public string BIZ_PARTNER_TYPE_ID { get; set; }

        [ExcelColumn("BIZ_PARTNER_CODE")]
        public string BIZ_PARTNER_CODE { get; set; }
        [ExcelColumn("PARENT_BP_ID")]//maps the "Name" property to the "Company Title" column
        public string PARENT_BP_ID { get; set; }

        [ExcelColumn("PARENT_BP_CODE")]
        public string PARENT_BP_CODE { get; set; }

        [ExcelColumn("AUTHORIZED_RETAILER")]
        public string AUTHORIZED_RETAILER { get; set; }

        [ExcelColumn("E_LOAD_MSISDN")]
        public string E_LOAD_MSISDN { get; set; }
        [ExcelColumn("BIZ_PARTNER_POSTAL_ADDRESS")]//maps the "Name" property to the "Company Title" column
        public string BIZ_PARTNER_POSTAL_ADDRESS { get; set; }

        [ExcelColumn("CONTACT_PERSON")]
        public string CONTACT_PERSON { get; set; }

        [ExcelColumn("CONTACT_NUMBER")]
        public string CONTACT_NUMBER { get; set; }

        [ExcelColumn("NTN_NO")]
        public string NTN_NO { get; set; }
        [ExcelColumn("NIC")]//maps the "Name" property to the "Company Title" column
        public string NIC { get; set; }

        [ExcelColumn("PARENT_BIZ_PARTNER_TYPE_ID")]
        public string PARENT_BIZ_PARTNER_TYPE_ID { get; set; }


    }
}
