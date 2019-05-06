using Caliburn.Micro;
using StarrOnline.Common;
using StarrOnline.Interface;
using StarrOnline.Models;
using StarrOnline.Utilities;
using System;
using System.Windows;

namespace StarrOnline.ViewModels
{
    public class CompanionIndViewModel : BaseViewModel
    {

        #region method
        public CompanionIndViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService settingsService)
            : base(navigationService, windowManager, settingsService)
        {
            //Companion = new Companion();
            //prepareInitialData();
        }
        #endregion
    }
}
