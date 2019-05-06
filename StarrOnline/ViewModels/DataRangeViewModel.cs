using Caliburn.Micro;
using StarrOnline.Interface;
using System;
using System.Windows;
using StarrOnline.Common;
using System.Data.SqlClient;
using System.Data;
using StarrOnline.Models;
using StarrOnline.ConfigSetup;

namespace StarrOnline.ViewModels
{
    public class DataRangeViewModel : BaseViewModel
    {
        private BindableCollection<UserGroupList> _AllGroup = new BindableCollection<UserGroupList>();
        private DataConnector _data = new DataConnector();

        public DataRangeViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService SettingService)
            : base(navigationService, windowManager, SettingService)
        {

            RefreshTransactions();
        }

        #region property
        public BindableCollection<UserGroupList> UserGroup
        {
            get
            {
                return _AllGroup;
            }
            set
            {
                _AllGroup = value;
                NotifyOfPropertyChange(() => UserGroup);
            }
        }
        #endregion property

        #region method
        private void RefreshTransactions()
        {
            string strSQL = "select * from " + GlobalConfig.GRPReport + " order by UserGroup";
            _AllGroup = new BindableCollection<UserGroupList>(_data.GetUserGroupList(strSQL));
            NotifyOfPropertyChange(() => UserGroup);
        }
        #endregion method
    }
}
