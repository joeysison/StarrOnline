using Caliburn.Micro;
using Dapper;
using StarrOnline.Common;
using StarrOnline.Interface;
using StarrOnline.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace StarrOnline.ViewModels
{
    public class ClientEditViewModel : BaseViewModel
    {
        private Transactions _Transaction = new Transactions();
        BindableCollection<Transactions> _Tran;
        //private SqlConnection connection;
        private String _canCancel;
        private DataConnector _data = new DataConnector();

        public ClientEditViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService settingsService)
            : base(navigationService, windowManager, settingsService)
        {
            //connection = new SqlConnection(GlobalConfig.CnnString(DefaultValues.db));
            getTransaction();

            if (_Transaction.STATUS.Equals("ACTIVE"))
            {
                _canCancel = "Visible";
            }
            else
            {
                _canCancel = "Hidden";
            }

            NotifyOfPropertyChange(() => cancelAction);
            NotifyOfPropertyChange(() => Transaction);
            
        }

        public String cancelAction
        {
            get
            {
                return _canCancel;
            }
            set
            {
                cancelAction = value;
                NotifyOfPropertyChange(() => cancelAction);
            }
        }

        public Transactions Transaction
        {
            get
            {
                return _Transaction;
            }
            set
            {
                if (_Transaction != value)
                {
                    _Transaction = value;
                    NotifyOfPropertyChange(() => Transaction);
                }
            }
        }

        private void populateData()
        {

            _Transaction.COCNO = _Tran[0].COCNO;
            _Transaction.MASTERPOLICY = _Tran[0].MASTERPOLICY.ToUpper();
            _Transaction.ISSUEDATE = _Tran[0].ISSUEDATE;
            _Transaction.COVERAGE = _Tran[0].COVERAGE.ToUpper();
            _Transaction.TRAVELDATEFROM = _Tran[0].TRAVELDATEFROM;
            _Transaction.TRAVELDATETO = _Tran[0].TRAVELDATETO;
            _Transaction.FULLNAME = _Tran[0].FULLNAME.ToUpper();
            _Transaction.FIRSTNAME = _Tran[0].FIRSTNAME.ToUpper();
            _Transaction.LASTNAME = _Tran[0].LASTNAME.ToUpper();
            _Transaction.NOOFDAYS = _Tran[0].NOOFDAYS;
            _Transaction.RELOC = _Tran[0].RELOC.ToUpper();
            _Transaction.BIRTHDATE = _Tran[0].BIRTHDATE;
            _Transaction.PRODUCTTYPEDESCR = _Tran[0].PRODUCTTYPEDESCR;
            _Transaction.PLANOPTIONDESCR = _Tran[0].PLANOPTIONDESCR.ToUpper();
            _Transaction.PLANTYPEDESCR = _Tran[0].PLANTYPEDESCR.ToUpper();
            _Transaction.GROSSAMOUNT = _Tran[0].GROSSAMOUNT;
            _Transaction.NETTAMOUNT = _Tran[0].NETTAMOUNT;
            _Transaction.STATUS = _Tran[0].STATUS.ToUpper();
            _Transaction.REMARKS = _Tran[0].REMARKS.ToString();
        }

        public void getTransaction()
        {

            string strSQL = DefaultValues.editTransaction + "'" + GlobalConfig.COCNumber + "'";

            _Tran = new BindableCollection<Transactions>(_data.GetTransactions_All(strSQL).ToList());

            populateData();

        }

        public void Preview()
        {
            if (_Transaction != null)
            {
                GlobalConfig.COCNumber = _Tran[0].COCNO;
                GlobalConfig.ProductType = _Tran[0].PLANTYPEDESCR;
                GlobalConfig.PlanOption = _Tran[0].PLANOPTIONDESCR;
                GlobalConfig.PlanType = _Tran[0].PLANTYPEDESCR;
                GlobalConfig.iSource = 1;

                _navigationService.GetWindow<ReportViewModel>().ShowWindowModal();

                GlobalConfig.COCNumber = "";
                GlobalConfig.ProductType = "";
                GlobalConfig.PlanOption = "";
                GlobalConfig.PlanType = "";
            }
            else
            {
                MessageBox.Show("Please select Policy.", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CancelTransaction()
        {
            //var parameterDate = DateTime.ParseExact(_Transaction.TRAVELDATEFROM, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var todaysDate = DateTime.Today;

            if (_Transaction.TRAVELDATEFROM < todaysDate)
            {
                MessageBox.Show("Policy Cannot be CANCELED,\n\nTRAVEL START DATE is PAST DATED.\n\nPLEASE CONTACT STARR INSURANCE OFFICE.",":: WARNING ::",MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                MessageBoxResult rslt = MessageBox.Show("Are you sure you want to CANCEL Transaction", ":: WARNING ::", MessageBoxButton.YesNo, MessageBoxImage.Stop);

                if (rslt == MessageBoxResult.Yes)
                {
                    _data.cancelData(_Transaction.COCNO, GlobalConfig.userID);
                    MessageBox.Show("POLICY HAS BEEN CANCELLED.", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Information);
                    TryClose(true);
                }
            }

        }

    }
}