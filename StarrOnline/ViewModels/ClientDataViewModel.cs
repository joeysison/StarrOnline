using Caliburn.Micro;
using Dapper;
using StarrOnline.Common;
using StarrOnline.Interface;
using StarrOnline.Models;
using System.Data.SqlClient;
using System.Linq;

namespace StarrOnline.ViewModels
{
    public class ClientDataViewModel : BaseViewModel
    {
        private Transactions _Transaction = new Transactions();
        BindableCollection<Transactions> _Tran;
        private SqlConnection connection;
        private DataConnector _data = new DataConnector();

        public ClientDataViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService settingsService)
            : base(navigationService, windowManager, settingsService)
        {
            connection = new SqlConnection(GlobalConfig.ConnectionStringData);

            getTransaction();
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
            //_Transaction.MASTERPOLICY = _Tran[0].MASTERPOLICY.ToUpper();
            //_Transaction.ISSUEDATE = _Tran[0].ISSUEDATE;
            //_Transaction.COVERAGE = _Tran[0].COVERAGE.ToUpper();
            //_Transaction.TRAVELDATEFROM = _Tran[0].TRAVELDATEFROM;
            //_Transaction.TRAVELDATETO = _Tran[0].TRAVELDATETO;
            //_Transaction.FIRSTNAME = _Tran[0].FIRSTNAME.ToUpper();
            //_Transaction.LASTNAME = _Tran[0].LASTNAME.ToUpper();
            //_Transaction.NOOFDAYS = _Tran[0].NOOFDAYS;
            //_Transaction.RELOC = _Tran[0].RELOC.ToUpper();
            //_Transaction.BIRTHDATE = _Tran[0].BIRTHDATE;
            //_Transaction.PRODUCTTYPEDESCR = _Tran[0].PRODUCTTYPEDESCR;
            //_Transaction.PLANOPTIONDESCR = _Tran[0].PLANOPTIONDESCR.ToUpper();
            //_Transaction.PLANTYPEDESCR = _Tran[0].PLANTYPEDESCR.ToUpper();
            //_Transaction.NETAMOUNT = _Tran[0].NETAMOUNT;
            //_Transaction.STATUS = _Tran[0].STATUS.ToUpper();
            //_Transaction.REMARKS = _Tran[0].REMARKS.ToString().Trim();
        }


        public void getTransaction()
        {
            string strSQL = DefaultValues.editTransaction + "'" + GlobalConfig.COCNumber + "'";
            _Tran = new BindableCollection<Transactions>(_data.GetTransactions_All(strSQL).ToList());
            populateData();
        }

    }
}
