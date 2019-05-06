using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using StarrOnline.Models;
using Caliburn.Micro;

namespace StarrOnline.Interface
{
    public interface IDataConnection
    {
        void createTransactions(Transactions model);
        void createPlanType();


        //List<Transactions> GetTransactions_All(string strSQL);

    }
}
