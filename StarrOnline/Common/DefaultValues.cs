using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarrOnline.Common
{
    class DefaultValues
    {

        public static string db = "starrDB";
        public static string getUserID = "spGetUserID";
        public static string getPremiumAmt = "spGetPremiumAmt";
        public static string saveTransaction = "spSaveTransactions";
        public static string cancelTransaction = "spCancelTransactions";
        public static string savePassword = "spSavePassword";
        public static string initializeTransactionWithCancelForNonAdmin = "SELECT * FROM VW_TRANSACTION_DATA where company = '";
        public static string initializeTransactionWithCancelForAdmin = "SELECT * FROM VW_TRANSACTION_DATA ";


        public static string initializeTransactionWithCancel = "SELECT * FROM VW_TRANSACTION_DATA where status ='ACTIVE' and company = '";
        public static string initializeTransaction = "SELECT * FROM VW_TRANSACTION_DATA where company like '";
        public static string searchForTransaction = "select * from VW_TRANSACTION_DATA  where status ='ACTIVE' and company like '";
        public static string searchForTransactionWithCancel = "select * from VW_TRANSACTION_DATA company = '";
        public static string editTransaction = "select * from VW_TRANSACTION_DATA where COCNO = ";
        /*like '' or reloc = '' and company =*/
    }
}
