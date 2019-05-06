using Caliburn.Micro;
using CrystalDecisions.CrystalReports.Engine;
using StarrOnline.Interface;
using StarrOnline.Models;

namespace StarrOnline.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {

        #region method
        public ReportViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService settingsService)
            : base(navigationService, windowManager, settingsService)
        {
            //prepareInitialData();
            //PrintPreview();

            //ReportSource = new ReportDocument();
           
        }

        private int _TransactionsID;
        public int TransactionsID
        {
            get { return _TransactionsID; }
            set
            {
                _TransactionsID = value;
            }
        }

        private Transaction _Transaction;
        public Transaction Transaction
        {
            get { return _Transaction; }
            set
            {
                _Transaction = value;
                NotifyOfPropertyChange(() => Transaction);
            }
        }


        public void PrintPreview()
        {
            //MessageBox.Show("tesT");
            ReportSource = new ReportDocument();

        }

        public CrystalDecisions.CrystalReports.Engine.ReportDocument ReportSource
        {
            //this.crystalReportsViewer.ViewerCore.ReportSource = aReport;

            get; set;

            
        }
        #endregion
    }
}
