using ProjectAegis.Shop.Providers;
using ProjectAegis.Shop.Providers.Interfaces;

namespace ProjectAegis.Shop.Core
{
    using System;
    using System.Windows;
    using System.Reflection;
    using System.Collections.Generic;

    using Caliburn.Micro;
    using Localization.Library.Managers;
    using Localization.Library.Providers;
    using ViewModels;

    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            _container.PerRequest<MainWindowViewModel>();
            _container.PerRequest<AddItemsWindowViewModel>();
            _container.PerRequest<SelectCategoryWindowViewModel>();

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<IItemInformationProvider, BasicItemInformationProvider>();

            Initialize();
            InitializeLocalization();        
        }

        #region Overrides of BootstrapperBase

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }
        
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        #endregion

        private void InitializeLocalization()
        {
            TranslationManager.Instance.TranslationProvider =
                new ResxTranslationProvider(
                    "ProjectAegis.Shop.Resources.Localization.Language",
                    Assembly.GetExecutingAssembly()
                );
        }
    }
}