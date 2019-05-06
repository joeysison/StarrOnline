using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using StarrOnline.Interface;
using StarrOnline.Common;
using StarrOnline.Utilities;
using System.Data.SqlClient;


namespace StarrOnline.Models
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }

        public static string ConnectionStringData;
        public static string SelectedGDS;
        public static string AgentName;
        public static string AgentIDAmadeus;
        public static string AgentIDSabre;
        public static string FileExport;
        public static string SOAReport;
        public static string GRPReport;
        public static string AppName;
        public static string ApplyFilter;
        public static string ReportFiles;

        public static int userID;
        public static int companyID;
        public static string policyNoINT = "";
        public static string policyNoDOM = "";
        public static string userName = "";
        public static string userGroup = "";
        public static string companyCode = "";
        public static string CompanyName = "";
        public static string COCNumber = "";
        public static string Status = "";
        public static String ProductType = "";
        public static String PlanOption = "";
        public static String PlanType = "";
        public static int iProductType = 0;
        public static int iPlanOption = 0;
        public static int iPlanType = 0;
        public static int iSource = 0;

        public static void InitializeConnections(DatabaseType db)
        {
            if (db == DatabaseType.Sql)
            {
                // TODO - Set up the SQL Connector properly
                DataConnector sql = new DataConnector();
                Connection = sql;
            }
            //else if (db == DatabaseType.TextFile)
            //{
            //    // TODO - Create the Text Connection
            //    DataConnector text = new DataConnector();
            //    Connection = text;
            //}
        }

        //public static string CnnString(string name)
        //{
        //    return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        //}

        public static string AppKeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

    }
}
