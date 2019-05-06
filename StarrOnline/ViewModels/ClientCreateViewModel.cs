using Caliburn.Micro;
using StarrOnline.Common;
using StarrOnline.Interface;
using StarrOnline.Models;
using StarrOnline.Utilities;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace StarrOnline.ViewModels
{
    public class ClientCreateViewModel : BaseViewModel
    {
        private DataConnector _data = new DataConnector();
        private Transaction _Transaction;
        private BindableCollection<ProductType> _ProductType;
        private ProductType _selectedProductType;
        private BindableCollection<PlanOption> _PlanOption;
        private PlanOption _selectedPlanOption;
        private BindableCollection<PlanType> _PlanType;
        private PlanType _selectedPlanType;
        private BindableCollection<Companion> _Companions;
        private clsAmadeus _clsAmadeus;

        #region Property
        private string _showReminderEssential = "Hidden";
        public string showReminderEssential
        {
            get { return _showReminderEssential; }
            set
            {
                _showReminderEssential = value;
                NotifyOfPropertyChange(() => _showReminderEssential);
            }
        }


        private string _showReminderExtra = "Hidden";
        public string showReminderExtra
        {
            get{ return _showReminderExtra; }
            set{
                _showReminderExtra = value;
                NotifyOfPropertyChange(() => _showReminderExtra);
            }
        }


        private string _isShowErrorOnDays = "Hidden";
        public string isShowErrorOnDays
        {
            get { return _isShowErrorOnDays; }
            set
            {
                _isShowErrorOnDays = value;
                NotifyOfPropertyChange(() => isShowErrorOnDays);
            }
        }

        private string _age;
        public string age
        {
            get { return _age; }
            set
            {
                _age = value;
                NotifyOfPropertyChange(() => age);
            }

        }

        private string _isShowErrorOnCompute = "Hidden";
        public string isShowErrorOnCompute
        {
            get { return _isShowErrorOnCompute; }
            set
            {
                _isShowErrorOnCompute = value;
                NotifyOfPropertyChange(() => isShowErrorOnCompute);
            }
        }

        private string _isBtnEnabled;
        public string isBtnEnabled
        {
            get { return _isBtnEnabled; }
            set
            {
                _isBtnEnabled = value;
                NotifyOfPropertyChange(() => isBtnEnabled);
            }
        }

        public BindableCollection<Companion> Companions
        {
            get { return _Companions; }
            set
            {
                if (_Companions != value)
                {
                    _Companions = value;
                    NotifyOfPropertyChange(() => Companions);
                }
            }
        }

        public BindableCollection<ProductType> ProductType
        {
            get
            {
                return _ProductType;
            }
            set
            {
                if (_ProductType != value)
                {
                    _ProductType = value;
                    NotifyOfPropertyChange(() => ProductType);
                    NotifyOfPropertyChange(() => Transaction);
                }
            }
        }

        public BindableCollection<PlanOption> PlanOption
        {
            get
            {
                return _PlanOption;
            }
            set
            {
                if (_PlanOption != value)
                {
                    _PlanOption = value;
                    NotifyOfPropertyChange(() => PlanOption);
                    NotifyOfPropertyChange(() => Transaction);
                }
            }
        }

        public BindableCollection<PlanType> PlanType
        {
            get
            {
                return _PlanType;
            }
            set
            {
                if (_PlanType != value)
                {
                    _PlanType = value;
                    NotifyOfPropertyChange(() => PlanType);
                    NotifyOfPropertyChange(() => Transaction);
                }
            }
        }

        public ProductType SelectedProductType
        {
            get
            {
                return _selectedProductType;
            }
            set
            {
                _selectedProductType = value;
                Transaction.PRODUCTTYPE = _selectedProductType.itemID;
                if (_selectedProductType.itemID > 0)
                {
                    if (_selectedProductType.itemID == 1)
                    {
                        Transaction.MASTERPOLICY = GlobalConfig.policyNoINT;
                    }else
                    {
                        Transaction.MASTERPOLICY = GlobalConfig.policyNoDOM;
                        _showReminderEssential = "Hidden";
                        _showReminderExtra = "Hidden";

                    }

                    PlanType = new BindableCollection<PlanType>(_data.GetPlanType("Select itemid, description from tbl_plantype where ProductID =" + _selectedProductType.itemID));
                }

                Transaction.PREMIUM = "0.00";
                NotifyOfPropertyChange(() => showReminderExtra);
                NotifyOfPropertyChange(() => showReminderEssential);
                NotifyOfPropertyChange(() => SelectedProductType);
                NotifyOfPropertyChange(() => Transaction);

            }
        }

        public PlanOption SelectedPlanOption
        {
            get
            {
                return _selectedPlanOption;
            }
            set
            {
                _selectedPlanOption = value;
                Transaction.PLANOPTION = _selectedPlanOption.itemID;
                Transaction.PREMIUM = "0.00";
                NotifyOfPropertyChange(() => SelectedPlanOption);
                NotifyOfPropertyChange(() => Transaction);

            }
        }

        public PlanType SelectedPlanType
        {
            get
            {
                return _selectedPlanType;
            }
            set
            {
                _selectedPlanType = value;
                Transaction.PLANTYPE = _selectedPlanType.itemID;
                Transaction.PREMIUM = "0.00";

                if (_selectedPlanType.itemID.Equals(1) && _selectedProductType.itemID.Equals(1))
                {
                    _showReminderEssential = "Visible";
                    _showReminderExtra = "Hidden";
                }

                if (_selectedPlanType.itemID.Equals(2) && _selectedProductType.itemID.Equals(1))
                {
                    _showReminderExtra = "Visible";
                    _showReminderEssential = "Hidden";
                }


                NotifyOfPropertyChange(() => showReminderEssential);
                NotifyOfPropertyChange(() => showReminderExtra);


                NotifyOfPropertyChange(() => SelectedPlanType);
                NotifyOfPropertyChange(() => Transaction);
            }
        }

        public Transaction Transaction
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
        #endregion

        #region method
        public ClientCreateViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService settingsService)
            : base(navigationService, windowManager, settingsService)
        {
            prepareInitialData();
        }

        private void prepareInitialData()
        {
            Transaction = new Transaction();
            ProductType = new BindableCollection<ProductType>(_data.GetProductType("Select itemid, description from tbl_producttype"));
            PlanOption = new BindableCollection<PlanOption>(_data.GetPlanOption("Select itemid, description from tbl_planoption"));

            Transaction.COCNO = "";
            Transaction.COMPANY = GlobalConfig.companyID;
            Transaction.CREATEDBY = GlobalConfig.userID;
            Transaction.ISSUEDATE = DateTime.Today;
            Transaction.BIRTHDATE = DateTime.Today;
            Transaction.TRAVELDATEFROM = DateTime.Today;
            Transaction.TRAVELDATETO = DateTime.Today;
            Transaction.PREMIUM = "0.00";
            _isBtnEnabled = "Visible";
            NotifyOfPropertyChange(() => Transaction);
            NotifyOfPropertyChange(() => isBtnEnabled);
        }

        TimeSpan getDateDifference(DateTime date1, DateTime date2)
        {
            TimeSpan ts = date1 - date2;
            return ts;
        }

        public void calculateNoOfDays()
        {
            int _daysDiff;

            if (Transaction.TRAVELDATETO < Transaction.TRAVELDATEFROM)
            {
                isShowErrorOnDays = "Visible";
                NotifyOfPropertyChange(() => isShowErrorOnDays);
            }
            else
            {
                _daysDiff = Convert.ToInt32(getDateDifference(Transaction.TRAVELDATETO, Transaction.TRAVELDATEFROM).TotalDays);
                Transaction.NOOFDAYS = _daysDiff + 1;
                NotifyOfPropertyChange(() => Transaction);
                isShowErrorOnDays = "Hidden";
                NotifyOfPropertyChange(() => isShowErrorOnDays);
            }
        }

        public void ComputePremium()
        {
            string _premiumBase = "0";
            string _premiumMorethan30 = "0";
            string _premiumTotal;
            double _Totalpremium;
            int _excessDays = 0;

            if ((Transaction.PRODUCTTYPE > 0) && (Transaction.PLANOPTION > 0) && (Transaction.PLANTYPE >0) && (Transaction.NOOFDAYS > 0))
            {
                if (Transaction.NOOFDAYS > 30)
                {
                    _premiumBase = _data.GetPremiumAmt(DefaultValues.getPremiumAmt, Transaction.PRODUCTTYPE, Transaction.PLANOPTION, Transaction.PLANTYPE, 30);
                    _excessDays = Transaction.NOOFDAYS - 30;
                    _premiumMorethan30 = _data.GetPremiumAmt(DefaultValues.getPremiumAmt, Transaction.PRODUCTTYPE, Transaction.PLANOPTION, Transaction.PLANTYPE, 31);
                }
                else
                {
                    _premiumBase = _data.GetPremiumAmt(DefaultValues.getPremiumAmt, Transaction.PRODUCTTYPE, Transaction.PLANOPTION, Transaction.PLANTYPE, Transaction.NOOFDAYS);
                }

                _Totalpremium = Convert.ToDouble(_premiumBase) + (Convert.ToDouble(_premiumMorethan30) * Convert.ToInt32(_excessDays));
                _premiumTotal = _Totalpremium.ToString("#,##0.00");

                Transaction.PREMIUM = _premiumTotal;
                NotifyOfPropertyChange(() => Transaction);
                isShowErrorOnCompute = "Hidden";
                NotifyOfPropertyChange(() => isShowErrorOnCompute);
            }
            else
            {
                isShowErrorOnCompute = "Visible";
                NotifyOfPropertyChange(() => isShowErrorOnCompute);
            }
        }

        private void RefreshTransactions()
        {
            //throw new NotImplementedException();
        }

        private bool allfilledup()
        {
            if (String.IsNullOrEmpty(Transaction.FIRSTNAME) || String.IsNullOrWhiteSpace(Transaction.FIRSTNAME))
            {
                MessageBox.Show("Check your TRAVELER NAME...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(Transaction.LASTNAME) || String.IsNullOrWhiteSpace(Transaction.LASTNAME))
            {
                MessageBox.Show("Check your TRAVELER NAME...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(Transaction.COVERAGE) || String.IsNullOrWhiteSpace(Transaction.COVERAGE))
            {
                MessageBox.Show("Check your COVERAGE...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(Transaction.RELOC) || String.IsNullOrWhiteSpace(Transaction.RELOC))
            {
                MessageBox.Show("Check your RELOC...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Transaction.PRODUCTTYPE == 0)
            { MessageBox.Show("Check your PRODUCT TYPE...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if(Transaction.PLANOPTION == 0)
            {
                MessageBox.Show("Check your PLAN OPTION...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Transaction.PLANTYPE == 0)
            {
                MessageBox.Show("Check your PLAN TYPE...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Transaction.NOOFDAYS == 0)
            {
                MessageBox.Show("Check your TRAVEL DATES...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            
            if (Regex.IsMatch(Transaction.PREMIUM, @"^\d+$"))
            {
                MessageBox.Show("Click compute button to get premium...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

                return true;

        }

        private bool checkAge()
        {
            int compage = 0;
            compage = Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(Transaction.BIRTHDATE.Year);

            if (compage > 1 && compage < 76)
                return true;
            else
                MessageBox.Show("Invalid Age", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        public void Save()
        {
            if (allfilledup() && checkAge())
            {
                Transaction.COCNO = _data.saveTransaction(Transaction);
                if (!_data._IsError)
                {
                    MessageBox.Show("SUSCCESSFULLY SAVED...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Information);
                    _isBtnEnabled = "hidden";
                    Transaction.STATUS = "ACTIVE";
                    NotifyOfPropertyChange(() => Transaction);
                    NotifyOfPropertyChange(() => isBtnEnabled);
                    return;
                    //TryClose(true);
                }else
                {
                    MessageBox.Show(_data._errorMessage, GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                    TryClose(true);
                }
            }

            //TryClose(true);
        }

        public void GDS()
        {
            _clsAmadeus = new clsAmadeus();

            try
            {
                if (!_clsAmadeus.connectASP())
                {
                    MessageBox.Show(_clsAmadeus._errorStr);
                }
                else
                {
                    Transaction.RELOC = _clsAmadeus.getPNR();
                    Transaction.LASTNAME = _clsAmadeus.getPax().Split('/') [0];
                    Transaction.FIRSTNAME = _clsAmadeus.getPax().Split('/')[1];

                    string travelFrom = _clsAmadeus.getDepartureTravelDate() + "-" + DateTime.Now.Year.ToString();
                    string travelTo = _clsAmadeus.getReturnTravelDate() + "-" + DateTime.Now.Year.ToString();

                    Transaction.TRAVELDATEFROM = Convert.ToDateTime(travelFrom);
                    Transaction.TRAVELDATETO = Convert.ToDateTime(travelTo);
                    Transaction.COVERAGE = _clsAmadeus.getItinerary();

                    NotifyOfPropertyChange(() => Transaction);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, GlobalConfig.AppName, MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
        }

        public void AddCompanion()
        {
            if (Transaction.PLANOPTION > 0)
            {
                if (Transaction.PLANOPTION == 1)
                {
                    _navigationService.GetWindow<CompanionIndViewModel>()
                        //.WithParam(vm => vm.Companion, new Companion())
                        //.DoIfSuccess(() => RefreshSelectedClient())
                        .ShowWindowModal();
                }
                else
                {
                    _navigationService.GetWindow<CompanionFamViewModel>()
                        //.WithParam(vm => vm.Companion, new Companion())
                        //.DoIfSuccess(() => RefreshSelectedClient())
                        .ShowWindowModal();
                }
            }else
            {
                MessageBox.Show ("Please Select Plan Option...", GlobalConfig.AppName,MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        public void DeleteCompanion()
        {

        }

        public void Preview()
        {
            GlobalConfig.COCNumber = _Transaction.COCNO;
            GlobalConfig.iProductType = _Transaction.PRODUCTTYPE;
            GlobalConfig.iPlanOption = _Transaction.PLANOPTION;
            GlobalConfig.iPlanType = _Transaction.PLANTYPE;
            GlobalConfig.iSource = 2;

            _navigationService.GetWindow<ReportViewModel>().ShowWindowModal();
        }
        #endregion method


    }
}
