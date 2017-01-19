using System;
using System.Windows;
using System.Collections.Generic;

using Caliburn.Micro;

using ProjectAegis.AI.ViewModels;

namespace ProjectAegis.AI.Core
{
    public class Bootstrapper : BootstrapperBase
    {
        #region Private Members
        
        private readonly SimpleContainer _container = new SimpleContainer();

        #endregion

        public Bootstrapper()
        {
            _container.PerRequest<MainWindowViewModel>();

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            Initialize();
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
    }
}