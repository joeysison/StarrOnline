using Caliburn.Micro;
using StarrOnline.Common;
using StarrOnline.Interface;
using StarrOnline.Models;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace StarrOnline.ViewModels
{
    public class PasswordViewModel : BaseViewModel
    {
        private SqlConnection connection;
        private string _newPassword;
        private DataConnector _data = new DataConnector();

        public PasswordViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService settingsService)
            : base(navigationService, windowManager, settingsService)
        {
            connection = new SqlConnection(GlobalConfig.ConnectionStringData);
        }

        public string newPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                if (_newPassword != value)
                {
                    _newPassword = value;
                    NotifyOfPropertyChange(() => newPassword);
                }
            }
        }

        public void BtnSave(string param)
        {
            //MessageBox.Show(param);

            MessageBoxResult rslt = MessageBox.Show("Proceed in changing password", ":: WARNING ::", MessageBoxButton.YesNo, MessageBoxImage.Information);
            try { 
                if (rslt == MessageBoxResult.Yes)
                {
                    _data.savePassword(_newPassword, GlobalConfig.userID);
                    MessageBox.Show("PASSWORD has been CHANGED.", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Information);
                    TryClose(true);
                }else
                {
                    return;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

    }
}
