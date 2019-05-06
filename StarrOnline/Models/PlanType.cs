using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarrOnline.Models
{
    public class PlanType : ModelBase
    {
        public int itemID { get; set; }
        public int productID { get; set; }
        public string description { get; set; }

    }
}
