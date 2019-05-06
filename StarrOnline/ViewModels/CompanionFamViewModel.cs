using Caliburn.Micro;
using StarrOnline.Common;
using StarrOnline.Interface;
using StarrOnline.Models;
using StarrOnline.Utilities;
using System;
using System.Windows;

namespace StarrOnline.ViewModels
{
    public class CompanionFamViewModel : BaseViewModel
    {

        Companion _Companion;
        private string _SelectedCompanionType;

        #region property


        public string SelectedCompanionType
        {
            get {
                return _SelectedCompanionType;
            }
            set {
                _SelectedCompanionType = value;
                NotifyOfPropertyChange(() => SelectedCompanionType);
            }
        }

        public Companion Companion
        {
            get
            {
                return _Companion;
            }
            set
            {
                if (_Companion != value)
                {
                    _Companion = value;
                    NotifyOfPropertyChange(() => Companion);
                }
            }
        }

        #endregion

        #region method
        public CompanionFamViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService settingsService)
            : base(navigationService, windowManager, settingsService)
        {
            Companion = new Companion();
            //prepareInitialData();
        }
        #endregion
    }
}
