using System;
using System.ComponentModel;

namespace StarrOnline.Models
{
    public class Transactions : ModelBase
    {
        public int ITEMID { get; set; }

        public string MASTERPOLICY { get; set; }

        public string COCNO { get; set; }

        public string FIRSTNAME { get; set; }

        public string LASTNAME { get; set; }

        public string FULLNAME { get; set; }

        public DateTime BIRTHDATE { get; set; }

        public int PRODUCTTYPE { get; set; }

        public string PRODUCTTYPEDESCR { get; set; }

        public int PLANOPTION { get; set; }
        
        public string PLANOPTIONDESCR { get; set; }

        public int PLANTYPE { get; set; }

        public string PLANTYPEDESCR { get; set; }

        public string RELOC { get; set; }

        public string COVERAGE { get; set; }

        public DateTime ISSUEDATE { get; set; }

        public DateTime TRAVELDATEFROM { get; set; }

        public DateTime TRAVELDATETO { get; set; }

        public int NOOFDAYS { get; set; }

        public double GROSSAMOUNT { get; set; }

        public double NETTAMOUNT { get; set; }

        public double BASEPREMIUM { get; set; }

        public double DOCSTAMP { get; set; }

        public double LBT { get; set; }

        public double PTAX { get; set; }

        public double COMMISSION { get; set; }

        public double SERVICEFEE { get; set; }

        public double ADDFEE { get; set; }

        public double VATONFEE { get; set; }

        public double WITHHOLDINGTAX { get; set; }

        public double ADDWITHHOLDINGTAX { get; set; }

        public double NETSERVICEFEE { get; set; }

        public double STARRPAYABLE { get; set; }

        public string CREATED { get; set; }

        public string TRAVELAGENCY { get; set; }

        public string COMPANY_NAME { get; set; }

        public int COMPANY { get; set; }

        public string REMARKS { get; set; }

        public string STATUS { get; set; }


    }
}
