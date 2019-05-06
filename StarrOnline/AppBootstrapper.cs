using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using StarrOnline.Utilities;
using StarrOnline.ViewModels;
using StarrOnline.Interface;

namespace StarrOnline
{
    public class AppBootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();
            container.Instance<SimpleContainer>(container);

            //ViewModels
            container.Singleton<LoginViewModel>();
            container.Singleton<MainWindowViewModel>();
            container.PerRequest<ClientCreateViewModel>();
            container.PerRequest<PasswordViewModel>();
            container.PerRequest<SettingsViewModel>();
            //container.PerRequest<CompanionFamViewModel>();
            container.PerRequest<ClientEditViewModel>();
            //container.PerRequest<ClientDataViewModel>();
            container.PerRequest<ReportViewModel>();
            container.PerRequest<DataRangeViewModel>();

            //Services            
            container.PerRequest<IWindowManager, MyWindowManager>();
            container.PerRequest<ISettingsService, MySettingsService>();
            container.Singleton<Interface.INavigationService, NavigationService>();

            //AutoMapper
            //Mapper.CreateMap<Client, Client>();
            //Mapper.CreateMap<BookCategory, BookCategory>();
            //Mapper.CreateMap<Book, Book>();
            //Mapper.CreateMap<Employee, Employee>();
            //Mapper.CreateMap<Lending, Lending>();
            //Mapper.CreateMap<Address, Address>();
            //Mapper.CreateMap<LentBook, LentBook>();
            //Mapper.CreateMap<Person, Person>();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            Interface.INavigationService nav = (Interface.INavigationService) container.GetInstance(typeof(Interface.INavigationService), null);

            nav.GetWindow<MainWindowViewModel>().ShowWindow();

            bool result =  nav.GetWindow<LoginViewModel>()
                 .ShowWindowModal(new Dictionary<string, object>() { { "WindowStyle", WindowStyle.ToolWindow }, { "ResizeMode", ResizeMode.NoResize } });
            
            if (!result)
                Application.Current.Shutdown();
            else
                ((MainWindowViewModel)container.GetInstance(typeof(MainWindowViewModel), null)).RefreshTransactions();
               // nav.GetWindow<MainWindowViewModel>().ShowWindow();
        }
    }
}
