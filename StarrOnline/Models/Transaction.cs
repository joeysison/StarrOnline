using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarrOnline.Models
{
    public class Transaction : ModelBase
    {
        public int ITEMID { get; set; }
        public string MASTERPOLICY { get; set; }
        public string COCNO { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public DateTime BIRTHDATE { get; set; }
        public int PRODUCTTYPE { get; set; }
        public int PLANOPTION { get; set; }
        public int PLANTYPE { get; set; }
        public string RELOC { get; set; }
        public string COVERAGE { get; set; }
        public DateTime ISSUEDATE { get; set; }
        public DateTime TRAVELDATEFROM { get; set; }
        public DateTime TRAVELDATETO { get; set; }
        public int NOOFDAYS { get; set; }
        public string PREMIUM { get; set; }
        public int COMPANY { get; set; }
        public string REMARKS { get; set; }
        public int CREATEDBY { get; set; }
        public DateTime CREATEDDATE { get; set; }
        public int MODIFIEDBY { get; set; }
        public DateTime MODIFIEDDATE { get; set; }
        public string STATUS { get; set; }
        //public ICollection<Companion> Companions { get; set; }
    }
}
