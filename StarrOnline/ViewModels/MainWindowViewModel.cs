using StarrOnline.Models;
using Caliburn.Micro;
using System.Data.SqlClient;
using StarrOnline.Common;
using StarrOnline.Interface;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

namespace StarrOnline.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        //ISettingsService _settingsService;

        private BindableCollection<Transactions> _AllTransactions = new BindableCollection<Transactions>();
        private DataConnector _data = new DataConnector();
        private Transactions _SelectedTransaction = new Transactions();


        public MainWindowViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService settingsService)
            : base(navigationService, windowManager, settingsService)
        {
            RefreshTransactions();
        }

        #region properties
        private string _isVisible;
        public string IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyOfPropertyChange(() => IsVisible);
            }
        }

        private string _showTotalRow = "0";
        public string showTotalRow
        {
            get { return _showTotalRow; }
            set
            {
                _showTotalRow = value;
                NotifyOfPropertyChange(() => showTotalRow);
            }
        }

        private bool _showCanceled = false;
        public bool showCanceled
        {
            get { return _showCanceled; }
            set
            {
                _showCanceled = value;
                NotifyOfPropertyChange(() => showCanceled);
            }
        }

        public Transactions SelectedTransaction
        {
            
            get { return _SelectedTransaction; }
            set
            {
                _SelectedTransaction = value;
                NotifyOfPropertyChange(() => SelectedTransaction);
            }
            
        }
        
        private string _searchField;
        public string searchField
        {
            get { return _searchField; }
            set
            {
                _searchField = value;
                NotifyOfPropertyChange(() => searchField);
            }
        }

        private string _searchParameter;
        public string searchParameter
        {
            get { return _searchParameter; }
            set
            {
                _searchParameter = value;
                NotifyOfPropertyChange(() => searchParameter);
            }
        }

        public BindableCollection<Transactions> Transactions
        {
            get
            {
                return _AllTransactions;
            }
            set
            {
                _AllTransactions = value;
                NotifyOfPropertyChange(() => Transactions);
            }
        }
        #endregion properties

        #region methods
        public void showCancelAction()
        {
            _showCanceled = !_showCanceled;
            NotifyOfPropertyChange(() => showCanceled);
        }

        public void BtnCreate()
        {
            _navigationService.GetWindow<ClientCreateViewModel>()
                .DoIfSuccess(() => RefreshTransactions())
                .ShowWindowModal();

            RefreshTransactions();
        }

        public void SeachTransactions()
        {
            string strParameter = "";
            string strSQLInitialize1 = DefaultValues.initializeTransactionWithCancelForNonAdmin;
            string strSQLInitialize2 = DefaultValues.initializeTransactionWithCancelForAdmin;

            string strSQL = DefaultValues.initializeTransaction;

            string filterCat = GlobalConfig.ApplyFilter;

            try
            {
                if (filterCat.ToUpper().Equals("USERS"))
                {
                    //USERS
                    strParameter += strSQLInitialize1 + GlobalConfig.companyID + "' and " + _searchField + " like '%" + _searchParameter + "%'";
                }
                else if (filterCat.ToUpper().Equals("USERGROUP"))
                {
                    //usergroup
                    strParameter += strSQLInitialize1 + GlobalConfig.companyID + "' and USERGRP ='" + GlobalConfig.userGroup + "' and " + _searchField + " like '%" + _searchParameter + "%'";
                }
                else if (filterCat.ToUpper().Equals("SUPERVISOR"))
                {
                    //accoutning
                    strParameter += strSQLInitialize1 + GlobalConfig.companyID + "' and " + _searchField + " like  '%" + _searchParameter + "%'"; ;
                }
                else
                {
                    //all
                    strParameter += strSQLInitialize2;
                }

                if (filterCat.Equals("USERS") || filterCat.Equals("USERGROUP") || filterCat.Equals("SUPERVISOR"))
                {
                    if (_showCanceled)
                        strParameter += " and STATUS in ('ACTIVE', 'CANCEL') and " + _searchField + " like '%" + _searchParameter + "%'"; 
                    else
                        strParameter += " and STATUS in ('ACTIVE') and " + _searchField + " like '%" + _searchParameter + "%'";
                }
                else
                {
                    if (_showCanceled)
                        strParameter += " where STATUS in ('ACTIVE', 'CANCEL') and " + _searchField + " like '%" + _searchParameter + "%'";
                    else
                        strParameter += " where STATUS in ('ACTIVE') and " + _searchField + " like '%" + _searchParameter + "%'";
                }


                strParameter += " ORDER BY ISSUEDATE";
                _AllTransactions = new BindableCollection<Transactions>(_data.GetTransactions_All(strParameter));
                _showTotalRow = _AllTransactions.Count.ToString() + " Total Number of Records";

                NotifyOfPropertyChange(() => showTotalRow);
                NotifyOfPropertyChange(() => Transactions);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return;
            }



            //NotifyOfPropertyChange(() => searchField);
            //NotifyOfPropertyChange(() => searchParameter);
            NotifyOfPropertyChange(() => Transactions);
        }

        public void RefreshTransactions()
        {
            string strParameter = "";
            _searchParameter = "";
            _searchField = "";
            string strSQLInitialize1 = DefaultValues.initializeTransactionWithCancelForNonAdmin;
            string strSQLInitialize2 = DefaultValues.initializeTransactionWithCancelForAdmin;

            string strSQL = DefaultValues.initializeTransaction;

            string filterCat = GlobalConfig.ApplyFilter;
            try
            {
                if (filterCat.ToUpper().Equals("USERS"))
                {
                    //USERS
                    strParameter += strSQLInitialize1 + GlobalConfig.companyID + "'";


                }else if (filterCat.ToUpper().Equals("USERGROUP"))
                {
                    //usergroup
                    strParameter += strSQLInitialize1 + GlobalConfig.companyID + "' and USERGRP ='" + GlobalConfig.userGroup + "'";

                }
                else if(filterCat.ToUpper().Equals("SUPERVISOR"))
                {
                    //accoutning
                    strParameter += strSQLInitialize1 + GlobalConfig.companyID + "'";

                }
                else
                {
                    //all
                    strParameter += strSQLInitialize2;

                }

                if (filterCat.Equals("USERS") || filterCat.Equals("USERGROUP") || filterCat.Equals("SUPERVISOR"))
                {
                    if (_showCanceled)
                        strParameter += " and STATUS in ('ACTIVE', 'CANCEL')";
                    else
                        strParameter += " and STATUS in ('ACTIVE')";
                }
                else
                {
                    if (_showCanceled)
                        strParameter += " where STATUS in ('ACTIVE', 'CANCEL')";
                    else
                        strParameter += " where STATUS in ('ACTIVE')";
                }


                strParameter += " ORDER BY ISSUEDATE";

                _AllTransactions = new BindableCollection<Transactions>(_data.GetTransactions_All(strParameter));

                _showTotalRow = _AllTransactions.Count.ToString() + " Total Number of Records" ;

                NotifyOfPropertyChange(() => showTotalRow);
                NotifyOfPropertyChange(() => searchParameter);
                NotifyOfPropertyChange(() => searchField);
                NotifyOfPropertyChange(() => Transactions);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return;
            }
        }

        public void BtnPreview()
        {
            try
            {
                if (_SelectedTransaction.COCNO != null)
                {
                    GlobalConfig.COCNumber = _SelectedTransaction.COCNO;
                    GlobalConfig.ProductType = _SelectedTransaction.PRODUCTTYPEDESCR;
                    GlobalConfig.PlanOption = _SelectedTransaction.PLANOPTIONDESCR;
                    GlobalConfig.PlanType = _SelectedTransaction.PLANTYPEDESCR;
                    GlobalConfig.iSource = 1;

                    _navigationService.GetWindow<ReportViewModel>()
                        .DoIfSuccess(() => RefreshTransactions())
                        .ShowWindowModal();

                    GlobalConfig.COCNumber = "";
                    GlobalConfig.ProductType = "";
                    GlobalConfig.PlanOption = "";
                    GlobalConfig.PlanType = "";
                }
                else
                {
                    MessageBox.Show("Please select Policy.", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        
        public void EditClient()
        {
            try
            {
                if (_SelectedTransaction.COCNO != null)
                {
                    GlobalConfig.COCNumber = _SelectedTransaction.COCNO;
                    GlobalConfig.ProductType = _SelectedTransaction.PRODUCTTYPEDESCR;
                    GlobalConfig.PlanOption = _SelectedTransaction.PLANOPTIONDESCR;
                    GlobalConfig.PlanType = _SelectedTransaction.PLANTYPEDESCR;
                    GlobalConfig.iSource = 1;

                    _navigationService.GetWindow<ClientEditViewModel>()
                        .DoIfSuccess(() => RefreshTransactions())
                        .ShowWindowModal();

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void BtnExport()
        {
            _navigationService.GetWindow<DataRangeViewModel>()
                                    .DoIfSuccess(() => RefreshTransactions())
                                    .ShowWindowModal();
        }

        public void BtnSettings()
        {
            _navigationService.GetWindow<SettingsViewModel>()
                        .DoIfSuccess(() => RefreshTransactions())
                        .ShowWindowModal();
        }
        #endregion methods
    }
}
