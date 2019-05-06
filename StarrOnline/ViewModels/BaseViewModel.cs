using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using StarrOnline.Common;
using StarrOnline.Interface;
using StarrOnline.Models;

namespace StarrOnline.ViewModels
{
    public class BaseViewModel: Screen//,INotifyPropertyChanged
    {
        protected IWindowManager _windowManager;
        //protected IDBServiceManager<IDatabaseService> _dbServiceManager;
        //protected IDBManagerService _dBManagerService;
        protected Interface.INavigationService _navigationService;
        protected ISettingsService _settingsService;

        public BaseViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService settingservice)
        {
            _windowManager = windowManager;
            //_dBManagerService = dBManagerService;
            _settingsService = settingservice;
            _navigationService = navigationService;

            GlobalConfig.InitializeConnections(DatabaseType.Sql);
        }

        public virtual void Cancel()
        {
            TryClose();
        }

        //private void NotifyPropertyChanged(string info)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        ////#region INotifyPropertyChanged Members

        /////// <summary>
        /////// Raises the PropertyChange event for the property specified
        /////// </summary>
        /////// <param name="propertyName">Property name to update. Is case-sensitive.</param>
        //public virtual void RaisePropertyChanged(string propertyName)
        //{
        //    //this.VerifyPropertyName(propertyName);
        //    OnPropertyChanged(propertyName);
        //}

        ///// <summary>
        ///// Raised when a property on this object has a new value.
        ///// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;

        /////// <summary>
        /////// Raises this object's PropertyChanged event.
        /////// </summary>
        /////// <param name="propertyName">The property that has a new value.</param>
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    //this.VerifyPropertyName(propertyName);

        //    PropertyChangedEventHandler handler = this.PropertyChanged;
        //    if (handler != null)
        //    {
        //        var e = new PropertyChangedEventArgs(propertyName);
        //        handler(this, e);
        //    }
        //}

        //#endregion // INotifyPropertyChanged Members
    }
}
