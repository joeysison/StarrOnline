using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarrOnline.Models
{
    public class Products : ModelBase
    {
        public int ITEMID;
        public int PRODUCTTYPE;
        public int PLANOPTION;
        public int PLANTYPE;
        public int INSURANCEDAYS;
        public double PREMIUMAMT;
        public int MINCOUNT;
        public int MAXCOUNT;
        public string CREATEDBY;
        public DateTime CREATEDDATE;
        public string EDITEDBY;
        public DateTime EDITEDDATE;
    }

}
