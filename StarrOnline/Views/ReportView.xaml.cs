using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Windows;
using CrystalDecisions.Shared;
using StarrOnline.Models;

namespace StarrOnline.Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : Window
    {
        public ReportView()
        {
            InitializeComponent();

            
            ReportDocument rptDoc = new ReportDocument();


            string appPath = "";
            if (GlobalConfig.iSource == 1)
            {
                if (GlobalConfig.ProductType.ToUpper().Equals("INTERNATIONAL"))
                {
                    if (GlobalConfig.PlanType.ToUpper().Equals("ESSENTIAL"))
                    {
                        //for Starr deployement
                        appPath = GlobalConfig.ReportFiles + "\\IntlEssential.rpt";

                        //for client deployment
                        //appPath = @"M:\IntlEssential.rpt";
                    }
                    else if (GlobalConfig.PlanType.ToUpper().Equals("EXTRA"))
                    {
                        //appPath = @"M:\IntlExtra.rpt";
                        appPath = GlobalConfig.ReportFiles + "\\IntlExtra.rpt";
                    }
                }
                else if (GlobalConfig.ProductType.ToUpper().Equals("DOMESTIC"))
                {
                    if (GlobalConfig.PlanType.ToUpper().Equals("ECONOMY"))
                    {
                        //appPath = @"M:\DomEconomy.rpt";
                        appPath = GlobalConfig.ReportFiles + "\\DomEconomy.rpt";
                    }
                    else if (GlobalConfig.PlanType.ToUpper().Equals("ELITE"))
                    {
                        //appPath = @"M:\DomElite.rpt";
                        appPath = GlobalConfig.ReportFiles + "\\DomElite.rpt";
                    }
                }
            }else
            {
                if (GlobalConfig.iProductType == 1)
                {
                    if (GlobalConfig.iPlanType == 1)
                    {
                        //appPath = @"M:\IntlEssential.rpt";
                        appPath = Environment.CurrentDirectory + "\\Report\\IntlEssential.rpt";
                    }
                    else if (GlobalConfig.iPlanType == 2)
                    {
                        //appPath = @"M:\IntlExtra.rpt";
                        appPath = Environment.CurrentDirectory + "\\Report\\IntlExtra.rpt";
                    }
                }
                else if (GlobalConfig.iProductType == 2)
                {
                    if (GlobalConfig.iPlanType == 3)
                    {
                        //appPath = @"M:\DomEconomy.rpt";
                        appPath = Environment.CurrentDirectory + "\\Report\\DomEconomy.rpt";
                    }
                    else if (GlobalConfig.iPlanType == 4)
                    {
                        //appPath = @"M:\DomElite.rpt";
                        appPath = Environment.CurrentDirectory + "\\Report\\DOMELITE.rpt";
                    }
                }
            }


            //appPath;
            rptDoc.Load(String.Format(appPath));

            ConnectionInfo ciReportConnection = new ConnectionInfo();

            ciReportConnection.ServerName = "starrdb.casrzvv9fyy4.ap-southeast-1.rds.amazonaws.com,1433";
            ciReportConnection.DatabaseName = "starrDB";
            ciReportConnection.UserID = "aUserDB";
            ciReportConnection.Password = "password123";

            SetDBLogonForReport(ciReportConnection, rptDoc);

            rptDoc.SetParameterValue("COCNumber", GlobalConfig.COCNumber);
            CrystalReportsViewer1.ViewerCore.ReportSource = rptDoc;

        }

        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }


    }
}
