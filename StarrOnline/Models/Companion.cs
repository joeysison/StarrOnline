using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarrOnline.Models
{
    public class Companion : ModelBase
    {
        public int itemNo;
        public int transactionNo;
        public string fullName;
        public string relationship;
        public DateTime birthDate;
        public int createdBy;
        public DateTime createdDate;
    }
}
