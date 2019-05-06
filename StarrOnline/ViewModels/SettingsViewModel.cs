using Caliburn.Micro;
using StarrOnline.Common;
using StarrOnline.Interface;
using StarrOnline.Models;
using System;
using System.Data.SqlClient;
using System.Windows;


namespace StarrOnline.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {

        public SettingsViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService settingsService)
        : base(navigationService, windowManager, settingsService)
        {
            //connection = new SqlConnection(GlobalConfig.CnnString(DefaultValues.db));
        }


        public void changePassword()
        {
            _navigationService.GetWindow<PasswordViewModel>()
                        //.DoIfSuccess(() => RefreshTransactions())
                        .ShowWindowModal();
        }

        public void Close()
        {
            TryClose(true);
        }
    }
}
