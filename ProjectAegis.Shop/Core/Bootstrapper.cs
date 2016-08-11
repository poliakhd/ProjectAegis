namespace ProjectAegis.Shop.Core
{
    using System.Windows;
    using System.Reflection;

    using Caliburn.Micro;

    using ViewModels;
    using Localization.Managers;
    using Localization.Providers;

    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
            InitializeLocalization();
        }



        #region Overrides of BootstrapperBase

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
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