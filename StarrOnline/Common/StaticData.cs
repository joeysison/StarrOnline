using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarrOnline.Common
{
    public static class StaticData
    {
        public static int userID;
        public static int companyID;
        public static string policyNoINT ="";
        public static string policyNoDOM ="";
        public static string userName ="";
        public static string userGroup ="";
        public static string companyCode ="";
        public static string CompanyName ="";
        public static string COCNumber ="";
        public static string Status ="";
        public static String ProductType ="";
        public static String PlanOption ="";
        public static String PlanType ="";
        public static int iProductType = 0;
        public static int iPlanOption = 0;
        public static int iPlanType = 0;
        public static int iSource = 0;
        public static string AppName = "";
        public static string ApplyFilter = "";
        public static string reportData = "";

        internal static void InitializeConnections(DatabaseType sql)
        {
            throw new NotImplementedException();
        }

        public static string reportFiles = "";




    }
}
