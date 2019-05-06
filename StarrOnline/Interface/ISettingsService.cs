using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarrOnline.Interface
{

    public interface ISettingsService
    {
            string UserName { get; set; }

            int UserID { get; set; }

            int CompanyID { get; set; }

            string CompanyName { get; set; }
    }
}
